

namespace JetStreamCommons.FlightSearch
{
  using System;
  using System.Threading.Tasks;
  using System.Collections.Generic;

  using JetStreamCommons;
  using JetStreamCommons.Flight;
  using JetStreamCommons.Airport;


  public class FlightSearchLoader
  {
    public FlightSearchLoader(
      RestManager restManagerToConsume, 
      IJetStreamAirport sourceAirport,
      IJetStreamAirport destinationAirport,
      DateTime flightDate,
      int datePrecisionInDays)
    {
    }

    public async Task< IEnumerable<IJetStreamFlight> > GetFlightsForTheGivenDateAsync()
    {
      return null;
    }

    public async Task<DaySummary> GetPreviousDayAsync()
    {
      return null;
    }

    public async Task<DaySummary> GetNextDayAsync()
    {
      return null;
    }
  }
}

