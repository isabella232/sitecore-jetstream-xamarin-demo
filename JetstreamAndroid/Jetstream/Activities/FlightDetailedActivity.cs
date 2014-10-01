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
      SetContentView(Resource.Layout.activity_webiew_teemplate);

      this.app = JetstreamApp.From(this);

      var userInput = this.app.FlightUserInput;

      var order = new JetStreamOrder(
        this.app.SelectedFlight, 
        null,
        userInput.DepartureAirport,
        userInput.DestinationAirport,
        1);

      var htmlBuilder = new AndroidFlightDetailsHtmlBuilder(this);
      var html = htmlBuilder.GetHtmlStringWithFlight(this.app.SelectedFlight, order);

      var webView = this.FindViewById<WebView>(Resource.Id.webView_flight_details);

      webView.Settings.JavaScriptEnabled = true;
      webView.LoadDataWithBaseURL("file:///android_asset/", html, "text/html", "UTF-8", null);
    }
  }
}