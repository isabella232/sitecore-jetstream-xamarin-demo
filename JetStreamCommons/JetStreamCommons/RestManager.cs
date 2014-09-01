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
      
    public async Task<ScItemsResponse> GetFullAirportsList()
    {
      var session = this.GetSession ();

      string testQuery = QueryHelpers.QueryToGetAllFlights(); 

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(testQuery)
        .Build();

      ScItemsResponse responce = await session.ReadItemAsync(request);

      return responce;
    }

    public async Task<ScItemsResponse> SearchTicketsWith(SearchTicketsRequest request)
    {
      var session = this.GetSession ();

      string testQuery = QueryHelpers.QueryToGetAllFlights(); 

      var readRequest = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(testQuery)
        .Build();

      ScItemsResponse responce = await session.ReadItemAsync(readRequest);

      return responce;
    }


    private ISitecoreWebApiSession session;
  }
}

