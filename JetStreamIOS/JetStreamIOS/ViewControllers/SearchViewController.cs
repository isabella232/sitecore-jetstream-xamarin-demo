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

		public SearchViewController (IntPtr handle) : base (handle)
		{
		}
      
    public override void ViewDidLoad()
    {
      base.ViewDidLoad();

      this.LocalizeUI();
      this.InitializeDateActionPicker();

      this.searchRequestBuilder = new SearchTicketsRequestBuilder();
      this.searchRequestBuilder.Set.ReturnDate(DateTime.Now);
      this.searchRequestBuilder.Set.DepartureDate(DateTime.Now);
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

      this.FromLocationTextField.Placeholder = NSBundle.MainBundle.LocalizedString("FROM_LOCATION_PLACEHOLDER", null); 
      this.ToLocationTextField.Placeholder = NSBundle.MainBundle.LocalizedString("TO_LOCATION_PLACEHOLDER", null);

      DateTime nowDate = DateTime.Now;
      string date = DateConverter.StringFromDateForUI(nowDate);
      ReturnDateButton.SetTitle(date, UIControlState.Normal);
      DepartDateButton.SetTitle(date, UIControlState.Normal);

      this.userInput = new MutableFlightSearchUserInput();
      this.userInput.SourceAirport = null;
      this.userInput.DestinationAirport = null;
      this.userInput.ForwardFlightDepartureDate = nowDate;
      this.userInput.ReturnFlightDepartureDate = nowDate;
      this.userInput.TicketsCount = Convert.ToInt32(this.TicketCountStepper.Value);
      this.userInput.TicketClass = TicketClass.Business;
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

    partial void OnDepartDateButtonTouched(MonoTouch.UIKit.UIButton sender)
    {
      this.actionSheetDatePicker.DatePicker.ValueChanged -= ReturnDateReceived;
      this.actionSheetDatePicker.DatePicker.ValueChanged += DepartDateReceived;
      this.actionSheetDatePicker.Show();
    }

    partial void OnReturnDateButtonTouched(MonoTouch.UIKit.UIButton sender)
    {
      this.actionSheetDatePicker.DatePicker.ValueChanged -= DepartDateReceived;
      this.actionSheetDatePicker.DatePicker.ValueChanged += ReturnDateReceived;
      this.actionSheetDatePicker.Show();
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
      this.userInput.ForwardFlightDepartureDate = date;

      string formatedDate = DateConverter.StringFromDateForUI(date);
      this.DepartDateButton.SetTitle(formatedDate, UIControlState.Normal);
      this.searchRequestBuilder.Set.DepartureDate(date);
    }

    private void ReturnDateReceived(object sender, EventArgs e)
    {
      DateTime date = (sender as UIDatePicker).Date;
      this.userInput.ReturnFlightDepartureDate = date;

      string formatedDate = DateConverter.StringFromDateForUI(date);
      this.ReturnDateButton.SetTitle(formatedDate, UIControlState.Normal);
      this.searchRequestBuilder.Set.ReturnDate(date);
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
        this.userInput.ReturnFlightDepartureDate = this.searchRequestBuilder.Build().DepartDate;
        if (null == this.userInput.ReturnFlightDepartureDate)
        {
          this.userInput.ReturnFlightDepartureDate = DateTime.Now;
        }
      }
    }

    private void OnSourceAirportSelected(IJetStreamAirport selectedAirport, int airportIndexInTable)
    {
      this.searchRequestBuilder.Set.SourceAirport(selectedAirport.Id);
      this.userInput.SourceAirport = selectedAirport;
    }

    private void OnDestinationAirportSelected(IJetStreamAirport selectedAirport, int airportIndexInTable)
    {
      this.searchRequestBuilder.Set.DestinationAirport(selectedAirport.Id);
      this.userInput.DestinationAirport = selectedAirport;
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

        searchAirportsViewController.SourceTextField = this.ToLocationTextField;
      }
      else if (StoryboardHelper.IsSegueToSourceAirportSearch(segue))
      {
        SearchAirportTableViewController searchAirportsViewController = null;
        searchAirportsViewController = segue.DestinationViewController as SearchAirportTableViewController;
        searchAirportsViewController.OnAirportSelected = this.OnSourceAirportSelected;

        searchAirportsViewController.SourceTextField = this.FromLocationTextField;
      }
      else if (StoryboardHelper.IsSegueToDepartureFlightsSearch(segue))
      {
        FlightListViewController nextScreen = segue.DestinationViewController as FlightListViewController;
        nextScreen.SearchOptionsFromUser = this.userInput;
      }
    }

    #endregion;
	}
}
