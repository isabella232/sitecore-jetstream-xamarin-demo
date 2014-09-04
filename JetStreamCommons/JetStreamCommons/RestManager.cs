using System;
using System.Collections;
using Sitecore.MobileSDK.API.Session;
using System.Threading.Tasks;
using Sitecore.MobileSDK.API;
using Sitecore.MobileSDK.API.Items;
using System.Linq;

namespace JetStreamCommons
{
  public class RestManager
  {
    public RestManager()
    {
    }

    ~RestManager()
    {
      if (null != this.session)
      {
        this.session.Dispose ();
        this.session = null;
      }
    }

    public ISitecoreWebApiSession GetSession()
    {
      if (null == this.session)
      {
        using (
          //TODO: move credentils info to the constructor
          var credentials = 
            new WebApiCredentialsPODInsequredDemo (
              "admin", 
              "b"))
        {
          var result = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost ("http://jetstream.sc-demo.net/")
          .Credentials (credentials)
          .Site ("/sitecore/shell")
          .DefaultDatabase ("master")
          .BuildSession ();

          this.session = result;
        }
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

