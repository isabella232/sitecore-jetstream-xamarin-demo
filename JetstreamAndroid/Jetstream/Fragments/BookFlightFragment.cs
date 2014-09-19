namespace JetstreamAndroid.Fragments
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Android.App;
  using Android.OS;
  using Android.Views;
  using Android.Widget;
  using JetstreamAndroid.Adapters;
  using JetstreamAndroid.Utils;
  using JetStreamCommons;
  using JetStreamCommons.Airport;
  using JetStreamCommons.Flight;
  using JetStreamCommons.FlightSearch;
  using Sitecore.MobileSDK.API.Session;

  public class BookFlightFragment : Fragment, IOperationListener
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

    private TextView returnDateTextView;

    private CheckBox roundTripCheckBox;

    private Spinner numberOfTicketSpinner;
    private Spinner ticketClassSpinner;

    #endregion

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
      var autoCompleteAdapter = new AutoCompleteAdapter(Activity, Android.Resource.Layout.SimpleDropDownItem1Line, new string[] { });
      var filter = new AirportsFilter(this.Activity, autoCompleteAdapter, this);

      autoCompleteAdapter.AirportsFilter = filter;

      var fromField = root.FindViewById<AutoCompleteTextView>(Resource.Id.field_from_location);
      var toField = root.FindViewById<AutoCompleteTextView>(Resource.Id.field_to_location);

      fromField.TextChanged += (sender, args) =>
      {
        this.fromAirport = null;
      };

      fromField.ItemClick += delegate(object sender, AdapterView.ItemClickEventArgs args)
      {
        this.fromAirport = autoCompleteAdapter.SearchedAirports[args.Position];
      };

      toField.ItemClick += delegate(object sender, AdapterView.ItemClickEventArgs args)
      {
        this.toAirport = autoCompleteAdapter.SearchedAirports[args.Position];
      };

      toField.TextChanged += (sender, args) =>
      {
        this.toAirport = null;
      };

      fromField.Adapter = autoCompleteAdapter;
      toField.Adapter = autoCompleteAdapter;
    }

    private void InitializeButtons(View root)
    {
      this.departDateButton = root.FindViewById<Button>(Resource.Id.button_depart_date);
      this.departDateButton.Click += (sender, args) => this.ShowDialogForDate(departDate, DialogDepart);

      this.returnDateButton = root.FindViewById<Button>(Resource.Id.button_return_date);
      this.returnDateButton.Click += (sender, args) => this.ShowDialogForDate(returnDate, DialogReturn);

      var searchButton = root.FindViewById<Button>(Resource.Id.button_search_tickets);
      searchButton.Click += (sender, args) => this.ReloadData();
    }

    private async void ReloadData()
    {
      this.OnOperationStarted();

      ISitecoreWebApiSession webApiSession = Prefs.From(Activity).Session;
      var input = this.prepareFlightSearchUserInput();

      using (var jetStreamSession = new RestManager(webApiSession))
      {
        var loader = new FlightSearchLoader(jetStreamSession,
          input.SourceAirport,
          input.DestinationAirport,
          input.ForwardFlightDepartureDate);

        DaySummary yesterday = null;
        DaySummary tomorrow = null;

        try
        {
          IEnumerable<IJetStreamFlight> flights = await loader.GetFlightsForTheGivenDateAsync();
          //                  this.allFlights = flights;
          //                  flights = this.FilterFlights(flights);
          yesterday = await loader.GetPreviousDayAsync();
          tomorrow = await loader.GetNextDayAsync();

          this.OnOperationFinished();

          var message = string.Format("Received {0} tickets", new List<IJetStreamFlight>(flights).Count);
          DialogHelper.ShowSimpleDialog(Activity, "Received", message);
        }
        catch
        {
          this.OnOperationFailed();
          DialogHelper.ShowSimpleDialog(Activity, "Error", "Failed to search tickets");
        }
      }

    }

    private IFlightSearchUserInput prepareFlightSearchUserInput()
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