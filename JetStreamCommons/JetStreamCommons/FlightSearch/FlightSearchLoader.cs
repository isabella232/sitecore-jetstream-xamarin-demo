namespace JetStreamCommons.FlightSearch
{
  using System;
  using System.Linq;
  using System.Threading.Tasks;
  using System.Collections.Generic;

  using JetStreamCommons;
  using JetStreamCommons.Flight;
  using JetStreamCommons.Airport;


  public class FlightSearchLoader
  {
    private IFlightsLoader flightsDataSource;
    private IJetStreamAirport sourceAirport;
    private IJetStreamAirport destinationAirport;
    private DateTime flightDate;

    public FlightSearchLoader(
      IFlightsLoader flightsDataSource, 
      IJetStreamAirport sourceAirport,
      IJetStreamAirport destinationAirport,
      DateTime flightDate)
    {
      this.flightsDataSource = flightsDataSource;
      this.sourceAirport = sourceAirport;
      this.destinationAirport = destinationAirport;
      this.flightDate = flightDate;
    }

    public async Task< IEnumerable<IJetStreamFlight> > GetFlightsForTheGivenDateAsync()
    {
      DateTime today = this.flightDate.Date;

      SearchFlightsRequest request = new SearchFlightsRequest(
        this.sourceAirport.Id,
        this.destinationAirport.Id,
        today,
        null);

      IEnumerable<IJetStreamFlight> flights = await this.flightsDataSource.LoadOneWayFlightsAsync(request);
      return flights;
    }

    public async Task<DaySummary> GetPreviousDayAsync()
    {
      return await this.GetDayWithOffsetAsync(-1);
    }

    public async Task<DaySummary> GetNextDayAsync()
    {
      return await this.GetDayWithOffsetAsync(+1);
    }

    private async Task<DaySummary> GetDayWithOffsetAsync(int offset)
    {
      DateTime otherDay = this.flightDate.AddDays(offset).Date;

      SearchFlightsRequest request = new SearchFlightsRequest(
        this.sourceAirport.Id,
        this.destinationAirport.Id,
        otherDay,
        null);
      IEnumerable<IJetStreamFlight> flights = await this.flightsDataSource.LoadOneWayFlightsAsync(request);

      decimal? lowestPrice = null;
      try
      {
        lowestPrice = flights.Min(flight => flight.Price);
      }
      catch
      {
        // suppress missing element exception
      }

      return new DaySummary(otherDay, lowestPrice);
    }
  }
}

