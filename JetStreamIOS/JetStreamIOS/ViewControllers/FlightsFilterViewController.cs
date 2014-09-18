namespace JetStreamIOS
{
  using System;
  using System.Linq;
  using System.Collections.Generic;

  using MonoTouch.Foundation;
  using MonoTouch.UIKit;

  using ActionSheetDatePicker;
  using JetStreamCommons.Flight;
  using JetStreamCommons.FlightFilter;



	public partial class FlightsFilterViewController : UIViewController
	{
    #region Model
    private MutableFlightsFilterSettings userInput;

    public IFlightFilterUserInput FilterData
    {
      get
      {
        return this.userInput;
      }
      set
      {
        if (null == value)
        {
          return;
        }

        this.userInput = new MutableFlightsFilterSettings(value);
      }
    }

    public IEnumerable<IJetStreamFlight> AllFlights;

    public DateTime DepartureLocalDate { get; set; }
    #endregion Model

    #region Initialization
		public FlightsFilterViewController(IntPtr handle) : base (handle)
		{
		}

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();

      this.InitializeDateActionPicker();
    }

    public override void ViewWillAppear(bool animated)
    {
      base.ViewWillAppear(animated);
      this.LocalizeUI();

      if (null == this.userInput)
      {
        this.userInput = this.NewDefaultFilterData();
      }

      this.ApplyFilterSettingsToUI();
    }

    private void LocalizeUI()
    {
      string doneButtonTitle = NSBundle.MainBundle.LocalizedString("APPLY_SETTINGS_BUTTON_TITLE", null); 
      this.DoneButton.SetTitle(doneButtonTitle, UIControlState.Normal);

      string cancelButtonTitle = NSBundle.MainBundle.LocalizedString("CLEAR_FILTERS_BUTTON_TITLE", null); 
      this.CancelButton.SetTitle(cancelButtonTitle, UIControlState.Normal);

      this.PriceTitleLabel.Text = NSBundle.MainBundle.LocalizedString("PRICE_TITLE", null);
      this.EarliestDepartureTimeTitleLabel.Text = NSBundle.MainBundle.LocalizedString("EARLIEST_DEPARTURE_TITLE", null);
      this.LatestDepartureTimeTitleLabel.Text = NSBundle.MainBundle.LocalizedString("LATEST_DEPARTURE_TITLE", null);
      this.DurationTitleLabel.Text = NSBundle.MainBundle.LocalizedString("DURATION_TITLE", null);
      this.RedEyeTitleLabel.Text = NSBundle.MainBundle.LocalizedString("RRED_EYE_TITLE", null);
      this.WifiTitleLabel.Text = NSBundle.MainBundle.LocalizedString("WIFI_INCLUDED_TITLE", null);
      this.PersonalEntertainmentTitleLabel.Text = NSBundle.MainBundle.LocalizedString("PERSONAL_ENTERTAINMENT_TITLE", null);
      this.FoodServiceTitleLabel.Text = NSBundle.MainBundle.LocalizedString("FOOD_SERVICE_TITLE", null);
    }

    private bool IsNoModel()
    {
      return ( null == this.AllFlights || 0 == this.AllFlights.Count() );
    }

    private TimeSpan GetMaxFlightDuration()
    {
      if ( this.IsNoModel() )
      {
        // fake value
        return new TimeSpan(24, 0, 0);
      }

      return this.AllFlights.Max(singleFlight => singleFlight.Duration);
    }

    private decimal GetMaxPrice()
    {
      if (this.IsNoModel())
      {
        return 10000;
      }

      return this.AllFlights.Max(singleFlight => singleFlight.Price);
    }

    private const bool SHOULD_UPDATE_SLIDERS_IN_REALTIME = true;
    private void ApplyFilterSettingsToUI()
    {
      this.PriceValueSlider.MinValue = 0;
      this.PriceValueSlider.MaxValue = Convert.ToSingle(this.GetMaxPrice()); // TODO : compute from flight list
      this.PriceValueSlider.Value = Convert.ToSingle(this.userInput.MaxPrice);
      this.PriceValueSlider.Continuous = SHOULD_UPDATE_SLIDERS_IN_REALTIME;
      this.PriceValueLabel.Text = this.userInput.MaxPrice.ToString("C");


      TimeSpan maxDuration = this.GetMaxFlightDuration();
      this.DurationValueSlider.MinValue = 0;
      this.DurationValueSlider.MaxValue = Convert.ToSingle(maxDuration.TotalHours + 1); // TODO : compute from flight list
      this.DurationValueSlider.Value = Convert.ToSingle(this.userInput.MaxDuration.TotalHours);
      this.DurationValueSlider.Continuous = SHOULD_UPDATE_SLIDERS_IN_REALTIME;
      this.DurationValueLabel.Text = DateConverter.StringFromTimeSpanForUI(this.userInput.MaxDuration);

      string strLatestTime = DateConverter.StringFromTimeForUI(this.userInput.LatestDepartureTime);
      this.LatestDepartureTimeButton.SetTitle(strLatestTime, UIControlState.Normal);

      string strEarliestTime = DateConverter.StringFromTimeForUI(this.userInput.EarliestDepartureTime);
      this.EarliestDepartureTimeButton.SetTitle(strEarliestTime, UIControlState.Normal);

      const bool IS_SWITCH_CHANGES_ANIMATED = false;
      this.RedEyeValueSwitch.On = this.userInput.IsRedEyeFlight;
      this.WifiValueSwitch.On = this.userInput.IsInFlightWifiIncluded;
      this.PersonalEntertainmentValueSwitch.On = this.userInput.IsPersonalEntertainmentIncluded;
      this.FoodServiceValueSwitch.On = this.userInput.IsFoodServiceIncluded;


      DateTime departureDateMidnight = this.DepartureLocalDate.Date;
      DateTime departureDateEnd = departureDateMidnight.AddDays(1).AddSeconds(-1);

      this.earlyDepartureActionSheetDatePicker.DatePicker.MinimumDate = departureDateMidnight;
      this.earlyDepartureActionSheetDatePicker.DatePicker.MaximumDate = departureDateEnd;
      this.earlyDepartureActionSheetDatePicker.DatePicker.Date = this.userInput.EarliestDepartureTime;

      this.laterDepartureActionSheetDatePicker.DatePicker.MinimumDate = departureDateMidnight;
      this.laterDepartureActionSheetDatePicker.DatePicker.MaximumDate = departureDateEnd;
      this.laterDepartureActionSheetDatePicker.DatePicker.Date = this.userInput.LatestDepartureTime;
    }

    private MutableFlightsFilterSettings NewDefaultFilterData()
    {
      var result = new MutableFlightsFilterSettings();
      {
        result.MaxPrice = Convert.ToDecimal(this.GetMaxPrice()); // TODO : add a proper value
        result.MaxDuration = this.GetMaxFlightDuration();

        result.IsRedEyeFlight = false;
        result.IsInFlightWifiIncluded = false;
        result.IsPersonalEntertainmentIncluded = false;
        result.IsFoodServiceIncluded = false;

        DateTime localNowDate = DateTime.Today;
        result.EarliestDepartureTime = localNowDate;
        result.LatestDepartureTime = localNowDate.AddDays(1).AddSeconds(-1);
      }

      return result;
    }
    #endregion Initialization

    #region DatePicker
    private ActionSheetDatePickerView earlyDepartureActionSheetDatePicker;
    private ActionSheetDatePickerView laterDepartureActionSheetDatePicker;

    private void InitializeDateActionPicker()
    {
      this.earlyDepartureActionSheetDatePicker = new ActionSheetDatePickerView(this.View);
      this.earlyDepartureActionSheetDatePicker.Title = NSBundle.MainBundle.LocalizedString("TIME_PICKER_CAPTION", null);
      this.earlyDepartureActionSheetDatePicker.DoneButtonTitle = NSBundle.MainBundle.LocalizedString("DONE_BUTTON_TITLE", null);
      this.earlyDepartureActionSheetDatePicker.DatePicker.Mode = UIDatePickerMode.Time;
      this.earlyDepartureActionSheetDatePicker.DatePicker.ValueChanged += this.OnEarlyDepartureTimeEntered;

      this.laterDepartureActionSheetDatePicker = new ActionSheetDatePickerView(this.View);
      this.laterDepartureActionSheetDatePicker.Title = NSBundle.MainBundle.LocalizedString("TIME_PICKER_CAPTION", null);
      this.laterDepartureActionSheetDatePicker.DoneButtonTitle = NSBundle.MainBundle.LocalizedString("DONE_BUTTON_TITLE", null);
      this.laterDepartureActionSheetDatePicker.DatePicker.Mode = UIDatePickerMode.Time;
      this.laterDepartureActionSheetDatePicker.DatePicker.ValueChanged += this.OnLateDepartureTimeEntered;
    }
    #endregion

    #region Navigation
    partial void OnDoneButtonTapped(MonoTouch.Foundation.NSObject sender)
    {
      // IDLE for Interface Builder
    }

    partial void OnCancelButtonTapped (MonoTouch.Foundation.NSObject sender)
    {
      this.userInput = null;
    }

    partial void unwindToFlightList(MonoTouch.UIKit.UIStoryboardSegue unwindSegue)
    {
      // IDLE for Interface Builder
    }

    partial void unwindToFlightListWithNoFilters (MonoTouch.UIKit.UIStoryboardSegue unwindSegue)
    {
      // IDLE for Interface Builder
    }
    #endregion Navigation

  

    #region Time Actions
    partial void OnEarliestDepartureTimeButtonTapped(MonoTouch.Foundation.NSObject sender)
    {
      this.earlyDepartureActionSheetDatePicker.Show();
    }

    partial void OnLatestDepartureTimeButtonTapped(MonoTouch.Foundation.NSObject sender)
    {
      this.laterDepartureActionSheetDatePicker.Show();
    }


    private void OnEarlyDepartureTimeEntered(object sender, EventArgs e)
    {
      UIDatePicker picker = sender as UIDatePicker;
      DateTime dateValue = picker.Date;

      this.userInput.EarliestDepartureTime = dateValue;
      string strDateValue = DateConverter.StringFromTimeForUI(dateValue);
      this.EarliestDepartureTimeButton.SetTitle(strDateValue, UIControlState.Normal);
    }

    private void OnLateDepartureTimeEntered(object sender, EventArgs e)
    {
      UIDatePicker picker = sender as UIDatePicker;
      DateTime dateValue = picker.Date;

      this.userInput.LatestDepartureTime = dateValue;
      string strDateValue = DateConverter.StringFromTimeForUI(dateValue);
      this.LatestDepartureTimeButton.SetTitle(strDateValue, UIControlState.Normal);
    }
    #endregion Time Actions

    #region Slider Actions
    partial void OnDurationValueChanged(MonoTouch.Foundation.NSObject sender)
    {
      TimeSpan newValue = TimeSpan.FromHours(this.DurationValueSlider.Value);
      this.userInput.MaxDuration = newValue;
      this.DurationValueLabel.Text = DateConverter.StringFromTimeSpanForUI(newValue);
    }

    partial void OnPriceValueChanged(MonoTouch.Foundation.NSObject sender)
    {
      decimal newValue = Convert.ToDecimal(this.PriceValueSlider.Value);
      this.userInput.MaxPrice = newValue;
      this.PriceValueLabel.Text = newValue.ToString("C");
    }
    #endregion Slider Actions

    #region Switch Actions
    partial void OnFoodServiceSwitchValueChanged(MonoTouch.Foundation.NSObject sender)
    {
      this.userInput.IsFoodServiceIncluded = this.FoodServiceValueSwitch.On;
    }

    partial void OnPersonalEntertainmentSwitchValueChanged(MonoTouch.Foundation.NSObject sender)
    {
      this.userInput.IsPersonalEntertainmentIncluded = this.PersonalEntertainmentValueSwitch.On;
    }


    partial void OnRedEyeSwitchValueChanged(MonoTouch.Foundation.NSObject sender)
    {
      this.userInput.IsRedEyeFlight = this.RedEyeValueSwitch.On;
    }

    partial void OnWifiSwitchValueChanged(MonoTouch.Foundation.NSObject sender)
    {
      this.userInput.IsInFlightWifiIncluded = this.WifiValueSwitch.On;
    }
    #endregion Switch Actions
  }
}
