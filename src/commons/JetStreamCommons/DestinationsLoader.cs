
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
    private const int DESTINATIONS_PER_PAGE = 500; //all items in one request

    private ISitecoreSSCSession session;

    private bool disposed = false;

    public DestinationsLoader(ISitecoreSSCSession session)
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

    public async Task<List<IDestination>> LoadOnlyDestinations(bool onlyWithCorrectCoordinates = false)
    {
      string destinationsQueryItemId = QueryHelpers.QueryItemIdToSearchAllCityItems();

      //care about extra pages
      var destinationsRequest = ItemSSCRequestBuilder.StoredQuerryRequest(destinationsQueryItemId)
                                                     .PageNumber(0)
                                                     .ItemsPerPage(DESTINATIONS_PER_PAGE)
                                                     .Build();

      var destinationsResponce = await this.session.RunStoredQuerryAsync(destinationsRequest);

      var destinations = destinationsResponce.Select(item => new Destination(item) as IDestination);

      return onlyWithCorrectCoordinates ? destinations.Where(item => item.IsCoordinatesAvailable).ToList() : destinations.ToList();
    }

    public async Task<List<IAttraction>> LoadAttractions(IDestination destination)
    {
      //care about extra pages
      var attractionsRequest = ItemSSCRequestBuilder.ReadChildrenRequestWithId(destination.Id)
                                                    .PageNumber(0)
                                                    .ItemsPerPage(DESTINATIONS_PER_PAGE)
                                                    .Build();

      var atractionsResponce = await this.session.ReadChildrenAsync(attractionsRequest);

      var atractions = atractionsResponce.Select(item => new Attraction(item) as IAttraction);

      return atractions.ToList();
    }

    #endregion Public API

    #region Private API

    private async Task<ScItemsResponse> LoadRegionsAndCountries()
    {
      var regionsAndCountriesQuery = QueryHelpers.QueryItemIdToSearchAllRegionsAndCountriesItems();
      var regionsAndCountriesRequest = ItemSSCRequestBuilder.StoredQuerryRequest(regionsAndCountriesQuery)
        .Build();

      return await this.session.RunStoredQuerryAsync(regionsAndCountriesRequest);
    }

    private List<Region> MatchRegionsCountriesAndDestinations(ScItemsResponse regionsAndCountriesResponse, IEnumerable<IDestination> destionations)
    {
      //TODO: Please notice that sometimes countries can have nested country like United Kingdom -> England
      var countries = regionsAndCountriesResponse.Where((item, i) => item["TemplateName"].Equals(Country.TemplateName))
        .Select(country =>
        {
          var countryDestination = destionations.Where(dest => dest.CountryName.Equals(country.DisplayName));
          return new Country(countryDestination, country);
        });

      var regions = regionsAndCountriesResponse.Where(item => item["TemplateName"].Equals(Region.TemplateName))
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