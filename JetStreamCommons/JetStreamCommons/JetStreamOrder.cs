namespace JetStreamCommons
{
  using JetStreamCommons.Flight;

  public class JetStreamOrder
  {
    public JetStreamOrder(IJetStreamFlight departFlight, IJetStreamFlight returnFlight)
    {
      this.DepartureFlight = departFlight;
      this.ReturnFlight = returnFlight;
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

  }
}

