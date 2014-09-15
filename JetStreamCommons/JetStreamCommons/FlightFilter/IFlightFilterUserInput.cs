namespace JetStreamCommons
{
  using System;

  public interface IFlightFilterUserInput
  {
    decimal MaxPrice { get; }
    DateTime EarliestDepartureTime { get; }
    DateTime LatestArrivalTime { get; }
    TimeSpan MaxDuration {get;}

    bool IsRedEyeFlight {get;}
    bool IsInFlightWifiIncluded {get;}
    bool IsPersonalEntertainmentIncluded {get;}
    bool IsFoodServiceIncluded {get;}
  }
}

