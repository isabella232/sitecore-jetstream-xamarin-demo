namespace JetStreamCommons.Airport
{
  using System;
  using Sitecore.MobileSDK.API.Items;


  public class TimeZoneInfoWithItem : ITimeZoneInfo
  {
    private ISitecoreItem underlyingItem;

    public TimeZoneInfoWithItem(ISitecoreItem item)
    {
      this.underlyingItem = item;
    }

    public string Name 
    { 
      get
      {
        return this.underlyingItem["Time Zone Name"].RawValue;
      }
    }

    public string Id 
    { 
      get
      {
        return this.underlyingItem["Time Zone ID"].RawValue;
      }
    }

    public string Abbreviation
    { 
      get
      {
        return this.underlyingItem["Abbreviation"].RawValue;
      }
    }
  }
}

