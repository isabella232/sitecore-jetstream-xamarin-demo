namespace JetStreamCommons.FlightSearch
{
  using System;
  using JetStreamCommons.Airport;


  public interface IFlightSearchUserInput
  {
    IJetStreamAirport DepartureAirport { get; }
    IJetStreamAirport DestinationAirport { get; }

    DateTime? ForwardFlightDepartureDate { get; }
    DateTime? ReturnFlightDepartureDate { get; }

    TicketClass TicketClass { get; }
    int TicketsCount { get; }

    bool IsRoundTrip { get; }
  }
}

