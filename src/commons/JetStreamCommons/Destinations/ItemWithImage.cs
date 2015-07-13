namespace JetStreamCommons.Destinations
{
  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.API.Items;

  public class ItemWithImage : BaseSitecoreItemWrapper, IItemWithImage
  {
    public ItemWithImage(ISitecoreItem wrapped)
      : base(wrapped)
    {
    }

    public string ImageUrl(ScApiSession session)
    {
      return session.MediaDownloadUrl(this.ImagePath);
    }

    public string ImagePath
    {
      get
      {
        return MediaPathExtractor.GetImagePathFromImageRawValue(this.Wrapped["Image"].RawValue);
      }
    }
  }
}
