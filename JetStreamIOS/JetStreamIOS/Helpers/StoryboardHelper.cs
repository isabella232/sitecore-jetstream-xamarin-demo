namespace JetStreamIOS.Helpers
{
  using System;
  using MonoTouch.UIKit;


  public static class StoryboardHelper
  {
    public static bool IsSegueToSourceAirportSearch(UIStoryboardSegue segue)
    {
      return "FromAirportQuickSearch".Equals(segue.Identifier);
    }

    public static bool IsSegueToDestinationAirportSearch(UIStoryboardSegue segue)
    {
      return "ToAirportQuickSearch".Equals(segue.Identifier);
    }

    public static bool IsSegueToDepartureFlightsSearch(UIStoryboardSegue segue)
    {
      return "ToDepartureFlightsList".Equals(segue.Identifier);
    }

    public static void NavigateToDepartureFlightsListFromViewController(UIViewController sourceViewController)
    {
      sourceViewController.PerformSegue("ToDepartureFlightsList", sourceViewController);
    }
  }
}

