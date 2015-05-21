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
    public DateTime FlightDate { get; set; }

    public FlightSearchLoader(
      IFlightsLoader flightsDataSource,
      IJetStreamAirport sourceAirport,
      IJetStreamAirport destinationAirport,
      DateTime flightDate)
    {
      this.flightsDataSource = flightsDataSource;
      this.sourceAirport = sourceAirport;
      this.destinationAirport = destinationAirport;
      this.FlightDate = flightDate;
    }

    public async Task<IEnumerable<IJetStreamFlight>> GetFlightsForTheGivenDateAsync()
    {
      DateTime today = this.FlightDate.Date;

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

    public async Task<DaySummary> GetCurrentDayAsync()
    {
      return await this.GetDayWithOffsetAsync(0);
    }

    private async Task<DaySummary> GetDayWithOffsetAsync(int offset)
    {
      DateTime otherDay = this.FlightDate.AddDays(offset).Date;

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
        // @adk : suppress missing element exception
      }

      return new DaySummary(otherDay, lowestPrice);
    }
  }
}

