namespace JetStreamCommons.Flight
{
  using System;

  public interface IJetStreamFlight
  {
    string FlightNumber
    {
      get;
    }

    string DepartureAirportId
    {
      get;
    }

    string ArrivalAirportId
    {
      get;
    }

    DateTime DepartureTime
    {
      get;
    }

    DateTime ArrivalTime
    {
      get;
    }

    decimal Price
    {
      get;
    }

    bool IsPersonalEntertainmentIncluded
    {
      get;
    }

    bool IsInFlightWifiIncluded
    {
      get;
    }

    bool IsFoodServiceIncluded
    {
      get;
    }
  }
}

