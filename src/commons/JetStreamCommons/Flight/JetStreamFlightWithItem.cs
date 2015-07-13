using JetStreamCommons.Airport;

namespace JetStreamCommons.Flight
{
  using System;
  using Sitecore.MobileSDK.API.Items;


  public class JetStreamFlightWithItem : IJetStreamFlightWithAirports
  {
    public JetStreamFlightWithItem(
      ISitecoreItem flight,
      IJetStreamAirportWithTimeZone departureAirport,
      IJetStreamAirportWithTimeZone arrivalAirport)
    {
      this.Flight = flight;
      this.DepartureAirport = departureAirport;
      this.ArrivalAirport = arrivalAirport;
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

    #region Timezone
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

    public bool IsRedEyeFlight
    {
      get
      {
        // TODO : add timezone computation

        return false;
      }
    }
    #endregion Timezone


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
  
  
    public IJetStreamAirportWithTimeZone DepartureAirport
    { 
      get;
      private set;
    }
    public IJetStreamAirportWithTimeZone ArrivalAirport
    { 
      get; 
      private set;
    }
  }
}

