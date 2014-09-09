namespace JetStreamIOS
{
  using System;
  using System.Collections;
  using System.Threading.Tasks;

  using MonoTouch.Foundation;
  using MonoTouch.UIKit;

  using ActionSheetDatePicker;

  using JetStreamCommons;
  using Sitecore.MobileSDK.API.Items;



	public partial class SearchViewController : UIViewController
	{

    private SearchTicketsRequestBuilder SearchRequestBuilder;
    private ActionSheetDatePickerView actionSheetDatePicker;

		public SearchViewController (IntPtr handle) : base (handle)
		{
		}
      
    public override void ViewDidLoad()
    {
      base.ViewDidLoad();

      this.LocalizeUI();
      this.InitializeDateActionPicker();

      this.SearchRequestBuilder = new  SearchTicketsRequestBuilder();
      this.SearchRequestBuilder.SetReturnDate(DateTime.Now);
      this.SearchRequestBuilder.SetDepartDate(DateTime.Now);
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

      string date = DateConverter.StringFromDateForUI(DateTime.Now);
      ReturnDateButton.SetTitle(date, UIControlState.Normal);
      DepartDateButton.SetTitle(date, UIControlState.Normal);
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
        request = SearchRequestBuilder
          .Build();
      }
      catch(ArgumentException e) 
      {
        string title = NSBundle.MainBundle.LocalizedString("TITLE", null);
        string message;
        if (null != e.ParamName)
        {
          message = NSBundle.MainBundle.LocalizedString (e.ParamName, null);
        }
        else
        {
          message = NSBundle.MainBundle.LocalizedString (e.Message, null);
        }

        AlertHelper.ShowLocalizedAlertWithOkOption(title, message);
        return;
      }

      //TODO: show flights list VC here
      //TODO: move this to flights list VC and make this method sync
      try
      {
        using (var restManager = new RestManager())
        {
          ScItemsResponse result = await restManager.SearchDepartTicketsWithRequest(request);
          AlertHelper.ShowAlertWithOkOption("result", "flights count: " + result.ResultCount.ToString());
        }
      }
      catch
      {
        AlertHelper.ShowAlertWithOkOption("Failure", "Unable to download flights");
      }
    }

    #region Events

    partial void OnDepartDateButtonTouched (MonoTouch.UIKit.UIButton sender)
    {
      actionSheetDatePicker.DatePicker.ValueChanged -= ReturnDateReceived;
      actionSheetDatePicker.DatePicker.ValueChanged += DepartDateReceived;
      actionSheetDatePicker.Show();
    }

    partial void OnReturnDateButtonTouched (MonoTouch.UIKit.UIButton sender)
    {

      actionSheetDatePicker.DatePicker.ValueChanged -= DepartDateReceived;
      actionSheetDatePicker.DatePicker.ValueChanged += ReturnDateReceived;
      actionSheetDatePicker.Show();
    }


    partial void CountValueChanged (MonoTouch.UIKit.UIStepper sender)
    {
      ResultCountLabel.Text = sender.Value.ToString();
    }

    private void DepartDateReceived(object sender, EventArgs e)
    {
      DateTime date = (sender as UIDatePicker).Date;
      string formatedDate = DateConverter.StringFromDateForUI(date);
      this.DepartDateButton.SetTitle(formatedDate, UIControlState.Normal);
      this.SearchRequestBuilder.SetDepartDate(date);
    }

    private void ReturnDateReceived(object sender, EventArgs e)
    {
      DateTime date = (sender as UIDatePicker).Date;
      string formatedDate = DateConverter.StringFromDateForUI(date);
      this.ReturnDateButton.SetTitle(formatedDate, UIControlState.Normal);
      this.SearchRequestBuilder.SetReturnDate(date);
    }

    partial void OnSearchButtonTouched(MonoTouch.UIKit.UIButton sender)
    {
      this.SearchTickets();
    }

    partial void OnRoundtripValueChanged (MonoTouch.UIKit.UISwitch sender)
    {
      this.ToLocationButton.Enabled = sender.On;
      this.ReturnDateButton.Enabled = sender.On;
    }
    #endregion;

    #region Segue

    public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
    {
      base.PrepareForSegue(segue, sender);
      SearchAirportTableViewController searchAirportsViewController = null;

      if ("ToAirportQuickSearch" == segue.Identifier)
      {
        searchAirportsViewController = segue.DestinationViewController as SearchAirportTableViewController;
        searchAirportsViewController.isFromAirportSearch = false;
        searchAirportsViewController.SourceTextField = this.ToLocationTextField;
      }
      else
        if ("FromAirportQuickSearch" == segue.Identifier)
        {
          searchAirportsViewController = segue.DestinationViewController as SearchAirportTableViewController;
          searchAirportsViewController.isFromAirportSearch = true;
          searchAirportsViewController.SourceTextField = this.FromLocationTextField;
        }


      searchAirportsViewController.SearchTicketsBuilder = this.SearchRequestBuilder;
    }

    #endregion;
	}
}
