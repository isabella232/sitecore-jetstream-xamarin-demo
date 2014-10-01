namespace JetstreamAndroid.Activities
{
  using Android.App;
  using Android.Content.PM;
  using Android.OS;
  using Android.Views;
  using Android.Webkit;
  using Android.Widget;
  using JetstreamAndroid.Utils;
  using JetStreamCommons;

  [Activity(Label = "SummaryActivity", ScreenOrientation = ScreenOrientation.Portrait)]
  public class SummaryActivity : Activity
  {
    private JetstreamApp app;
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      SetContentView(Resource.Layout.activity_webiew_teemplate);
      ActionBar.SetDisplayHomeAsUpEnabled(true);

      this.app = JetstreamApp.From(this);

      var userInput = this.app.FlightUserInput;

      var departureFlight = this.app.DepartureFlight;
      var returnFlight = this.app.ReturnFlight;

      var order = new JetStreamOrder(
        departureFlight,
        returnFlight,
        userInput.DepartureAirport,
        userInput.DestinationAirport,
        userInput.TicketsCount);

      var htmlBuilder = new AndroidOrderSummaryHtmlBuilder(this);
      var html = htmlBuilder.GetHtmlStringWithOrder(order);

      var button = this.FindViewById<Button>(Resource.Id.button_template);
      button.Text = "Purchase";
      button.Click += (sender, args) => Toast.MakeText(this, "Tickets purchased", ToastLength.Long).Show();

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