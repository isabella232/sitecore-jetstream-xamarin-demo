using System;
using Sitecore.MobileSDK.Items;

namespace JetStreamCommons
{
  public class JetStreamFlight
  {
    public JetStreamFlight(ScItem flight)
    {
      this.Flight = flight;
    }

    private ScItem Flight
    {
      get;
      set;
    }

    public string DepartureAirportId
    {
      get
      {
        return this.Flight.FieldWithName ("Departure Airport").RawValue;
      }
    }

    public string ArrivalAirportId
    {
      get
      {
        return this.Flight.FieldWithName ("Arrival Airport").RawValue;
      }
    }

    public string DepartureDate
    {
      get
      {
        return this.Flight.FieldWithName ("Departure Time").RawValue;
      }
    }

    public string DeparturePrice
    {
      get
      {
        return this.Flight.FieldWithName ("Price").RawValue;
      }
    }

    public string DeparturePersonalEntertainment
    {
      get
      {
        return this.Flight.FieldWithName ("Personal Entertainment").RawValue;
      }
    }

    public string DepartureInFlightWifi 
    {
      get
      {
        return this.Flight.FieldWithName ("In Flight Wifi").RawValue;
      }
    }

    public string DepartureFoodService
    {
      get
      {
        return this.Flight.FieldWithName ("Food Service").RawValue;
      }
    }

  }
}

