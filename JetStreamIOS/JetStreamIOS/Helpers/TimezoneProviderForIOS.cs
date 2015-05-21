namespace JetStreamIOS.Helpers
{
  using System;
  using JetStreamCommons.Timezone;


  public class TimezoneProviderForIOS : ITimeZoneProvider
  {
    public TimeZoneInfo FindSystemTimeZoneById(string timeZoneId)
    {
      return TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
    }
  }
}

