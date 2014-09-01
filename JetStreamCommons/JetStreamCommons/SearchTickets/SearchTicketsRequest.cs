using System;

namespace JetStreamCommons
{
  public class SearchTicketsRequest
  {
    public SearchTicketsRequest(
      string fromAirportName, 
      string toAirportName, 
      DateTime flightDate, 
      int ticketClass, 
      int ticketsCount, 
      bool roundtrip
    )
    {
      this.FromAirportName = fromAirportName; //TODO: must be ID!!!
      this.ToAirportName = toAirportName; //TODO: must be ID!!!
      this.FlightDate = flightDate;
      this.TicketClass = ticketClass;
      this.TicketsCount = ticketsCount;
      this.Roundtrip = roundtrip;
    }

    public string FromAirportName
    {
      get;
      private set;
    }

    public string ToAirportName
    {
      get;
      private set;
    }

    public DateTime FlightDate
    {
      get;
      private set;
    }

    public int TicketClass
    {
      get;
      private set;
    } 

    public int TicketsCount
    {
      get;
      private set;
    }

    public bool Roundtrip
    {
      get;
      private set;
    }
  }
}

