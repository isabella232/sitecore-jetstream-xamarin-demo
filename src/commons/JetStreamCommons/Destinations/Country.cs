namespace JetStreamCommons.Destinations
{
  using System.Collections.Generic;
  using JetStreamCommons.Helpers;
  using Sitecore.MobileSDK.API.Items;

  public class Country : BaseSitecoreItemWrapper
  {
    public const string TemplateName = "Jetstream/Destinations/Country Item";

    public IEnumerable<IDestination> Destinations { get; private set; }

    public Country(IEnumerable<IDestination> destinations, ISitecoreItem wrapped)
      : base(wrapped)
    {
      this.Destinations = destinations;
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
