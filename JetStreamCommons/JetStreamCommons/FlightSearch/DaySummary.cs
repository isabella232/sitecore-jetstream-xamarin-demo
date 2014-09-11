namespace JetStreamCommons.FlightSearch
{
  using System;

  public class DaySummary
  {
    public DaySummary(DateTime flightDepartureDate, decimal minFlightPrice)
    {
      this.DepartureDate = flightDepartureDate;
      this.LowestPrice = minFlightPrice;
    }

    public DateTime DepartureDate { get; private set; }
    public decimal LowestPrice { get; private set; }
  }
}

