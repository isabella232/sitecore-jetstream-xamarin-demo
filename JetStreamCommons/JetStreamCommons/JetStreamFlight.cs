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

    public string DepartureDate
    {
      get
      {
        return this.Flight["Departure Time"].RawValue;
      }
    }

    public string DeparturePrice
    {
      get
      {
        return this.Flight["Price"].RawValue;
      }
    }

    public string DeparturePersonalEntertainment
    {
      get
      {
        return this.Flight["Personal Entertainment"].RawValue;
      }
    }

    public string DepartureInFlightWifi 
    {
      get
      {
        return this.Flight["In Flight Wifi"].RawValue;
      }
    }

    public string DepartureFoodService
    {
      get
      {
        return this.Flight["Food Service"].RawValue;
      }
    }
  }
}

