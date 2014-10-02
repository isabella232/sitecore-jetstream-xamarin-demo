namespace JetstreamAndroid.Activities
{
  using System;
  using Android.App;
  using Android.Content.PM;
  using JetStreamCommons.Flight;

  [Activity(Label = "Depart", ScreenOrientation = ScreenOrientation.Portrait)]
  public class DepartFlightsActivity : BaseFlightsActvity
  {
    public override DateTime GetDateTime()
    {
      return this.userInput.ForwardFlightDepartureDate;
    }

    public override void OnBackPressed()
    {
      this.app.DepartureFlight = null;
      base.OnBackPressed();
    }

    public override void OnFlightOrderSelected(IJetStreamFlight flight)
    {
      this.app.DepartureFlight = flight;
      this.StartActivity(this.userInput.IsRoundTrip ? typeof(ReturnFlightsActivity) : typeof(SummaryActivity));
    }
  }
}