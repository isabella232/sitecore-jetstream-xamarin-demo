namespace JetStreamCommons.Destinations
{
  public interface IDestination
  {
    string CountryName
    {
      get;
    }

    string Id
    {
      get;
    }

    string Overview
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

    string ImagePath
    {
      get;
    }

    bool CoordinatesIsAvailable
    {
      get;
    }

    string DisplayName { get; }
  }
}

