namespace JetStreamCommons
{
  using System;
  using System.Threading.Tasks;
  using System.Collections.Generic;

  using JetStreamCommons.Flight;
  using JetStreamCommons.Airport;
  using JetStreamCommons.Helpers;
  using JetStreamCommons.Timezone;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public class RestManager : IFlightsLoader, IDisposable
  {
    private ITimeZoneProvider timezoneProvider;

    #region IDisposable
    private ISitecoreSSCSession globalSession;
    private ISitecoreSSCSession GetSession()
    {
      return this.globalSession;
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
        if (null != this.globalSession)
        {
          this.globalSession.Dispose();
          this.globalSession = null;
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

    public RestManager(ISitecoreSSCSession sessionToConsume, ITimeZoneProvider timezoneProvider)
    {
      this.globalSession = sessionToConsume;
      this.timezoneProvider = timezoneProvider;
    }

    public async Task<IEnumerable<IJetStreamAirport>> SearchAllAirports()
    {
      var session = this.GetSession();

      string testQuery = QueryHelpers.QueryItemIdToSearchAllAirports();

      var request = ItemSSCRequestBuilder.StoredQuerryRequest(testQuery)
        .Build();

      ScItemsResponse responce = await session.RunStoredQuerryAsync(request);


      var result = new List<IJetStreamAirport>();
      foreach (ISitecoreItem airportItem in responce)
      {
        //        ITimeZoneInfo timeZone = await this.TimezoneForAirportAsync(airportItem);
        //        IJetStreamAirport airport = new JetStreamAirportWithItem(airportItem, timeZone);

        IJetStreamAirport airport = new JetStreamAirportWithItem(airportItem, null);

        result.Add(airport);
      }

      return result.ToArray();
    }

    private async Task<ITimeZoneInfo> TimezoneForAirportAsync(ISitecoreItem airportItem)
    {
      string timezoneItemId = airportItem["Time Zone"].RawValue;

      var timezoneItemRequest = ItemSSCRequestBuilder.ReadItemsRequestWithId(timezoneItemId)
        .Build();
      ScItemsResponse timezoneResponse = await globalSession.ReadItemAsync(timezoneItemRequest);

      ISitecoreItem timeZoneItem = timezoneResponse[0];
      ITimeZoneInfo timeZone = new TimeZoneInfoWithItem(timeZoneItem, this.timezoneProvider);

      return timeZone;
    }

    private async Task<ScItemsResponse> SearchTicketsWithRequest(SearchFlightsRequest request)
    {
      return null;
      //var session = this.GetSession();

      //string testQuery = QueryHelpers.QueryToSearchDepartFlightsWithRequest(request);
      //var readRequest = ItemSSCRequestBuilder.ReadItemsRequestWithSitecoreQuery(testQuery)
      //  .Payload(PayloadType.Content)
      //  .Build();

      //ScItemsResponse responce = await session.ReadItemAsync(readRequest);

      //return responce;
    }

    #region IFlightsLoader
    public async Task<IEnumerable<IJetStreamFlightWithAirports>> LoadOneWayFlightsAsync(SearchFlightsRequest request)
    {
      var requestCopy = new SearchFlightsRequest(
        request.FromAirportId,
        request.ToAirportId,
        request.DepartDate.Value,
        null);

      ScItemsResponse flightItems = await this.SearchTicketsWithRequest(requestCopy);

      var result = new List<IJetStreamFlightWithAirports>();
      foreach (ISitecoreItem singleFlightItem in flightItems)
      {
        string departureAirportId = singleFlightItem["Departure Airport"].RawValue;
        IJetStreamAirportWithTimeZone departureAirport = await this.LoadAirportWithTimezoneByIdAsync(departureAirportId);

        string arrivalAirportId = singleFlightItem["Arrival Airport"].RawValue;
        IJetStreamAirportWithTimeZone arrivalAirport = await this.LoadAirportWithTimezoneByIdAsync(arrivalAirportId);


        var newFlight = new JetStreamFlightWithItem(singleFlightItem, departureAirport, arrivalAirport);
        result.Add(newFlight);
      }

      return result;
    }

    private async Task<IJetStreamAirportWithTimeZone> LoadAirportWithTimezoneByIdAsync(string airportId)
    {
      var session = this.GetSession();
      var request = ItemSSCRequestBuilder.ReadItemsRequestWithId(airportId)
        .Build();

      ScItemsResponse airportResponse = await session.ReadItemAsync(request);
      ISitecoreItem airportItem = airportResponse[0];
      ITimeZoneInfo timezone = await this.TimezoneForAirportAsync(airportItem);

      var result = new JetStreamAirportWithItem(airportItem, timezone);
      return result;
    }
    #endregion
  }
}

