namespace JetstreamAndroid
{
  using System;
  using Android.App;
  using Android.Content;
  using JetStreamCommons;
  using JetStreamCommons.Flight;
  using JetStreamCommons.FlightSearch;

  [Application(Theme = "@android:style/Theme.Holo.Light")]
  public class JetstreamApp : Application
  {
    private IFlightsLoader restManager;

    public JetstreamApp(System.IntPtr javaReference, Android.Runtime.JniHandleOwnership transfer) : base(javaReference, transfer)
    {
    }

    public static JetstreamApp From(Context context)
    {
      return context.ApplicationContext as JetstreamApp;
    }

    public IFlightSearchUserInput FlightUserInput { get; set; }
    public IJetStreamFlight SelectedFlight { get; set; }

    public FlightSearchLoader FlightSearchLoaderForDate(DateTime date)
    {
      if (this.restManager == null)
      {
        this.restManager = new RestManager(Prefs.From(this).Session);
      }

      var loader = new FlightSearchLoader(this.restManager,
         this.FlightUserInput.DepartureAirport,
         this.FlightUserInput.DestinationAirport,
         date);

      return loader;
    }
  }
}