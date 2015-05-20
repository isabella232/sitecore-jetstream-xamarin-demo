namespace JetstreamAndroid.Activities
{
  using Android.App;
  using Android.Content;
  using Android.Content.PM;
  using Android.OS;
  using Android.Views;
  using Android.Webkit;
  using Android.Widget;
  using JetstreamAndroid.Utils;
  using JetStreamCommons;

  [Activity(Label = "FlightDetailedActivity", ScreenOrientation = ScreenOrientation.Portrait)]
  public class FlightDetailedActivity : Activity
  {
    public const string IsDepartFlight = "flight_tag";

    private JetstreamApp app;
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      SetContentView(Resource.Layout.activity_webiew_teemplate);
      ActionBar.SetDisplayHomeAsUpEnabled(true);

      var isDepartFlight = Intent.Extras.GetBoolean(IsDepartFlight);
      this.app = JetstreamApp.From(this);

      var userInput = this.app.FlightUserInput;

      var order = new JetStreamOrder(
        this.app.SelectedFlight, 
        null,
        userInput.DepartureAirport,
        userInput.DestinationAirport,
        userInput.TicketsCount);

      var htmlBuilder = new AndroidFlightDetailsHtmlBuilder(this);
      var html = htmlBuilder.GetHtmlStringWithFlight(this.app.SelectedFlight, order);

      var button = this.FindViewById<Button>(Resource.Id.button_template);
      button.Text = GetString(Resource.String.text_button_order);
      button.Click += (sender, args) =>
      {
        if (this.app.FlightUserInput.IsRoundTrip)
        {
          if (isDepartFlight)
          {
            this.app.DepartureFlight = this.app.SelectedFlight;
            this.app.SelectedFlight = null;

            StartActivity(typeof(ReturnFlightsActivity));
          }
          else
          {
            this.app.ReturnFlight = this.app.SelectedFlight;
            this.app.SelectedFlight = null;

            StartActivity(typeof(SummaryActivity));
          }
        }
        else
        {
          this.app.DepartureFlight = this.app.SelectedFlight;
          this.app.SelectedFlight = null;

          StartActivity(typeof(SummaryActivity));
        }
      };
      
      var webView = this.FindViewById<WebView>(Resource.Id.webView_flight_details);
      webView.Settings.JavaScriptEnabled = true;
      webView.LoadDataWithBaseURL("file:///android_asset/", html, "text/html", "UTF-8", null);
    }

    public override bool OnOptionsItemSelected(IMenuItem item)
    {
      switch (item.ItemId)
      {
        case Android.Resource.Id.Home:
          this.Finish();
          return true;
      }
      return base.OnOptionsItemSelected(item);
    }
  }
}