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
      using ( var session = this.GetSession() )
      {
        string testQuery = QueryHelpers.QueryToGetAllFlights(); 

        var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(testQuery)
          .Build();
        
        ScItemsResponse responce = await session.ReadItemAsync(request);

        return responce;
      }
    }

    private ISitecoreWebApiSession session;
  }
}

