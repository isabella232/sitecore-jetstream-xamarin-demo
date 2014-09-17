namespace JetStreamCommons.FlightFilter
{
  using System;
  using System.Linq;
  using System.Collections.Generic;
  using System.Diagnostics;

  using JetStreamCommons.Flight;


  public class FlightFilter
  {
    private IFlightFilterUserInput filterUserInput;

    public FlightFilter(IFlightFilterUserInput settings)
    {
      this.filterUserInput = settings;
    }

    public bool IsFlightMatchingPredicate(IJetStreamFlight singleFlight)
    {
      if (this.filterUserInput.IsFoodServiceIncluded)
      {
        if (!singleFlight.IsFoodServiceIncluded)
        {
          return false;
        }
      }
      else if (this.filterUserInput.IsRedEyeFlight)
      {
        if (!singleFlight.IsRedEyeFlight)
        {
          return false;
        }
      }
      else if (this.filterUserInput.IsInFlightWifiIncluded)
      {
        if (!singleFlight.IsInFlightWifiIncluded)
        {
          return false;
        }
      }
      else if (this.filterUserInput.IsPersonalEntertainmentIncluded)
      {
        if (!singleFlight.IsPersonalEntertainmentIncluded)
        {
          return false;
        }
      }


      bool isPriceNotExceedsLimits = (singleFlight.Price <= this.filterUserInput.MaxPrice);
      if (!isPriceNotExceedsLimits)
      {
        return false;
      }

      bool isFlightDurationNotExceedsLimits = ( 1 != TimeSpan.Compare(singleFlight.Duration, this.filterUserInput.MaxDuration) );
      if (!isFlightDurationNotExceedsLimits)
      {
        return false;
      }

      DateTime flightTime = singleFlight.DepartureTime;
      DateTime minTime = this.filterUserInput.EarliestDepartureTime;
      DateTime maxTime = this.filterUserInput.LatestDepartureTime;

      int lowerLimitResult = DateTime.Compare(minTime, flightTime);
      int upperLimitResult = DateTime.Compare(flightTime, maxTime);
      bool isFlightTimeBetweenLimits = 
      (
        (1 != lowerLimitResult) && 
        (1 != upperLimitResult)
      );
      if (!isFlightTimeBetweenLimits)
      {
        return false;
      }
        
      return true;
    }

    public IEnumerable<IJetStreamFlight> FilterFlights(IEnumerable<IJetStreamFlight> allFlights)
    {
      if (null == this.filterUserInput)
      {
        return allFlights;
      }

      return allFlights.Where( singleFlight => this.IsFlightMatchingPredicate(singleFlight) );
    }

  }
}

