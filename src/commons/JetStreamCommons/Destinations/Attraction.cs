
namespace JetStreamCommons.Destinations
{
  using Sitecore.MobileSDK.API.Items;


  public class Attraction : BaseSitecoreItemWrapper, IAttraction
  {
    private readonly string imagePath;

    public Attraction(ISitecoreItem wrapped) : base(wrapped)
    {
      this.imagePath = MediaPathExtractor.GetImagePathFromImageRawValue(wrapped["Image"].RawValue);
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
