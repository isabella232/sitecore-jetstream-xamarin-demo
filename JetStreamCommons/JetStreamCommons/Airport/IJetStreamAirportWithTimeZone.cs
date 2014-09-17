namespace JetStreamCommons.Airport
{
  using System;

  public interface IJetStreamAirportWithTimeZone : IJetStreamAirport
  {
    ITimeZoneInfo TimeZone { get; }
  }
}

