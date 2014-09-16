namespace JetStreamIOS
{
  using System;
  using System.Collections.Generic;

  using MonoTouch.Foundation;
  using MonoTouch.UIKit;

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
    #endregion Model

		public FlightsFilterViewController(IntPtr handle) : base (handle)
		{
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
      this.PriceTitleLabel.Text = NSBundle.MainBundle.LocalizedString("", null);
      this.EarliestDepartureTimeTitleLabel.Text = NSBundle.MainBundle.LocalizedString("", null);
      this.PriceTitleLabel.Text = NSBundle.MainBundle.LocalizedString("", null);
      this.LatestDepartureTimeTitleLabel.Text = NSBundle.MainBundle.LocalizedString("", null);
      this.DurationTitleLabel.Text = NSBundle.MainBundle.LocalizedString("", null);
      this.RedEyeTitleLabel.Text = NSBundle.MainBundle.LocalizedString("", null);
      this.WifiTitleLabel.Text = NSBundle.MainBundle.LocalizedString("", null);
      this.PersonalEntertainmentTitleLabel.Text = NSBundle.MainBundle.LocalizedString("", null);
      this.FoodServiceTitleLabel.Text = NSBundle.MainBundle.LocalizedString("", null);
    }


    private const decimal FAKE_MAX_PRICE = 10000;

    private TimeSpan GetMaxFlightDuration()
    {
      return new TimeSpan(24, 0, 0);
    }

    private const decimal FAKE_MAX_DURATION_IN_HOURS = 24;
    private void ApplyFilterSettingsToUI()
    {
      this.PriceValueSlider.MinValue = 0;
      this.PriceValueSlider.MaxValue = Convert.ToSingle(FAKE_MAX_PRICE); // TODO : compute from flight list
      this.PriceValueSlider.Value = Convert.ToSingle(this.userInput.MaxPrice);
      this.PriceValueSlider.Continuous = false;
      this.PriceValueLabel.Text = this.userInput.MaxPrice.ToString("C");


      TimeSpan maxDuration = this.GetMaxFlightDuration();
      this.DurationValueSlider.MinValue = 0;
      this.DurationValueSlider.MaxValue = Convert.ToSingle(maxDuration.TotalHours + 1); // TODO : compute from flight list
      this.DurationValueSlider.Value = Convert.ToSingle(this.userInput.MaxDuration.TotalHours);
      this.DurationValueSlider.Continuous = false;
      this.DurationValueLabel.Text = DateConverter.StringFromTimeSpanForUI(this.userInput.MaxDuration);

      string strLatestTime = DateConverter.StringFromTimeForUI(this.userInput.LatestDepartureTime);
      this.LatestDepartureTimeButton.SetTitle(strLatestTime, UIControlState.Normal);

      string strEarliestTime = DateConverter.StringFromTimeForUI(this.userInput.EarliestDepartureTime);
      this.EarliestDepartureTimeButton.SetTitle(strEarliestTime, UIControlState.Normal);

      const bool IS_SWITCH_CHANGES_ANIMATED = false;
      this.RedEyeValueSwitch.SetState(this.userInput.IsRedEyeFlight, IS_SWITCH_CHANGES_ANIMATED);
      this.WifiValueSwitch.SetState(this.userInput.IsInFlightWifiIncluded, IS_SWITCH_CHANGES_ANIMATED);
      this.PersonalEntertainmentValueSwitch.SetState(this.userInput.IsPersonalEntertainmentIncluded, IS_SWITCH_CHANGES_ANIMATED);
      this.FoodServiceValueSwitch.SetState(this.userInput.IsFoodServiceIncluded, IS_SWITCH_CHANGES_ANIMATED);
    }

    private MutableFlightsFilterSettings NewDefaultFilterData()
    {
      var result = new MutableFlightsFilterSettings();
      {
        result.MaxPrice = Convert.ToDecimal(FAKE_MAX_PRICE); // TODO : add a proper value
        result.MaxDuration = this.GetMaxFlightDuration();

        result.IsRedEyeFlight = false;
        result.IsInFlightWifiIncluded = false;
        result.IsPersonalEntertainmentIncluded = false;
        result.IsFoodServiceIncluded = false;

        DateTime localNowDate = DateTime.Today;
        result.EarliestDepartureTime = localNowDate;
        result.LatestDepartureTime = localNowDate.AddDays(1);
      }

      return result;
    }

    #region Navigation
    partial void OnDoneButtonTapped(MonoTouch.Foundation.NSObject sender)
    {
      // IDLE for Interface Builder
    }

    partial void unwindToFlightList(MonoTouch.UIKit.UIStoryboardSegue unwindSegue)
    {
      // IDLE for Interface Builder
    }
    #endregion Navigation

  

    #region Time Actions
    partial void OnLatestDepartureTimeButtonTapped (MonoTouch.Foundation.NSObject sender)
    {
    }

    partial void OnEarliestDepartureTimeButtonTapped (MonoTouch.Foundation.NSObject sender)
    {
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
    partial void OnFoodServiceSwitchValueChanged (MonoTouch.Foundation.NSObject sender)
    {
    }

    partial void OnPersonalEntertainmentSwitchValueChanged (MonoTouch.Foundation.NSObject sender)
    {
    }


    partial void OnRedEyeSwitchValueChanged (MonoTouch.Foundation.NSObject sender)
    {
    }

    partial void OnWifiSwitchValueChanged (MonoTouch.Foundation.NSObject sender)
    {
    }
    #endregion Switch Actions
  }
}
