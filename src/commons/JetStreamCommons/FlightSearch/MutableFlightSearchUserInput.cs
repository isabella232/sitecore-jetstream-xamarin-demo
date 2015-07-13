namespace JetStreamCommons.FlightSearch
{
  using System;
  using JetStreamCommons.Airport;

  public class MutableFlightSearchUserInput : IFlightSearchUserInput
  {
    public IJetStreamAirport DepartureAirport { get; set; }
    public IJetStreamAirport DestinationAirport { get; set; }

    public DateTime? ForwardFlightDepartureDate { get; set; }
    public DateTime? ReturnFlightDepartureDate { get; set; }

    public TicketClass TicketClass { get; set; }
    public int TicketsCount { get; set; }

    public bool IsRoundTrip
    {
      get
      {
        return ( null != this.ReturnFlightDepartureDate );
      }
    }
  }
}

