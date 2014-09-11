namespace JetStreamCommons.Flight
{
  using System;
  using Sitecore.MobileSDK.API.Items;


  public class JetStreamFlightWithItem : IJetStreamFlight
  {
    public JetStreamFlightWithItem(ISitecoreItem flight)
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

    public decimal Price
    {
      get
      {
        string rawResult = this.Flight["Price"].RawValue;
        decimal result = Decimal.Parse(rawResult);

        return result;
      }
    }

    public bool IsPersonalEntertainmentIncluded
    {
      get
      {
        return RawValueConverter.ToBoolean(this.Flight["Personal Entertainment"].RawValue);
      }
    }

    public bool IsInFlightWifiIncluded
    {
      get
      {
        return RawValueConverter.ToBoolean(this.Flight["In Flight Wifi"].RawValue);
      }
    }

    public bool IsFoodServiceIncluded
    {
      get
      {
        return RawValueConverter.ToBoolean(this.Flight["Food Service"].RawValue);
      }
    }
  }
}

