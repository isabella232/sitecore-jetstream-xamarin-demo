namespace JetStreamCommons.Destinations
{
  public interface IDestination : IItemWithImage
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

    bool IsCoordinatesAvailable
    {
      get;
    }

    string DisplayName { get; }
  }
}

