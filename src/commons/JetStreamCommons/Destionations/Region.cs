namespace JetStreamCommons.Destionations
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Items;

  public class Region : BaseSitecoreItemWrapper
  {
    public const string TemplateName = "Jetstream/Destinations/Region Item";

    public IEnumerable<Country> Countries { get; private set; }

    public Region(IEnumerable<Country> countries, ISitecoreItem wrapped)
      : base(wrapped)
    {
      this.Countries = countries;
    }
  }
}
