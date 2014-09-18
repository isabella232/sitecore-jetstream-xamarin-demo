namespace JetStreamCommons.Flight
{
  using System;
  using JetStreamCommons.Airport;


  public interface IJetStreamFlightWithAirports : IJetStreamFlight
  {
    IJetStreamAirportWithTimeZone DepartureAirport { get; }
    IJetStreamAirportWithTimeZone ArrivalAirport { get; }
  }
}

