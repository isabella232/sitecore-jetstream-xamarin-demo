using JetStreamCommons.Airport;

namespace JetStreamCommons
{
  using JetStreamCommons.Flight;

  public class JetStreamOrder
  {
    public JetStreamOrder(
      IJetStreamFlight departFlight, 
      IJetStreamFlight returnFlight,
      IJetStreamAirport departureAirport, 
      IJetStreamAirport returnAirport, 
      int ticketsCount
    )
    {
      this.DepartureFlight = departFlight;
      this.ReturnFlight = returnFlight;
      this.DepartureAirport = departureAirport;
      this.DestinationAirport = returnAirport;
      this.TicketsCount = ticketsCount;
    }

    public IJetStreamFlight DepartureFlight
    {
      get;
      private set;
    }

    public IJetStreamFlight ReturnFlight
    {
      get;
      private set;
    }

    public IJetStreamAirport DepartureAirport
    {
      get;
      private set;
    }

    public IJetStreamAirport DestinationAirport
    {
      get;
      private set;
    }

    public int TicketsCount
    {
      get;
      private set;
    }
  }
}

