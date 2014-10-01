namespace JetstreamAndroid.Fragments
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Android.App;
  using Android.OS;
  using Android.Views;
  using Android.Widget;
  using JetstreamAndroid.Activities;
  using JetstreamAndroid.Adapters;
  using JetstreamAndroid.Utils;
  using JetStreamCommons.Flight;
  using JetStreamCommons.FlightSearch;

  public class FlightsListFragment : ListFragment
  {
    private const string FragmentDateKey = "fragment_date";

    private JetstreamApp app;
    private IEnumerable<IJetStreamFlight> flights;

    public static FlightsListFragment NewInstance(DateTime date)
    {
      var fragment = new FlightsListFragment();

      var arguments = new Bundle();
      arguments.PutString(FragmentDateKey, date.ToString());

      fragment.Arguments = arguments;

      return fragment;
    }

    public override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);
      RetainInstance = true;
    }

    public override void OnActivityCreated(Bundle savedInstanceState)
    {

      base.OnActivityCreated(savedInstanceState);
      app = JetstreamApp.From(Activity);

      DateTime dateTime = DateTime.Parse(Arguments.GetString(FragmentDateKey));
      var loader = this.app.FlightSearchLoaderForDate(dateTime);

      this.SetEmptyText("No tickets avalible");

      this.UpdateFligths(loader);
    }

    private async void UpdateFligths(FlightSearchLoader loader)
    {
      try
      {
        this.flights  = await loader.GetFlightsForTheGivenDateAsync();
        this.InitAndSetAdapter();
      }
      catch (System.Exception exception)
      {
        LogUtils.Error(typeof(BookFlightFragment), "Exception during searching tickets\n" + exception);
      }
    }

    public override void OnListItemClick(ListView l, View v, int position, long id)
    {
      var flight = this.flights.ToList()[position];
      this.app.DepartureFlight = flight;
      Activity.StartActivity(typeof(FlightDetailedActivity));
    }

    private void InitAndSetAdapter()
    {
      var flightsList = flights == null ? new List<IJetStreamFlight>() : new List<IJetStreamFlight>(this.flights);
      ListAdapter = new FlightsListAdapter(Activity, flightsList);
    }
  }
}