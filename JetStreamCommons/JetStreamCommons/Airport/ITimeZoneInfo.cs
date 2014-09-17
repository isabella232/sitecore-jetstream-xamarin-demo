namespace JetStreamCommons.Airport
{
  using System;

  public interface ITimeZoneInfo
  {
    string Name { get; }
    string Id { get; }
    string Abbreviation { get; }
  }
}

