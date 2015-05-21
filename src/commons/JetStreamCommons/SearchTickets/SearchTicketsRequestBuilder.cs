using System;

namespace JetStreamCommons
{
  public class SearchTicketsRequestBuilder
  {
    public SearchTicketsRequestBuilder()
    {
    }

    public SearchTicketsRequestBuilder Set 
    { 
      get
      {
        return this;
      }
    }

    public SearchTicketsRequestBuilder SourceAirport(string airportId)
    {
      this.fromAirportId = airportId;
      return this;
    }

    public SearchTicketsRequestBuilder DestinationAirport(string airportId)
    {
      this.toAirportId = airportId;
      return this;
    }

    public SearchTicketsRequestBuilder DepartureDate(DateTime date)
    {
      this.departDate = date;
      return this;
    }

    public SearchTicketsRequestBuilder ReturnDate(DateTime date)
    {
      this.returnDate = date;
      return this;
    }

    public SearchTicketsRequestBuilder RoundTrip(bool roundTrip)
    {
      this.roundTrip = roundTrip;
      return this;
    }

    public SearchFlightsRequest Build()
    {
      this.CheckRequestDataWithExeption();

      DateTime? returnDateValue = null;
      if (this.roundTrip)
      {
        returnDateValue = this.returnDate.Value;
      }

      SearchFlightsRequest request = new SearchFlightsRequest (
                                        this.fromAirportId,
                                        this.toAirportId,
                                        this.departDate.Value,
                                        returnDateValue
                                     );
      return request;
    }

    private void CheckRequestDataWithExeption()
    {
      if (null == this.fromAirportId)
      {
        throw new ArgumentNullException("FROM_AIRPORT_IS_NULL");
      }

      if (null == this.toAirportId)
      {
        throw new ArgumentNullException("TO_AIRPORT_IS_NULL");
      }

      if (null == this.departDate)
      {
        throw new ArgumentNullException("DEPART_DATE_IS_NULL");
      }

      if (null == this.returnDate && this.roundTrip)
      {
        throw new ArgumentNullException("RETURN_DATE_IS_NULL");
      }

      DateTime pastDate = DateTime.Now;

      DateTime departDateValue = this.departDate.Value;
      DateTime returnDateValue = this.returnDate.Value;

      if (pastDate.Date > departDateValue.Date)
      {
        throw new ArgumentNullException("DEPART_DATE_MUST_BE_A_FUTURE");
      }

      if (pastDate.Date > returnDateValue.Date)
      {
        throw new ArgumentNullException("RETURN_DATE_MUST_BE_A_FUTURE");
      }

      if (departDateValue.Date > returnDateValue.Date)
      {
        throw new ArgumentException("RETURN_DATE_MUST_BE_AFTER_DEPARTURE");
      }
    }

    private string fromAirportId;
    private string toAirportId;
    private DateTime? departDate;
    private DateTime? returnDate;
    private bool roundTrip;
  }
}

