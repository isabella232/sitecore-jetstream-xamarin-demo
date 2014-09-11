namespace JetStreamCommons.Flight
{
  public interface IJetStreamFlight
  {
    string DepartureAirportId
    {
      get;
    }

    string ArrivalAirportId
    {
      get;
    }

    string DepartureTime
    {
      get;
    }

    string ArrivalTime
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

