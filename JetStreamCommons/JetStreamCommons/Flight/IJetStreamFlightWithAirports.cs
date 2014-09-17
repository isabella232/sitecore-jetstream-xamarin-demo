namespace JetStreamCommons.Flight
{
  using System;
  using JetStreamCommons.Airport;


  public interface IJetStreamFlightWithAirports : IJetStreamFlight
  {
    IJetStreamAirport DepartureAirport { get; }
    IJetStreamAirport ArrivalAirport { get; }
  }
}

