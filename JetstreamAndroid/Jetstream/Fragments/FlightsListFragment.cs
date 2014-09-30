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
  using Java.IO;
  using JetstreamAndroid.Activities;
  using JetstreamAndroid.Adapters;
  using JetstreamAndroid.Utils;
  using JetStreamCommons;
  using JetStreamCommons.Flight;
  using JetStreamCommons.FlightSearch;
  using Sitecore.MobileSDK.API.Session;

  public class FlightsListFragment : ListFragment
  {
    private const string FragmentTypeKey = "fragment_type";

    private JetstreamApp app;
    private IEnumerable<IJetStreamFlight> flights;

    public static FlightsListFragment NewInstance(FragmentType type)
    {
      System.Diagnostics.Debug.WriteLine(type + " : NewInstance");
      var fragment = new FlightsListFragment();

      var arguments = new Bundle();
      arguments.PutString(FragmentTypeKey, type.ToString());

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

      FragmentType type;
      Enum.TryParse(Arguments.GetString(FragmentTypeKey), out type);

      if (this.flights == null)
      {
        switch (type)
        {
          case FragmentType.TodayFlights:
            this.flights = app.FlightsContainer.Flights;
            break;
          case FragmentType.TommorowFlights:
            this.LoadFlightsForTommorow();
            break;
          case FragmentType.YesterdayFlights:
            this.LoadFlightsForYesterday();
            break;
        }
      }

      var adapter = this.PrepareAdapter(this.flights);
      ListAdapter = adapter;

      SetEmptyText("No tickets avalible");
    }

    public override void OnListItemClick(ListView l, View v, int position, long id)
    {
      var flight = this.flights.ToList()[position];
      this.app.SelectedFlight = flight;
      Activity.StartActivity(typeof(FlightDetailedActivity));
    }

    private async void LoadFlightsForYesterday()
    {
      ISitecoreWebApiSession webApiSession = Prefs.From(app).Session;
      Activity.SetProgressBarIndeterminateVisibility(true);

      using (var jetStreamSession = new RestManager(webApiSession))
      {
        var loader = new FlightSearchLoader(jetStreamSession,
                       app.FlightsContainer.FlightSearchUserInput.DepartureAirport,
                       app.FlightsContainer.FlightSearchUserInput.DestinationAirport,
                       app.FlightsContainer.FlightSearchUserInput.ForwardFlightDepartureDate.AddDays(-1));

        try
        {
          this.flights = await loader.GetFlightsForTheGivenDateAsync();
        }
        catch (System.Exception exception)
        {
          LogUtils.Error(typeof(BookFlightFragment), "Exception during searching tickets\n" + exception);
          DialogHelper.ShowSimpleDialog(Activity, "Error", "Failed to search tickets");
        }
      }

      Activity.SetProgressBarIndeterminateVisibility(false);
    }

    private async void LoadFlightsForTommorow()
    {
      Activity.SetProgressBarIndeterminateVisibility(true);

      ISitecoreWebApiSession webApiSession = Prefs.From(app).Session;
      using (var jetStreamSession = new RestManager(webApiSession))
      {
        var loader = new FlightSearchLoader(jetStreamSession,
                       app.FlightsContainer.FlightSearchUserInput.DepartureAirport,
                       app.FlightsContainer.FlightSearchUserInput.DestinationAirport,
                       app.FlightsContainer.FlightSearchUserInput.ForwardFlightDepartureDate.AddDays(1));

        try
        {
          this.flights = await loader.GetFlightsForTheGivenDateAsync();
        }
        catch (System.Exception exception)
        {
          LogUtils.Error(typeof(BookFlightFragment), "Exception during searching tickets\n" + exception);
          DialogHelper.ShowSimpleDialog(Activity, "Error", "Failed to search tickets");
        }
      }
      Activity.SetProgressBarIndeterminateVisibility(false);
    }

    private BaseAdapter<IJetStreamFlight> PrepareAdapter(IEnumerable<IJetStreamFlight> flights)
    {
      List<IJetStreamFlight> flightsList = flights == null ? new List<IJetStreamFlight>() : new List<IJetStreamFlight>(flights);
      return new FlightsListAdapter(Activity, flightsList);
    }
  }

  public enum FragmentType
  {
    YesterdayFlights,
    TodayFlights,
    TommorowFlights
  }

  ;
}