namespace JetStreamCommons.Destionations
{
  using JetStreamCommons.Helpers;
  using Sitecore.MobileSDK.API.Items;

  public class Destionation : BaseSitecoreItemWrapper
  {
    public Destionation(ISitecoreItem wrapped) : base(wrapped)
    {
    }

    public string CountryName
    {
      get
      {
        return SitecoreItemUtil.ParseParentItemName(this.Wrapped.Path);
      }
    }
  }
}
