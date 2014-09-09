using System;

namespace JetStreamCommons
{
  public class SearchFlightsRequest
  {
    private DateTime? optionalReturnDate;

    public SearchFlightsRequest(
      string fromAirportId, 
      string toAirportId, 
      DateTime departDate,
      DateTime? returnDate
    )
    {
      this.FromAirportId = fromAirportId;
      this.ToAirportId = toAirportId;
      this.DepartDate = departDate;
      this.optionalReturnDate = returnDate;
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
      get
      {
        return this.optionalReturnDate.Value;
      }
    }

    public bool RoundTrip
    {
      get
      {
        bool result = (this.optionalReturnDate != null);
        return result;
      }
    }
  }
}

