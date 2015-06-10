
namespace JetStreamCommons.Destinations
{
  using System.Globalization;
  using JetStreamCommons.Helpers;
  using Sitecore.MobileSDK.API.Items;

  public class Destination : BaseSitecoreItemWrapper, IDestination
  {
    private readonly float latitude;
    private readonly float longitude;
    private readonly string countryName;
    private readonly string imagePath;
    private readonly string overview;

    public Destination(ISitecoreItem wrapped) : base(wrapped)
    {
      
      this.countryName = SitecoreItemUtil.ParseParentItemName(wrapped.Path);

      float latitude;
      float.TryParse(wrapped["Latitude"].RawValue, NumberStyles.Any, new CultureInfo("en-US"), out latitude);
      this.latitude = latitude;

      float longitude;
      float.TryParse(wrapped["Longitude"].RawValue, NumberStyles.Any, new CultureInfo("en-US"), out longitude);
      this.longitude = longitude;

      this.overview = wrapped["Overview"].RawValue;

      this.imagePath = MediaPathExtractor.GetImagePathFromImageRawValue(wrapped["Image"].RawValue);
    }

    public bool CoordinatesIsAvailable
    {
      get
      { 
        return (this.Latitude != 0.0) && (this.Longitude != 0.0); 
      }
    }

    public string Overview
    {
      get
      {
        return this.overview;
      }
    }

    public string CountryName
    {
      get
      {
        return this.countryName;
      }
    }

    public float Latitude
    {
      get
      { 
        return this.latitude;
      }
    }

    public float Longitude
    {
      get
      { 
        return this.longitude;
      }
    }

    public string ImagePath
    {
      get
      { 
        return this.imagePath;
      }
    }
  }
}
