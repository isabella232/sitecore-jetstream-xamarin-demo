using System;
using JetStreamCommons.Airport;

namespace JetStreamUnitTests
{
  public class AirportPOD : IJetStreamAirport
  {
    private string privateName;
    private string privateCity;

    public AirportPOD(string name, string city)
    {
      this.privateName = name;
      this.privateCity = city;
    }

    public string Id 
    { 
      get
      {
        return null;
      }
    }

    public string DisplayName 
    {
      get
      {
        return this.privateName;
      }
    }

    public string City 
    { 
      get
      {
        return  this.privateCity;
      }
    }

    public string Country { get; set; }

    public string Name
    {
      get
      {
        return this.privateName;
      }
    }

    public string Code
    {
      get;
      set;
    }
  }
}

