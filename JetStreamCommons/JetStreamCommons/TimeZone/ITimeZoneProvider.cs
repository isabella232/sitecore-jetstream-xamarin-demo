namespace JetStreamCommons.Airport
{
  using System;

  public interface ITimeZoneProvider
  {
    TimeZoneInfo FindSystemTimeZoneById(string timeZoneId);
  }
}

