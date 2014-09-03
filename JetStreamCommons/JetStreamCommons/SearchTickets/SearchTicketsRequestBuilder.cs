using System;

namespace JetStreamCommons
{
  public class SearchTicketsRequestBuilder
  {
    public SearchTicketsRequestBuilder()
    {
    }

    public SearchTicketsRequestBuilder SetFromAirportItem(string airportId)
    {
      this.FromAirportId = airportId;
      return this;
    }

    public SearchTicketsRequestBuilder SetToAirportItem(string airportId)
    {
      this.ToAirportId = airportId;
      return this;
    }

    public SearchTicketsRequestBuilder SetDepartDate(DateTime date)
    {
      this.DepartDate = date;
      return this;
    }

    public SearchTicketsRequestBuilder SetReturnDate(DateTime date)
    {
      this.ReturnDate = date;
      return this;
    }

    public SearchFlightsRequest Build()
    {
      SearchFlightsRequest request = new SearchFlightsRequest (
                                        this.FromAirportId,
                                        this.ToAirportId,
                                        this.DepartDate,
                                        this.ReturnDate
                                     );
      return request;
    }

    private string FromAirportId;
    private string ToAirportId;
    private DateTime DepartDate;
    private DateTime ReturnDate;
  }
}

