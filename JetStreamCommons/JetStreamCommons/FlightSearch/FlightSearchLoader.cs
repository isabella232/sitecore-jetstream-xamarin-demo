namespace JetStreamCommons.FlightSearch
{
  using System;
  using System.Collections.Generic;

  using JetStreamCommons;
  using JetStreamCommons.Airport;


  public class FlightSearchLoader
  {
    public FlightSearchLoader(
      RestManager restManagerToConsume, 
      IJetStreamAirport sourceAirport,
      IJetStreamAirport destinationAirport,
      DateTime flightDate,
      int datePrecisionInDays
    )
    {
    }
  }
}

