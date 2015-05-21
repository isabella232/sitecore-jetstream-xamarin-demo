namespace JetStreamCommons
{
  using System;
  using System.Diagnostics;
  using System.Linq;
  using System.Threading.Tasks;
  using System.Collections.Generic;

  using JetStreamCommons.Flight;
  using JetStreamCommons.Airport;
  using JetStreamCommons.Helpers;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.API.Items;

  public class RestManager : IFlightsLoader, IDisposable
  {
    #region IDisposable
    private ISitecoreWebApiSession session;
    private ISitecoreWebApiSession GetSession()
    {
      return this.session;
    }

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
      
    public async Task< IEnumerable<IJetStreamAirport> > SearchAllAirports()
    {
      var session = this.GetSession();

      string testQuery = QueryHelpers.QueryToSearchAllAirports(); 

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(testQuery)
        .Build();

      ScItemsResponse responce = await session.ReadItemAsync(request);

      // TODO : maybe wrap in Task.Factory.StartNew()
      IEnumerable<IJetStreamAirport> result = responce.Select(item => new JetStreamAirportWithItem(item));
      return result;
    }

    public async Task<ScItemsResponse> SearchDepartTicketsWithRequest(SearchFlightsRequest request)
    {
      return await this.SearchTicketsWithRequest (request, true);
    }

    public async Task<ScItemsResponse> SearchReturnTicketsWithRequest(SearchFlightsRequest request)
    {
      return await this.SearchTicketsWithRequest(request, false);
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

    #region IFlightsLoader
    public async Task< IEnumerable<IJetStreamFlight> > LoadOneWayFlightsAsync(SearchFlightsRequest request)
    {
      var requestCopy = new SearchFlightsRequest(
        request.FromAirportId, 
        request.ToAirportId, 
        request.DepartDate.Value, 
        null);
      try
      {
        ScItemsResponse flightItems = await this.SearchDepartTicketsWithRequest(requestCopy);
        IEnumerable<IJetStreamFlight> result = flightItems.Select(item => new JetStreamFlightWithItem(item));
        return result;
      }
      catch (Exception exception)
      {
        Debug.WriteLine("Exce");
      }
      // TODO : maybe wrap in Task.Factory.StartNew()
      return null;
    }
    #endregion
  }
}

