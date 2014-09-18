namespace JetStreamCommons.Airport
{
  using System;
  using JetStreamCommons.Timezone;

  public interface IJetStreamAirportWithTimeZone : IJetStreamAirport
  {
    ITimeZoneInfo TimeZone { get; }
  }
}

