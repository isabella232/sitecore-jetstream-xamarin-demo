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

    private DateTime date;

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

      this.date = DateTime.Parse(dateString);
      var loader = this.app.FlightSearchLoaderForDate(date);

      this.SetEmptyText(this.GetString(Resource.String.text_no_tickets));
      this.UpdateFligths(loader);
    }

    public override void OnListItemClick(ListView l, View v, int position, long id)
    {
      var flight = this.allFlights.ToList()[position];
      this.app.SelectedFlight = flight;

      var intent = new Intent(this.Activity, typeof(FlightDetailedActivity));
      intent.PutExtra(FlightDetailedActivity.IsDepartFlight, this.Activity.GetType() == typeof(DepartFlightsActivity));

      this.StartActivity(intent);
    }

    private async void UpdateFligths(FlightSearchLoader loader)
    {
      if (this.allFlights != null)
      {
        this.PerformFiltering();
        return;
      }

      try
      {
        this.allFlights = await loader.GetFlightsForTheGivenDateAsync();
        this.FilterResults();
      }
      catch (System.Exception exception)
      {
        LogUtils.Error(typeof(BookFlightFragment), "Exception during searching tickets\n" + exception);
      }
      this.UpdateAdapterFligths();
    }

    private void FilterResults()
    {
      if (this.allFlights == null || this.filterActionListener.GetFilterInput() == null)
      {
        this.filteredFlights = this.allFlights;
      }
      else
      {
        var filterInput = this.filterActionListener.GetFilterInput();

        var newEarliest = new DateTime(
          this.date.Year,
          this.date.Month,
          this.date.Day,
          filterInput.EarliestDepartureTime.Hour,
          filterInput.EarliestDepartureTime.Minute, 0);


        var newLatest = new DateTime(
          this.date.Year,
          this.date.Month,
          this.date.Day,
          filterInput.LatestDepartureTime.Hour,
          filterInput.LatestDepartureTime.Minute, 0);

        var newFilter = new ExtendedFlightsFilterSettings(filterInput)
        {
          EarliestDepartureTime = newEarliest,
          LatestDepartureTime = newLatest
        };

        this.filteredFlights = new FlightFilter(newFilter).FilterFlights(this.allFlights);
      }
    }

    public void PerformFiltering()
    {
      this.FilterResults();
      this.UpdateAdapterFligths();
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

    private void UpdateAdapterFligths()
    {
      if (this.IsVisible)
      {
        var flightsList = this.filteredFlights == null ? new List<IJetStreamFlight>() : new List<IJetStreamFlight>(this.filteredFlights);

        if (this.ListAdapter == null)
        {
          ListAdapter = new FlightsListAdapter(Activity, flightsList, this.flightOrderSelectedListener);
        }
        else
        {
          ((FlightsListAdapter)ListAdapter).Fligths = flightsList;
          ((FlightsListAdapter)ListAdapter).NotifyDataSetChanged();
        }
      }
    }
  }
}