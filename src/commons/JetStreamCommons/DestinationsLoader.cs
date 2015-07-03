using JetStreamCommons.Destinations;
using Sitecore.MobileSDK.API.Request.Parameters;

namespace JetStreamCommons
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using JetStreamCommons.Destinations;
  using JetStreamCommons.Helpers;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Session;

  public class DestinationsLoader : IDisposable
  {
    private ISitecoreWebApiSession session;

    private bool disposed = false;

    public DestinationsLoader(ISitecoreWebApiSession session)
    {
      this.session = session;
    }

    #region Public API
    public async Task<List<Region>> LoadAllDestinations()
    {
      var destination = await this.LoadOnlyDestinations();
      var regionsAndCountries = await this.LoadRegionsAndCountries();

      return await Task.Factory.StartNew(() => this.MatchRegionsCountriesAndDestinations(regionsAndCountries, destination));
    }

    public async Task<List<IDestination>> LoadOnlyDestinations()
    {
      string destinationsQuery = QueryHelpers.QueryToSearchAllCityItems();

      var destinationsRequest = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(destinationsQuery)
        .Build();

      var destinationsResponce = await this.session.ReadItemAsync(destinationsRequest);

      var destinations = destinationsResponce.Select(item => new Destination(item) as IDestination);

      return destinations.ToList();
    }

    public async Task<List<IDestination>> LoadOnlyDestinations(bool onlyWithCorrectCoordinates)
    {
      List<IDestination> result = await this.LoadOnlyDestinations();

      if (onlyWithCorrectCoordinates)
      {
        List<IDestination> filteredResult = new List<IDestination>();

        foreach (IDestination elem in result)
        {
          if (elem.IsCoordinatesAvailable)
          {
            filteredResult.Add(elem);
          }
        }

        return filteredResult;
      }

      return result;
    }

    public async Task<List<IAttraction>> LoadAttractions(IDestination destination)
    {

      var attractionsRequest = ItemWebApiRequestBuilder.ReadItemsRequestWithId(destination.Id)
        .AddScope(ScopeType.Children)
        .Build();

      var atractionsResponce = await this.session.ReadItemAsync(attractionsRequest);

      var atractions = atractionsResponce.Select(item => new Attraction(item) as IAttraction);

      return atractions.ToList();
    }

    #endregion Public API

    #region Private API

    private async Task<ScItemsResponse> LoadRegionsAndCountries()
    {
      var regionsAndCountriesQuery = QueryHelpers.QueryToSearchAllRegionsAndCountriesItems();
      var regionsAndCountriesRequest = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(regionsAndCountriesQuery)
        .Build();

      return await this.session.ReadItemAsync(regionsAndCountriesRequest);
    }

    private List<Region> MatchRegionsCountriesAndDestinations(ScItemsResponse regionsAndCountriesResponse, IEnumerable<IDestination> destionations)
    {
      //TODO: Please notice that sometimes countries can have nested country like United Kingdom -> England
      var countries = regionsAndCountriesResponse.Where((item, i) => item.Template.Equals(Country.TemplateName))
        .Select(country =>
        {
          var countryDestination = destionations.Where(dest => dest.CountryName.Equals(country.DisplayName));
          return new Country(countryDestination, country);
        });

      var regions = regionsAndCountriesResponse.Where(item => item.Template.Equals(Region.TemplateName))
        .Select(region =>
        {
          var regionCountries = countries.Where(item => region.DisplayName.Equals(item.RegionName));
          return new Region(regionCountries, region);
        });

      return regions.ToList();
    }
    #endregion Private API

    #region IDisposable implementation
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.disposed)
      {
        return;
      }

      // Free any other managed objects here.
      if (disposing)
      {
        if (null != this.session)
        {
          this.session.Dispose();
          this.session = null;
        }
      }


      this.disposed = true;
    }

    ~DestinationsLoader()
    {
      this.Dispose(false);
    }
    #endregion IDisposable implementation
  }
}