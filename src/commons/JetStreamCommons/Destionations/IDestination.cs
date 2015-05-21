using System;

namespace JetStreamCommons.Destinations
{
  public interface IDestination
  {
    string CountryName
    {
      get;
    }

    float Latitude
    {
      get;
    }

    float Longitude
    {
      get;
    }
  }
}

