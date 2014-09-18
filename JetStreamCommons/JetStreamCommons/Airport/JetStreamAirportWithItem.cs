namespace JetStreamCommons.Airport
{
  using System;
  using Sitecore.MobileSDK.API.Items;
  using JetStreamCommons.Timezone;


  public class JetStreamAirportWithItem : IJetStreamAirportWithTimeZone
  {
    private ISitecoreItem item;

    public JetStreamAirportWithItem(ISitecoreItem item, ITimeZoneInfo timeZone)
    {
      this.item = item;
      this.TimeZone = timeZone;
    }

    public string Country 
    { 
      get
      {
        return this.item["Country"].RawValue;
      }
    }

    public string City 
    { 
      get
      {
        return this.item["City"].RawValue;
      }
    }

    public string Code 
    { 
      get
      {
        return this.item["Airport Code"].RawValue;
      }
    }

    public string Name
    { 
      get
      {
        return this.item["Airport Name"].RawValue;
      }
    }

    public string TimeZoneId 
    { 
      get
      {
        return this.item["Time Zone"].RawValue;
      }
    }

    public string Id 
    { 
      get
      {
        return this.item.Id;
      }
    }

    public string DisplayName 
    { 
      get
      {
        return this.item.DisplayName;
      }
    }

    public ITimeZoneInfo TimeZone
    { 
      get; 
      private set;
    }
  }
}

