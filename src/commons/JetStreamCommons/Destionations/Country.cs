namespace JetStreamCommons.Destionations
{
  using System.Collections.Generic;
  using JetStreamCommons.Helpers;
  using Sitecore.MobileSDK.API.Items;

  public class Country : BaseSitecoreItemWrapper
  {
    public const string TemplateName = "Jetstream/Destinations/Country Item";

    public IEnumerable<Destionation> Destionations { get; private set; }

    public Country(IEnumerable<Destionation> destionations, ISitecoreItem wrapped)
      : base(wrapped)
    {
      this.Destionations = destionations;
    }

    public string RegionName
    {
      get
      {
        return SitecoreItemUtil.ParseParentItemName(this.Wrapped.Path);
      }
    }
  }
}
