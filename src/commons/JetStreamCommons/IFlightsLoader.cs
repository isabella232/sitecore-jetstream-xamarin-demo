namespace JetStreamCommons
{
  using System;
  using System.Threading.Tasks;
  using JetStreamCommons.Flight;
  using System.Collections.Generic;


  public interface IFlightsLoader
  {
    Task< IEnumerable<IJetStreamFlight> > LoadOneWayFlightsAsync(SearchFlightsRequest request);
  }
}

