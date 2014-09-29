namespace JetstreamAndroid.Fragments
{
  using System;
  using System.Collections.Generic;
  using Android.App;
  using Android.OS;
  using Android.Widget;
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

      if(this.flights == null)
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
    }

    private async void LoadFlightsForYesterday()
    {
      ISitecoreWebApiSession webApiSession = Prefs.From(app).Session;
      Activity.SetProgressBarIndeterminateVisibility(true);

      using (var jetStreamSession = new RestManager(webApiSession))
      {
        var loader = new FlightSearchLoader(jetStreamSession,
                       app.FlightsContainer.FlightSearchUserInput.SourceAirport,
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
                       app.FlightsContainer.FlightSearchUserInput.SourceAirport,
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

    private ArrayAdapter<string> PrepareAdapter(IEnumerable<IJetStreamFlight> flights)
    {
      List<IJetStreamFlight> flightsList;
      if(flights == null)
      {
        flightsList = new List<IJetStreamFlight>();
      }
      else
      {
        flightsList = new List<IJetStreamFlight>(flights);
      }

      FragmentType type;
      var isParsed = Enum.TryParse(Arguments.GetString(FragmentTypeKey), out type);
      System.Diagnostics.Debug.WriteLine(type + " : Count" + flightsList.Count);

      var data = flightsList.ConvertAll(input => input.FlightNumber);
      return new ArrayAdapter<String>(Activity, Android.Resource.Layout.SimpleListItem1, data);
    }
  }

  public enum FragmentType
  {
    YesterdayFlights,
    TodayFlights,
    TommorowFlights}

  ;
}