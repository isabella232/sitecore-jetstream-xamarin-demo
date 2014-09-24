namespace JetstreamAndroid.Models
{
  using System.Collections.Generic;
  using JetStreamCommons.Flight;
  using JetStreamCommons.FlightSearch;

  public class FlightsContainer
  {
    public IEnumerable<IJetStreamFlight> Flights { get; set; }
    public DaySummary YesterdaySummary { get; set; }
    public DaySummary TomorrowSummary { get; set; }

    public IFlightSearchUserInput FlightSearchUserInput { get; set; }
  }
}