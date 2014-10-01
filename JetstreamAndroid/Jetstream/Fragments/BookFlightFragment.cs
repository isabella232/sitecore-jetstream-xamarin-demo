namespace JetstreamAndroid.Fragments
{
  using System.Collections.Generic;
  using System.Linq;
  using Android.App;
  using System;
  using Android.OS;
  using Android.Text;
  using Android.Views;
  using Android.Widget;
  using JetstreamAndroid.Activities;
  using JetstreamAndroid.Adapters;
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

    private bool isRoundTrip = false;

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
        this.isRoundTrip = this.roundTripCheckBox.Checked;
        if (this.isRoundTrip)
        {
          this.ShowReturnDate();
        }
        else
        {
          this.HideReturnDate();
        }
      };

      this.roundTripCheckBox.Checked = this.isRoundTrip;
      if (this.isRoundTrip)
      {
        this.ShowReturnDate();
      }

      this.UpdateDepartDate(this.departDate);
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

    private void SearchTickets()
    {
      if (!this.CheckUserInput())
      {
        return;
      }

      JetstreamApp app = JetstreamApp.From(Activity);
      app.FlightUserInput = this.PrepareFlightSearchUserInput();

      Activity.StartActivity(typeof(FlightsActvity));

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

    private bool CheckUserInput()
    {
      if (this.fromAirport == null)
      {
        Toast.MakeText(Activity, "Please select airport from list", ToastLength.Long).Show();
        this.fromAirportField.Error = "Please select airport from list";
        return false;
      }

      if (this.toAirport == null)
      {
        Toast.MakeText(Activity, "Please select airport from list", ToastLength.Long).Show();
        this.toAirportField.Error = "Please select airport from list";
        return false;
      }

      return true;
    }

    private IFlightSearchUserInput PrepareFlightSearchUserInput()
    {
      var userInput = new MutableFlightSearchUserInput
      {
        DepartureAirport = this.fromAirport,
        DestinationAirport = this.toAirport,
        ForwardFlightDepartureDate = this.departDate
      };

      if (this.isRoundTrip)
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
      this.UpdateReturnDate(this.returnDate);
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