namespace JetStreamCommons.FlightFilter
{
  using System;

  public class MutableFlightsFilterSettings : IFlightFilterUserInput
  {
    public MutableFlightsFilterSettings()
    {
    }

    public MutableFlightsFilterSettings(IFlightFilterUserInput other)
    {
      this.MaxPrice = other.MaxPrice;
      this.EarliestDepartureTime = other.EarliestDepartureTime;
      this.LatestDepartureTime = other.LatestDepartureTime;
      this.MaxDuration = other.MaxDuration;
      this.IsRedEyeFlight = other.IsRedEyeFlight;
      this.IsInFlightWifiIncluded = other.IsInFlightWifiIncluded;
      this.IsPersonalEntertainmentIncluded = other.IsPersonalEntertainmentIncluded;
      this.IsFoodServiceIncluded = other.IsFoodServiceIncluded;
    }

    public decimal MaxPrice { get; set; }
    public DateTime EarliestDepartureTime { get; set; }
    public DateTime LatestDepartureTime { get; set; }
    public TimeSpan MaxDuration { get; set; }

    public bool IsRedEyeFlight { get; set; }
    public bool IsInFlightWifiIncluded { get; set; }
    public bool IsPersonalEntertainmentIncluded { get; set; }
    public bool IsFoodServiceIncluded { get; set; }
  }
}

