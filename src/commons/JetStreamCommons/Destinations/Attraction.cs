namespace JetStreamCommons.Destinations
{
  using Sitecore.MobileSDK.API.Items;

  public class Attraction : ItemWithImage, IAttraction
  {
    public Attraction(ISitecoreItem wrapped) : base(wrapped)
    {
    }
  }
}
