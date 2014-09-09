namespace JetStreamCommons
{
  using System;
  using System.Linq;
  using System.Collections;
  using System.Threading.Tasks;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.API.Items;


  public class RestManager : IDisposable
  {
    #region IDisposable
    private bool disposed = false;

    public void Dispose()
    { 
      Dispose(true);
      GC.SuppressFinalize(this);           
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposed)
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


      // Free any unmanaged objects here. 
      {
        // IDLE
      }

      disposed = true;
    }
    #endregion

    public RestManager()
    {
    }

    ~RestManager()
    {
      this.Dispose(false);
    }

    private ISitecoreWebApiSession GetAnonymousSession()
    {
      var result = SitecoreWebApiSessionBuilder.AnonymousSessionWithHost("http://jetstream.test24dk1.dk.sitecore.net/")

        // @adk : does not work with anonymous
//        .Site ("/sitecore/shell")
//        .DefaultDatabase("master") // flights are stored in "master" db
        .BuildSession();

      return result;
    }

    private ISitecoreWebApiSession GetAdminSession()
    {
      using (
        //TODO: move credentils info to the constructor
        var credentials = new WebApiCredentialsPODInsequredDemo("admindddddddd", "b"))
      {
        var result = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("http://jetstream.test24dk1.dk.sitecore.net/")
          .Credentials(credentials)
          .Site ("/sitecore/shell")
          .DefaultDatabase ("master")
          .BuildSession ();

        return result;
      }
    }

    private ISitecoreWebApiSession GetSession()
    {
      if (null == this.session)
      {
        this.session = this.GetAdminSession();
      }
        
      return this.session;
    }
      
    public async Task<ScItemsResponse> SearchAirportsWithNameContains(string name)
    {
      var session = this.GetSession ();

      string testQuery = QueryHelpers.QueryToSearchAirportsWithName(name); 

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(testQuery)
        .Build();

      ScItemsResponse responce = await session.ReadItemAsync(request);

      return responce;
    }

    public async Task<ScItemsResponse> SearchAllAirports()
    {
      var session = this.GetSession ();

      string testQuery = QueryHelpers.QueryToSearchAllAirports(); 

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(testQuery)
        .Build();

      ScItemsResponse responce = await session.ReadItemAsync(request);

      return responce;
    }

    public async Task<ScItemsResponse> SearchDepartTicketsWithRequest(SearchFlightsRequest request)
    {
      return await this.SearchTicketsWithRequest (request, true);
    }

    public async Task<ScItemsResponse> SearchReturnTicketsWithRequest(SearchFlightsRequest request)
    {
      return await this.SearchTicketsWithRequest (request, false);
    }

    private async Task<ScItemsResponse> SearchTicketsWithRequest(SearchFlightsRequest request, bool isDepart)
    {
      var session = this.GetSession ();

      string testQuery;

      if (isDepart)
      {
        testQuery = QueryHelpers.QueryToSearchDepartFlightsWithRequest (request); 
      }
      else
      {
        testQuery = QueryHelpers.QueryToSearchReturnFlightsWithRequest (request); 
      }

      var readRequest = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(testQuery)
        .Build();

      ScItemsResponse responce = await session.ReadItemAsync(readRequest);

      return responce;
    }

    private ISitecoreWebApiSession session;
  }
}

