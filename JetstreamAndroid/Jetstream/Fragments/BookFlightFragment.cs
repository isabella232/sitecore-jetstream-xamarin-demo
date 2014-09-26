namespace JetstreamAndroid.Fragments
{
  using System.Collections.Generic;
  using Android.App;
  using System;
  using Android.OS;
  using Android.Text;
  using Android.Views;
  using Android.Widget;
  using JetstreamAndroid.Activities;
  using JetstreamAndroid.Adapters;
  using JetstreamAndroid.Models;
  using JetstreamAndroid.Utils;
  using JetStreamCommons;
  using JetStreamCommons.Airport;
  using JetStreamCommons.Flight;
  using JetStreamCommons.FlightSearch;
  using Sitecore.MobileSDK.API.Session;

  public class BookFlightFragment : Android.Support.V4.App.Fragment, IOperationListener
  {
    private const int DialogDepart = 1;
    private const int DialogReturn = 2;

    #region User input values

    private DateTime departDate = DateTime.Today;
    private DateTime returnDate = DateTime.Today.AddDays(1);

    private IJetStreamAirport fromAirport;
    private IJetStreamAirport toAirport;

    #endregion

    #region Views

    private Button departDateButton;
    private Button returnDateButton;
    private Button searchButton;

    private TextView returnDateTextView;

    private CheckBox roundTripCheckBox;

    private Spinner numberOfTicketSpinner;
    private Spinner ticketClassSpinner;

    private AutoCompleteTextView toAirportField;
    private AutoCompleteTextView fromAirportField;

    #endregion

    #region Additional variables

    private AutoCompleteAdapter autoCompleteAdapter;
    private AirportsFilter filter;

    #endregion

    public override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);
      RetainInstance = true;
    }

    public override View OnCreateView(LayoutInflater inflater, ViewGroup viewGroup, Bundle bundle)
    {
      var root = inflater.Inflate(Resource.Layout.fragment_search, viewGroup, false);

      this.InitializeButtons(root);

      this.InitSpinners(root);

      this.returnDateTextView = root.FindViewById<TextView>(Resource.Id.textview_return_date);

      this.roundTripCheckBox = root.FindViewById<CheckBox>(Resource.Id.checkBox_roundtrip);
      this.roundTripCheckBox.Click += (o, e) =>
      {
        if (this.roundTripCheckBox.Checked)
        {
          this.ShowReturnDate();
        }
        else
        {
          this.HideReturnDate();
        }
      };

      this.UpdateDepartDate(DateTime.Today);
      this.InitFields(root);
      return root;
    }

    private void InitSpinners(View root)
    {
      this.numberOfTicketSpinner = root.FindViewById<Spinner>(Resource.Id.spinner_number_of_tickets);
      this.ticketClassSpinner = root.FindViewById<Spinner>(Resource.Id.spinner_classes);
    }

    private void InitFields(View root)
    {
      if (this.autoCompleteAdapter == null)
      {
        this.autoCompleteAdapter = new AutoCompleteAdapter(Activity, Android.Resource.Layout.SimpleDropDownItem1Line, new string[] { });
        this.filter = new AirportsFilter(this.Activity, autoCompleteAdapter, this);
      }

      autoCompleteAdapter.AirportsFilter = filter;

      this.fromAirportField = root.FindViewById<AutoCompleteTextView>(Resource.Id.field_from_location);
      this.toAirportField = root.FindViewById<AutoCompleteTextView>(Resource.Id.field_to_location);
    }

    private void InitializeButtons(View root)
    {
      this.departDateButton = root.FindViewById<Button>(Resource.Id.button_depart_date);
      this.departDateButton.Click += (sender, args) => this.ShowDialogForDate(departDate, DialogDepart);

      this.returnDateButton = root.FindViewById<Button>(Resource.Id.button_return_date);
      this.returnDateButton.Click += (sender, args) => this.ShowDialogForDate(returnDate, DialogReturn);

      this.searchButton = root.FindViewById<Button>(Resource.Id.button_search_tickets);
      this.searchButton.Click += (sender, args) => this.SearchTickets();
    }

    private async void SearchTickets()
    {
      this.OnOperationStarted();
      this.searchButton.Enabled = false;

      ISitecoreWebApiSession webApiSession = Prefs.From(Activity).Session;
      var input = this.PrepareFlightSearchUserInput();

      using (var jetStreamSession = new RestManager(webApiSession))
      {
        var loader = new FlightSearchLoader(jetStreamSession,
          input.SourceAirport,
          input.DestinationAirport,
          input.ForwardFlightDepartureDate);

        try
        {
          IEnumerable<IJetStreamFlight> flights = await loader.GetFlightsForTheGivenDateAsync();
          DaySummary yesterday = await loader.GetPreviousDayAsync();
          DaySummary tomorrow = await loader.GetNextDayAsync();

          this.OnOperationFinished();
          this.searchButton.Enabled = true;

          var message = string.Format("Received {0} tickets", new List<IJetStreamFlight>(flights).Count);
          Toast.MakeText(Activity, message, ToastLength.Short).Show();

          var flightsContainer = new FlightsContainer
          {
            FlightSearchUserInput = input,
            Flights = flights,
            YesterdaySummary = yesterday,
            TomorrowSummary = tomorrow
          };

          JetstreamApp app = JetstreamApp.From(Activity);
          app.FlightsContainer = flightsContainer;

          Activity.StartActivity(typeof(FlightsActvity));
        }
        catch (System.Exception exception)
        {
          this.OnOperationFailed();
          this.searchButton.Enabled = true;

          LogUtils.Error(typeof(BookFlightFragment), "Exception during searching tickets\n" + exception);
          DialogHelper.ShowSimpleDialog(Activity, "Error", "Failed to search tickets");
        }
      }

    }

    public override void OnResume()
    {
      base.OnResume();

      this.fromAirportField.Adapter = autoCompleteAdapter;
      this.toAirportField.Adapter = autoCompleteAdapter;

      this.fromAirportField.TextChanged += this.HandleFromAirportFieldTextChanged;
      this.toAirportField.TextChanged += this.HandleToAirportFieldTextChanged;

      this.fromAirportField.ItemClick += this.HandleFromAirportFieldItemClick;
      this.toAirportField.ItemClick += this.HandleToAirportFieldItemClick;
    }

    private IFlightSearchUserInput PrepareFlightSearchUserInput()
    {
      var userInput = new MutableFlightSearchUserInput
      {
        SourceAirport = this.fromAirport,
        DestinationAirport = this.toAirport,
        ForwardFlightDepartureDate = this.departDate
      };

      if (this.roundTripCheckBox.Checked)
      {
        userInput.ReturnFlightDepartureDate = this.returnDate;
      }

      userInput.TicketsCount = this.numberOfTicketSpinner.SelectedItemPosition + 1;

      switch (this.ticketClassSpinner.SelectedItemPosition)
      {
        case 0:
          userInput.TicketClass = TicketClass.Business;
          break;
        case 1:
          userInput.TicketClass = TicketClass.Economy;
          break;
        case 2:
          userInput.TicketClass = TicketClass.FirstClass;
          break;
      }

      return userInput;
    }

    private void ShowDialogForDate(DateTime time, int dialogId)
    {
      EventHandler<DatePickerDialog.DateSetEventArgs> action = null;

      switch (dialogId)
      {
        case DialogDepart:
          action = (sender, e) => this.UpdateDepartDate(e.Date);
          break;
        case DialogReturn:
          action = (sender, e) => this.UpdateReturnDate(e.Date);
          break;
      }

      new DatePickerDialog(Activity, action, time.Year, time.Month - 1, time.Day).Show();
    }

    private void UpdateReturnDate(DateTime date)
    {
      this.returnDate = date.Date;
      this.returnDateButton.Text = date.Date.ToShortDateString();
    }

    private void HandleToAirportFieldTextChanged(object sender, TextChangedEventArgs e)
    {
      this.toAirport = null;
    }

    private void HandleToAirportFieldItemClick(object sender, AdapterView.ItemClickEventArgs e)
    {
      this.toAirport = this.autoCompleteAdapter.SearchedAirports[e.Position];
    }

    private void HandleFromAirportFieldTextChanged(object sender, TextChangedEventArgs e)
    {
      this.fromAirport = null;
    }

    private void HandleFromAirportFieldItemClick(object sender, AdapterView.ItemClickEventArgs e)
    {
      this.fromAirport = this.autoCompleteAdapter.SearchedAirports[e.Position];
    }

    private void UpdateDepartDate(DateTime date)
    {
      this.departDate = date.Date;
      this.departDateButton.Text = date.Date.ToShortDateString();
    }

    private void HideReturnDate()
    {
      this.returnDateButton.Visibility = ViewStates.Gone;
      this.returnDateTextView.Visibility = ViewStates.Gone;
    }

    private void ShowReturnDate()
    {
      this.returnDateButton.Visibility = ViewStates.Visible;
      this.returnDateTextView.Visibility = ViewStates.Visible;
    }

    public void OnOperationStarted()
    {
      Activity.RunOnUiThread(() => Activity.SetProgressBarIndeterminateVisibility(true));
    }

    public void OnOperationFinished()
    {
      Activity.RunOnUiThread(() => Activity.SetProgressBarIndeterminateVisibility(false));
    }

    public void OnOperationFailed()
    {
      Activity.RunOnUiThread(() => Activity.SetProgressBarIndeterminateVisibility(false));
    }
  }
}