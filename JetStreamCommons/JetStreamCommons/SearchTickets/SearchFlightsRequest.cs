using System;

namespace JetStreamCommons
{
  public class SearchFlightsRequest
  {
    private DateTime? optionalReturnDate;
    private DateTime? optionalDepartDate;

    public SearchFlightsRequest(
      string fromAirportId, 
      string toAirportId, 
      DateTime departDate,
      DateTime? returnDate
    )
    {
      this.FromAirportId = fromAirportId;
      this.ToAirportId = toAirportId;
      this.optionalDepartDate = departDate;
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

    public DateTime? DepartDate
    {
      get
      {
        return this.optionalDepartDate;
      }
    }

    public DateTime? ReturnDate
    {
      get
      {
        return this.optionalReturnDate;
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

