namespace JetStreamIOS
{
  using System;
  using System.Collections;
  using System.Threading.Tasks;

  using MonoTouch.Foundation;
  using MonoTouch.UIKit;

  using ActionSheetDatePicker;

  using JetStreamIOS.Helpers;
  using JetStreamCommons;
  using JetStreamCommons.Airport;
  using JetStreamCommons.FlightSearch;
  using Sitecore.MobileSDK.API.Items;

	public partial class SearchViewController : UIViewController
	{
    private SearchTicketsRequestBuilder searchRequestBuilder;
    private ActionSheetDatePickerView actionSheetDatePicker;
    private MutableFlightSearchUserInput userInput;

    private DateTime departureDateHolder;
    private DateTime returnDateHolder;

		public SearchViewController (IntPtr handle) : base (handle)
		{
		}
      
    public override void ViewDidLoad()
    {
      base.ViewDidLoad();

      //@igk order matters
      this.searchRequestBuilder = new SearchTicketsRequestBuilder();
      this.userInput = new MutableFlightSearchUserInput();
      this.InitializeDateActionPicker();
      this.LocalizeUI();
    }

    private void LocalizeUI()
    {
      string searchButtonTitle = NSBundle.MainBundle.LocalizedString("SEARCH_FLIGHTS_BUTTON_TITLE", null);
      this.SearchButton.SetTitle(searchButtonTitle, UIControlState.Normal);

      string businessClassButtonTitle = NSBundle.MainBundle.LocalizedString("BUSINESS_CLASS_BUTTON_TITLE", null);
      string economyClassButtonTitle = NSBundle.MainBundle.LocalizedString("ECONOMY_CLASS_BUTTON_TITLE", null);
      string firstClassButtonTitle = NSBundle.MainBundle.LocalizedString("FIRST_CLASS_BUTTON_TITLE", null);
      this.ClassSegmentedControl.SetTitle(businessClassButtonTitle, 0);
      this.ClassSegmentedControl.SetTitle(economyClassButtonTitle, 1);
      this.ClassSegmentedControl.SetTitle(firstClassButtonTitle, 2);

      this.DepartTitleLabel.Text = NSBundle.MainBundle.LocalizedString("DEPART_TITLE", null);
      this.ReturnTitleLabel.Text = NSBundle.MainBundle.LocalizedString("RETURN_TITLE", null);
      this.ClassTitleLabel.Text = NSBundle.MainBundle.LocalizedString("CLASS_TITLE", null);
      this.CountTitleLabel.Text = NSBundle.MainBundle.LocalizedString("COUNT_TITLE", null);
      this.RoundtripTitleLabel.Text = NSBundle.MainBundle.LocalizedString("ROUNDTRIP_TITLE", null);

      string fromButtonTitle = NSBundle.MainBundle.LocalizedString("FROM_LOCATION_PLACEHOLDER", null);
      string toButtonTitle = NSBundle.MainBundle.LocalizedString("TO_LOCATION_PLACEHOLDER", null);
      this.FromLocationButton.SetTitle(fromButtonTitle, UIControlState.Normal);
      this.ToLocationButton.SetTitle(toButtonTitle, UIControlState.Normal);

      this.FromLocationLabel.Text = NSBundle.MainBundle.LocalizedString("FROM_LOCATION_LABEL_TITLE", null); 
      this.ToLocationLabel.Text = NSBundle.MainBundle.LocalizedString("TO_LOCATION_LABEL_TITLE", null); 

      this.userInput.SourceAirport = null;
      this.userInput.DestinationAirport = null;
      this.userInput.TicketsCount = Convert.ToInt32(this.TicketCountStepper.Value);
      this.userInput.TicketClass = TicketClass.Business;

      this.DepartureDate = DateTime.Now;
      this.ReturnDate = DateTime.Now;
    }

    private void InitializeDateActionPicker()
    {
      actionSheetDatePicker = new ActionSheetDatePickerView (this.View);
      actionSheetDatePicker.Title = NSBundle.MainBundle.LocalizedString("DATE_PICKER_TITLE", null);
      actionSheetDatePicker.DoneButtonTitle = NSBundle.MainBundle.LocalizedString("DONE_BUTTON_TITLE", null);
      actionSheetDatePicker.DatePicker.Mode = UIDatePickerMode.Date;   
    }

    private async void SearchTickets()
    {
      SearchFlightsRequest request = null;

      try
      {
        request = this.searchRequestBuilder.Build();
      }
      catch(ArgumentException e) 
      {
        string title = NSBundle.MainBundle.LocalizedString("TITLE", null);
        string message;
        if (null != e.ParamName)
        {
          message = NSBundle.MainBundle.LocalizedString(e.ParamName, null);
        }
        else
        {
          message = NSBundle.MainBundle.LocalizedString(e.Message, null);
        }

        AlertHelper.ShowLocalizedAlertWithOkOption(title, message);
        return;
      }

      //TODO: show flights list VC here
      //TODO: move this to flights list VC and make this method sync
      try
      {
        // It will automatically get values from the NSUserDefaults singleton
        var endpoint = new InstanceSettings();

        // It will be disposed by RestManager
        var session = endpoint.GetSession();
        using (var restManager = new RestManager(session))
        {
          ScItemsResponse result = await restManager.SearchDepartTicketsWithRequest(request);

          string flightsCountFormat = NSBundle.MainBundle.LocalizedString("FLIGHTS_COUNT_FORMAT", null);
          string flightsCountMessage = string.Format(flightsCountFormat, result.ResultCount.ToString());
          AlertHelper.ShowLocalizedAlertWithOkOption("RESULT_TITLE_ALERT", flightsCountMessage);
        }
      }
      catch
      {
        AlertHelper.ShowLocalizedAlertWithOkOption("FAILURE_ALERT_TITLE", "FLIGHTS_DOWNLOAD_FAILED_ALERT_MESSAGE");
      }
    }

    #region Events
    partial void OnSearchButtonTouched(MonoTouch.UIKit.UIButton sender)
    {
      if (!FlightInputValidator.IsFlightInputValid(this.userInput))
      {
        AlertHelper.ShowLocalizedAlertWithOkOption("VALIDATION_FAILED_ALERT_TITLE", "VALIDATION_FAILED_ALERT_MESSAGE");
        return;
      }

      StoryboardHelper.NavigateToDepartureFlightsListFromViewController(this);
    }

    partial void OnDepartDateButtonTouched(MonoTouch.UIKit.UIButton sender)
    {
      this.actionSheetDatePicker.DatePicker.ValueChanged -= ReturnDateReceived;
      this.actionSheetDatePicker.DatePicker.ValueChanged += DepartDateReceived;
      this.actionSheetDatePicker.ShowWithDate(this.DepartureDate);
    }

    partial void OnReturnDateButtonTouched(MonoTouch.UIKit.UIButton sender)
    {
      this.actionSheetDatePicker.DatePicker.ValueChanged -= DepartDateReceived;
      this.actionSheetDatePicker.DatePicker.ValueChanged += ReturnDateReceived;
      this.actionSheetDatePicker.ShowWithDate(this.ReturnDate);
    }
      
    partial void OnTicketClassChanged(MonoTouch.UIKit.UISegmentedControl sender)
    {
      TicketClass[] segmentValues = 
      {
        TicketClass.Business,
        TicketClass.Economy,
        TicketClass.FirstClass
      };

      this.userInput.TicketClass = segmentValues[sender.SelectedSegment];
    }

    partial void CountValueChanged(MonoTouch.UIKit.UIStepper sender)
    {
      ResultCountLabel.Text = sender.Value.ToString();
      this.userInput.TicketsCount = Convert.ToInt32(sender.Value);
    }

    private void DepartDateReceived(object sender, EventArgs e)
    {
      DateTime date = (sender as UIDatePicker).Date;
      this.DepartureDate = date;
    }

    private void ReturnDateReceived(object sender, EventArgs e)
    {
      DateTime date = (sender as UIDatePicker).Date;
      this.ReturnDate = date;
    }

    partial void OnRoundtripValueChanged(MonoTouch.UIKit.UISwitch sender)
    {
      this.ReturnDateButton.Enabled = sender.On;

      if (!sender.On)
      {
        this.userInput.ReturnFlightDepartureDate = null;
      }
      else
      {
        // @adk : using legacy until full migration
        this.ReturnDate = this.returnDateHolder;
        if (null == this.userInput.ReturnFlightDepartureDate)
        {
          this.ReturnDate = DateTime.Now;
        }
      }
    }

    private void OnSourceAirportSelected(IJetStreamAirport selectedAirport, int airportIndexInTable)
    {
      this.searchRequestBuilder.Set.SourceAirport(selectedAirport.Id);
      this.userInput.SourceAirport = selectedAirport;
      this.FromLocationButton.SetTitle(selectedAirport.DisplayName, UIControlState.Normal);
    }

    private void OnDestinationAirportSelected(IJetStreamAirport selectedAirport, int airportIndexInTable)
    {
      this.searchRequestBuilder.Set.DestinationAirport(selectedAirport.Id);
      this.userInput.DestinationAirport = selectedAirport;
      this.ToLocationButton.SetTitle(selectedAirport.DisplayName, UIControlState.Normal);
    }
    #endregion;

    #region Segue

    public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
    {
      base.PrepareForSegue(segue, sender);

      if (StoryboardHelper.IsSegueToDestinationAirportSearch(segue))
      {
        SearchAirportTableViewController searchAirportsViewController = null;
        searchAirportsViewController = segue.DestinationViewController as SearchAirportTableViewController;
        searchAirportsViewController.OnAirportSelected = this.OnDestinationAirportSelected;

        this.FillSearchFieldWithAirportName (searchAirportsViewController, this.userInput.DestinationAirport);
      }
      else if (StoryboardHelper.IsSegueToSourceAirportSearch(segue))
      {
        SearchAirportTableViewController searchAirportsViewController = null;
        searchAirportsViewController = segue.DestinationViewController as SearchAirportTableViewController;
        searchAirportsViewController.OnAirportSelected = this.OnSourceAirportSelected;

        this.FillSearchFieldWithAirportName (searchAirportsViewController, this.userInput.SourceAirport);
      }
      else if (StoryboardHelper.IsSegueToDepartureFlightsSearch(segue))
      {
        FlightListViewController nextScreen = segue.DestinationViewController as FlightListViewController;
        nextScreen.SearchOptionsFromUser = this.userInput;
      }
    }

    private void FillSearchFieldWithAirportName(SearchAirportTableViewController searchController, IJetStreamAirport airport)
    {
      string textToSearch = "";
      if (null != airport)
      {
        textToSearch = airport.DisplayName;
      }
      searchController.SourceText = textToSearch;
    }

    #endregion;

    #region Date Logik
    private DateTime DepartureDate
    {
      get
      {
        return this.departureDateHolder;
      }
      set
      {
        this.departureDateHolder = value;

        this.searchRequestBuilder.Set.DepartureDate(value);
        this.userInput.ForwardFlightDepartureDate = value;

        string formatedDate = DateConverter.StringFromDateForUI(value);
        this.DepartDateButton.SetTitle(formatedDate, UIControlState.Normal);
      }
    }

    private DateTime ReturnDate
    {
      get
      {
        return this.returnDateHolder;
      }
      set
      {
        this.returnDateHolder = value;

        this.searchRequestBuilder.Set.ReturnDate(value);
        this.userInput.ReturnFlightDepartureDate = value;

        string formatedDate = DateConverter.StringFromDateForUI(value);
        this.ReturnDateButton.SetTitle(formatedDate, UIControlState.Normal);
      }
    }

    #endregion;
	}
}
