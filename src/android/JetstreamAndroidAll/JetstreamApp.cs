namespace JetstreamAndroid
{
  using System;
  using Android.App;
  using Android.Content;
  using Java.Util;
  using JetStreamCommons;
  using JetStreamCommons.Flight;
  using Android.Content.Res;
  using JetStreamCommons.FlightSearch;
  using JetstreamAndroid.Utils;

  [Application(Theme = "@android:style/Theme.Holo.Light")]
  public class JetstreamApp : Application
  {
    private IFlightsLoader restManager;

    public JetstreamApp(System.IntPtr javaReference, Android.Runtime.JniHandleOwnership transfer)
      : base(javaReference, transfer)
    {
    }

    public override void OnCreate()
    {
      base.OnCreate();

      var locale = new Locale("en");
      Locale.Default = locale;

      var config = new Configuration
      {
        Locale = locale
      };
      Resources.UpdateConfiguration(config, Resources.DisplayMetrics);
    }

    public static JetstreamApp From(Context context)
    {
      return context.ApplicationContext as JetstreamApp;
    }

    public IFlightSearchUserInput FlightUserInput { get; set; }

    public IJetStreamFlight DepartureFlight { get; set; }
    public IJetStreamFlight ReturnFlight { get; set; }
    public IJetStreamFlight SelectedFlight { get; set; }

    public FlightSearchLoader FlightSearchLoaderForDate(DateTime date)
    {
      if (this.restManager == null)
      {
        this.restManager = new RestManager(Prefs.From(this).Session, new TimezoneProviderForAndroid());
      }

      var loader = new FlightSearchLoader(this.restManager,
         this.FlightUserInput.DepartureAirport,
         this.FlightUserInput.DestinationAirport,
         date);

      return loader;
    }
  }
}