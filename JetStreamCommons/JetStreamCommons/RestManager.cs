﻿

namespace JetStreamCommons
{
  using System;
  using System.Linq;
  using System.Threading.Tasks;
  using System.Collections;
  using System.Collections.Generic;

  using JetStreamCommons.Airport;

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

    ~RestManager()
    {
      this.Dispose(false);
    }
    #endregion

    public RestManager(ISitecoreWebApiSession sessionToConsume)
    {
      this.session = sessionToConsume;
    }

    private ISitecoreWebApiSession GetSession()
    {
      return this.session;
    }
      
    public async Task< IEnumerable<IJetStreamAirport> > SearchAllAirports()
    {
      var session = this.GetSession();

      string testQuery = QueryHelpers.QueryToSearchAllAirports(); 

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(testQuery)
        .Build();

      ScItemsResponse responce = await session.ReadItemAsync(request);

      IEnumerable<IJetStreamAirport> result = responce.Select(item => new JetStreamAirportWithItem(item));
      return result;
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

