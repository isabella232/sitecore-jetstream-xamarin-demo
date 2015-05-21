namespace JetStreamCommons.Timezone
{
  using System;

  public interface ITimeZoneProvider
  {
    TimeZoneInfo FindSystemTimeZoneById(string timeZoneId);
  }
}

