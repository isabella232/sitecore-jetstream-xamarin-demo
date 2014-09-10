namespace JetStreamCommons.Airport
{
  using System;
  using Sitecore.MobileSDK.API.Items;


  public class JetStreamAirportWithItem : IJetStreamAirport
  {
    private ISitecoreItem item;

    public JetStreamAirportWithItem(ISitecoreItem item)
    {
      this.item = item;
    }

    public string City 
    { 
      get
      {
        return this.item["City"].RawValue;
      }
    }

    public string Name
    { 
      get
      {
        return this.item["Airport Name"].RawValue;
      }
    }
  }
}

