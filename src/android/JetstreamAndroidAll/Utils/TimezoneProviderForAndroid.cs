namespace JetstreamAndroid.Utils
{
  using System;
  using JetStreamCommons.Timezone;

  public class TimezoneProviderForAndroid : ITimeZoneProvider
  {
    public TimeZoneInfo FindSystemTimeZoneById(string timeZoneId)
    {
      return TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
    }
  }
}