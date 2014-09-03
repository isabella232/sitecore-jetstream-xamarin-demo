using System;

namespace JetStreamCommons
{
  public class SearchFlightsRequest
  {
    public SearchFlightsRequest(
      string fromAirportId, 
      string toAirportId, 
      DateTime departDate,
      DateTime returnDate
    )
    {
      this.FromAirportId = fromAirportId;
      this.ToAirportId   = toAirportId;
      this.DepartDate      = departDate;
      this.ReturnDate      = returnDate;
      this.RoundTrip = (returnDate != null);  //TODO: do we need this additional option in ui?
    }

    public string FromAirportId
    {
      get;
      private set;
    }

    public string ToAirportId
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

