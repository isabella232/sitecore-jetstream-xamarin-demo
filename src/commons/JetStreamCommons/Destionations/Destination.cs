using System.Globalization;

namespace JetStreamCommons.Destinations
{
  using JetStreamCommons.Helpers;
  using Sitecore.MobileSDK.API.Items;

  public class Destination : BaseSitecoreItemWrapper, IDestination
  {
    private float latitude;
    private float longitude;
    private string countryName;
    private string imagePath;

    public Destination(ISitecoreItem wrapped) : base(wrapped)
    {
      this.countryName = SitecoreItemUtil.ParseParentItemName(wrapped.Path);

      float latitude;
      float.TryParse(wrapped["Latitude"].RawValue, NumberStyles.Any, new CultureInfo("en-US"), out latitude);
      this.latitude = latitude;

      float longitude;
      float.TryParse(wrapped["Longitude"].RawValue, NumberStyles.Any, new CultureInfo("en-US"), out longitude);
      this.longitude = longitude;

      this.imagePath = MediaPathExtractor.GetImagePathFromImageRawValue(wrapped["Image"].RawValue);
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
