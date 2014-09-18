namespace JetStreamCommons.Timezone
{
  using System;
  using Sitecore.MobileSDK.API.Items;


  public class TimeZoneInfoWithItem : ITimeZoneInfo
  {
    private ISitecoreItem underlyingItem;
    private ITimeZoneProvider timezoneProvider;

    public TimeZoneInfoWithItem(ISitecoreItem item, ITimeZoneProvider timezoneProvider)
    {
      this.underlyingItem = item;
      this.timezoneProvider = timezoneProvider;
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

    public TimeZoneInfo TimeZone 
    { 
      get
      {
        return this.timezoneProvider.FindSystemTimeZoneById(this.Id);
      }
    }
  }
}

