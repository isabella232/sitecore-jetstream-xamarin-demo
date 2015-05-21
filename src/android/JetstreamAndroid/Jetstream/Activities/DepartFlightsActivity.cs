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
      return this.UserInput.ForwardFlightDepartureDate;
    }

    public override void OnBackPressed()
    {
      this.App.DepartureFlight = null;
      base.OnBackPressed();
    }

    public override void OnFlightOrderSelected(IJetStreamFlight flight)
    {
      this.App.DepartureFlight = flight;
      this.StartActivity(this.UserInput.IsRoundTrip ? typeof(ReturnFlightsActivity) : typeof(SummaryActivity));
    }
  }
}