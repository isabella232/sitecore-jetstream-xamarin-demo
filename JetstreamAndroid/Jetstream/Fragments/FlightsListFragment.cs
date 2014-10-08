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
  using JetStreamCommons.FlightFilter;
  using JetStreamCommons.FlightSearch;

  public class FlightsListFragment : ListFragment
  {
    private const string FragmentDateKey = "fragment_date";

    private JetstreamApp app;
    private IEnumerable<IJetStreamFlight> allFlights;
    private IEnumerable<IJetStreamFlight> filteredFlights;

    private FlightsListAdapter.IFlightOrderSelectedListener flightOrderSelectedListener;
    private FilterFragment.IFilterActionListener filterActionListener;

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
      this.filterActionListener = (FilterFragment.IFilterActionListener)activity;
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
        this.allFlights = await loader.GetFlightsForTheGivenDateAsync();
        this.FilterResults();
        this.InitAndSetAdapter();
      }
      catch (System.Exception exception)
      {
        LogUtils.Error(typeof(BookFlightFragment), "Exception during searching tickets\n" + exception);
      }
    }

    private void FilterResults()
    {
      if (this.allFlights == null || this.filterActionListener.GetFilter() == null)
      {
        this.filteredFlights = this.allFlights;
      }
      else
      {
        var filter = new FlightFilter(this.filterActionListener.GetFilter());
        this.filteredFlights = filter.FilterFlights(this.allFlights);
      }
    }

    public void PerformFiltering()
    {
      this.FilterResults();
      this.InitAndSetAdapter();
    }

    public override void OnListItemClick(ListView l, View v, int position, long id)
    {
      var flight = this.allFlights.ToList()[position];
      this.app.SelectedFlight = flight;

      var intent = new Intent(Activity, typeof(FlightDetailedActivity));
      intent.PutExtra(FlightDetailedActivity.IsDepartFlight, Activity.GetType() == typeof(DepartFlightsActivity));

      StartActivity(intent);
    }

    public ExtendedFlightsFilterSettings CreateDefaultFilter()
    {
      return new ExtendedFlightsFilterSettings()
      {
        MaxPrice = Convert.ToDecimal(this.GetMaxPrice()),
        MaxAvaliblePrice = Convert.ToDecimal(this.GetMaxPrice()),
      };
    }

    private decimal GetMaxPrice()
    {
      if (this.allFlights != null && this.allFlights.Count() > 0)
      {
        return this.allFlights.Max(singleFlight => singleFlight.Price);
      }

      return 10000;
    }

    private void InitAndSetAdapter()
    {
      var flightsList = this.filteredFlights == null ? new List<IJetStreamFlight>() : new List<IJetStreamFlight>(this.filteredFlights);
      ListAdapter = new FlightsListAdapter(Activity, flightsList, this.flightOrderSelectedListener);
    }
  }
}