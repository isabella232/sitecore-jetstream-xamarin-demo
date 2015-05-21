namespace JetStreamCommons
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.Linq;
  using System.Threading.Tasks;
  using JetStreamCommons.Destionations;
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
    public async Task<List<Region>> LoadAllDestionations()
    {
      var destionations = await this.LoadOnlyDestionations();
      var regionsAndCountries = await this.LoadRegionsAndCountries();

      return await Task.Factory.StartNew(() => this.MatchRegionsCountriesAndDestionations(regionsAndCountries, destionations));
    }

    public async Task<List<Destionation>> LoadOnlyDestionations()
    {
      string destionationsQuery = QueryHelpers.QueryToSearchAllCityItems();

      var destinationsRequest = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(destionationsQuery)
        .Build();

      var destionationsResponce = await this.session.ReadItemAsync(destinationsRequest);
      var destionations = destionationsResponce.Select(item => new Destionation(item));

      return destionations.ToList();
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

    private List<Region> MatchRegionsCountriesAndDestionations(ScItemsResponse regionsAndCountriesResponse, IEnumerable<Destionation> destionations)
    {
      //TODO: Please notice that sometimes countries can have nested country like United Kingdom -> England
      var countries = regionsAndCountriesResponse.Where((item, i) => item.Template.Equals(Country.TemplateName))
        .Select(country =>
        {
          var countryDestionation = destionations.Where(dest => dest.CountryName.Equals(country.DisplayName));
          return new Country(countryDestionation, country);
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