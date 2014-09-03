using System;
using Sitecore.MobileSDK.Items;

namespace JetStreamCommons
{
  public class SearchFlightsRequest
  {
    public SearchFlightsRequest(
      ScItem fromAirportName, 
      ScItem toAirportName, 
      DateTime departDate,
      DateTime returnDate,
      bool RoundTrip
    )
    {
      this.FromAirportName = fromAirportName; //TODO: must be ID!!!
      this.ToAirportName   = toAirportName;     //TODO: must be ID!!!
      this.DepartDate      = departDate;
      this.ReturnDate      = returnDate;
    }

    public ScItem FromAirportName
    {
      get;
      private set;
    }

    public ScItem ToAirportName
    {
      get;
      private set;
    }

    public DateTime DepartDate
    {
      get;
      private set;
    }

    public DateTime ReturnDate
    {
      get;
      private set;
    }

    public bool RoundTrip
    {
      get;
      private set;
    }
  }
}

