namespace JetstreamAndroid.Activities
{
  using System;
  using Android.App;
  using Android.Content.PM;
  using JetStreamCommons.Flight;

  [Activity(Label = "Return", ScreenOrientation = ScreenOrientation.Portrait)]
  public class ReturnFlightsActivity : BaseFlightsActvity
  {
    public override DateTime GetDateTime()
    {
      return (DateTime)this.userInput.ReturnFlightDepartureDate;
    }

    public override void OnFlightOrderSelected(IJetStreamFlight flight)
    {
      this.app.ReturnFlight = flight;
      this.StartActivity(typeof(SummaryActivity));
    }

    public override void OnBackPressed()
    {
      this.app.ReturnFlight = null;
      base.OnBackPressed();
    }
  }
}