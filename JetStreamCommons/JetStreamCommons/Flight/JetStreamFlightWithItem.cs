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

    public string FlightNumber
    {
      get
      {
        return this.Flight["Flight Number"].RawValue;
      }
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

    public DateTime DepartureTime
    {
      get
      {
        string rawResult = this.Flight["Departure Time"].RawValue;
        return RawValueConverter.ToDateTime(rawResult);
      }
    }

    public DateTime ArrivalTime
    {
      get
      {
        string rawResult = this.Flight["Arrival Time"].RawValue;
        return RawValueConverter.ToDateTime(rawResult);
      }
    }

    public TimeSpan Duration 
    { 
      get
      {
        // @adk : DepartureTime, ArrivalTime are local.
        // Airport info must be used to produce proper output.

        // TODO : add timezone computation
        return this.ArrivalTime.Subtract(this.DepartureTime);
      }
    }

    public decimal Price
    {
      get
      {
        string rawResult = this.Flight["Price"].RawValue;
        decimal result = RawValueConverter.ToMoney(rawResult);

        return result;
      }
    }

    public bool IsPersonalEntertainmentIncluded
    {
      get
      {
        string rawResult = this.Flight["Personal Entertainment"].RawValue;
        return RawValueConverter.ToBoolean(rawResult);
      }
    }

    public bool IsInFlightWifiIncluded
    {
      get
      {
        string rawResult = this.Flight["In Flight Wifi"].RawValue;
        return RawValueConverter.ToBoolean(rawResult);
      }
    }

    public bool IsFoodServiceIncluded
    {
      get
      {
        string rawResult = this.Flight["Food Service"].RawValue;
        return RawValueConverter.ToBoolean(rawResult);
      }
    }
  }
}

