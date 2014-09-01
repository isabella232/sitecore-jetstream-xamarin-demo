using System;

namespace JetStreamCommons
{
  public class SearchTicketsRequestBuilder
  {
    public SearchTicketsRequestBuilder()
    {
    }

    public SearchTicketsRequestBuilder SetFromAirportName(string airportName)
    {
      this.FromAirportName = airportName;
      return this;
    }

    public SearchTicketsRequestBuilder SetToAirportName(string airportName)
    {
      this.ToAirportName = airportName;
      return this;
    }

    public SearchTicketsRequestBuilder SetFlightDate(DateTime date)
    {
      this.FlightDate = date;
      return this;
    }

    public SearchTicketsRequestBuilder SetTicketClass(int ticketClass)
    {
      this.TicketClass = ticketClass;
      return this;
    }

    public SearchTicketsRequestBuilder SetTicketsCount(int ticketsCount)
    {
      this.TicketsCount = ticketsCount;
      return this;
    }

    public SearchTicketsRequestBuilder SetRoundtrip(bool roundtrip)
    {
      this.Roundtrip = roundtrip;
      return this;
    }

    public SearchTicketsRequest Build()
    {
      SearchTicketsRequest request = new SearchTicketsRequest (
                                       this.FromAirportName,
                                       this.ToAirportName,
                                       this.FlightDate,
                                       this.TicketClass,
                                       this.TicketsCount,
                                       this.Roundtrip
                                     );
      return request;
    }

    private string FromAirportName;
    private string ToAirportName;
    private DateTime FlightDate;
    private int TicketClass;
    private int TicketsCount; 
    private bool Roundtrip;
  }
}

