namespace JetstreamAndroid.Activities
{
  using Android.App;
  using Android.Content.PM;
  using Android.OS;
  using Android.Webkit;
  using JetstreamAndroid.Utils;
  using JetStreamCommons;

  [Activity(Label = "FlightDetailedActivity", ScreenOrientation = ScreenOrientation.Portrait)]
  public class FlightDetailedActivity : Activity
  {
    private JetstreamApp app;
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      SetContentView(Resource.Layout.activity_flight_details);

      this.app = JetstreamApp.From(this);

      var container = this.app.FlightsContainer;

      var order = new JetStreamOrder(
        this.app.SelectedFlight, 
        null,
        container.FlightSearchUserInput.DepartureAirport,
        container.FlightSearchUserInput.DestinationAirport,
        1);

      var htmlBuilder = new AndroidFlightDetailsHtmlBuilder(this);

      var webView = this.FindViewById<WebView>(Resource.Id.webView_flight_details);

      var html = htmlBuilder.GetHtmlStringWithFlight(this.app.SelectedFlight, order);

      webView.Settings.JavaScriptEnabled = true;
      webView.LoadDataWithBaseURL("file:///android_asset/", html, "text/html", "UTF-8", null);
    }
  }
}