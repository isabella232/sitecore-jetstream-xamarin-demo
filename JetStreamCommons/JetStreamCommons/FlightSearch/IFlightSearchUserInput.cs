namespace JetStreamCommons.FlightSearch
{
  using System;
  using JetStreamCommons.Airport;


  public interface IFlightSearchUserInput
  {
    IJetStreamAirport SourceAirport { get; }
    IJetStreamAirport DestinationAirport { get; }

    DateTime ForwardFlightDepartureDate { get; }
    DateTime? ReturnFlightDepartureDate { get; }

    TicketClass TicketClass { get; }
    int DatePrecisionInDays { get; }

    bool IsRoundTrip { get; }
  }
}

