namespace JetstreamAndroid.Fragments
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Android.App;
  using Android.Content;
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

    private FlightsListAdapter.IFlightOrderSelectedListener flightOrderSelectedListener;

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

    public override void OnAttach(Activity activity)
    {
      base.OnAttach(activity);
      this.flightOrderSelectedListener = (FlightsListAdapter.IFlightOrderSelectedListener)activity;
    }

    public override void OnActivityCreated(Bundle savedInstanceState)
    {

      base.OnActivityCreated(savedInstanceState);
      this.app = JetstreamApp.From(Activity);

      var dateString = Arguments.GetString(FragmentDateKey);

      var date = DateTime.Parse(dateString);
      var loader = this.app.FlightSearchLoaderForDate(date);

      this.SetEmptyText("No tickets avalible");

      this.UpdateFligths(loader);
    }

    private async void UpdateFligths(FlightSearchLoader loader)
    {
      try
      {
        this.flights = await loader.GetFlightsForTheGivenDateAsync();
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
      this.app.SelectedFlight = flight;

      var intent = new Intent(Activity, typeof(FlightDetailedActivity));
      intent.PutExtra(FlightDetailedActivity.IsDepartFlight, Activity.GetType() == typeof(DepartFlightsActivity));

      StartActivity(intent);
    }

    private void InitAndSetAdapter()
    {
      var flightsList = flights == null ? new List<IJetStreamFlight>() : new List<IJetStreamFlight>(this.flights);
      ListAdapter = new FlightsListAdapter(Activity, flightsList, this.flightOrderSelectedListener);
    }
  }
}