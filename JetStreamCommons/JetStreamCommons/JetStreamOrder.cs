using System;
using Sitecore.MobileSDK.Items;
namespace JetStreamCommons
{
  public class JetStreamOrder
  {
    public JetStreamOrder(JetStreamFlight departFlight, JetStreamFlight returnFlight)
    {
      this.DepartFlight = departFlight;
      this.ReturnFlight = returnFlight;
    }

    private JetStreamFlight DepartFlight
    {
      get;
      set;
    }

    private JetStreamFlight ReturnFlight
    {
      get;
      set;
    }

  }
}

