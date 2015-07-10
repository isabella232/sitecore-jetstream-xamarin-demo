namespace JetStreamCommons.Destinations
{
  using Sitecore.MobileSDK;

  public interface IItemWithImage
  {
    string ImageUrl(ScApiSession session);

    string ImagePath { get; }
  }
}
