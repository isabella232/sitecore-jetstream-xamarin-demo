namespace JetStreamCommons
{
  using System;
  using Sitecore.MobileSDK.API.Items;


  public class JetStreamFlight
  {
    public JetStreamFlight(ISitecoreItem flight)
    {
      this.Flight = flight;
    }

    private ISitecoreItem Flight
    {
      get;
      set;
    }

    public string DepartureAirportId
    {
      get
      {
        return this.Flight["Departure Airport"].RawValue;
      }
    }

    public string ArrivalAirportId
    {
      get
      {
        return this.Flight["Arrival Airport"].RawValue;
      }
    }

    public string DepartureTime
    {
      get
      {
        return this.Flight["Departure Time"].RawValue;
      }
    }

    public string ArrivalTime
    {
      get
      {
        return this.Flight["Arrival Time"].RawValue;
      }
    }

    public string Price
    {
      get
      {
        return this.Flight["Price"].RawValue;
      }
    }

    public bool IsPersonalEntertainmentIncluded
    {
      get
      {
        return this.Flight["Personal Entertainment"].RawValue.ToLowerInvariant().Equals("true");
      }
    }

    public bool IsInFlightWifiIncluded
    {
      get
      {
        return this.Flight["In Flight Wifi"].RawValue;
      }
    }

    public bool IsFoodServiceIncluded
    {
      get
      {
        return this.Flight["Food Service"].RawValue;
      }
    }
  }
}

