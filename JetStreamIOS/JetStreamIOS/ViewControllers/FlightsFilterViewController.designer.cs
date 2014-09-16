// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace JetStreamIOS
{
	[Register ("FlightsFilterViewController")]
	partial class FlightsFilterViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton DoneButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel DurationTitleLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel DurationValueLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISlider DurationValueSlider { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton EarliestDepartureTimeButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel EarliestDepartureTimeTitleLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel FoodServiceTitleLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch FoodServiceValueSwitch { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton LatestDepartureTimeButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel LatestDepartureTimeTitleLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel PersonalEntertainmentTitleLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch PersonalEntertainmentValueSwitch { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel PriceTitleLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel PriceValueLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISlider PriceValueSlider { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel RedEyeTitleLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch RedEyeValueSwitch { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel WifiTitleLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch WifiValueSwitch { get; set; }

		[Action ("OnDoneButtonTapped:")]
		partial void OnDoneButtonTapped (MonoTouch.Foundation.NSObject sender);

		[Action ("OnDurationValueChanged:")]
		partial void OnDurationValueChanged (MonoTouch.Foundation.NSObject sender);

		[Action ("OnEarliestDepartureTimeButtonTapped:")]
		partial void OnEarliestDepartureTimeButtonTapped (MonoTouch.Foundation.NSObject sender);

		[Action ("OnFoodServiceSwitchValueChanged:")]
		partial void OnFoodServiceSwitchValueChanged (MonoTouch.Foundation.NSObject sender);

		[Action ("OnLatestDepartureTimeButtonTapped:")]
		partial void OnLatestDepartureTimeButtonTapped (MonoTouch.Foundation.NSObject sender);

		[Action ("OnPersonalEntertainmentSwitchValueChanged:")]
		partial void OnPersonalEntertainmentSwitchValueChanged (MonoTouch.Foundation.NSObject sender);

		[Action ("OnPriceValueChanged:")]
		partial void OnPriceValueChanged (MonoTouch.Foundation.NSObject sender);

		[Action ("OnRedEyeSwitchValueChanged:")]
		partial void OnRedEyeSwitchValueChanged (MonoTouch.Foundation.NSObject sender);

		[Action ("OnWifiSwitchValueChanged:")]
		partial void OnWifiSwitchValueChanged (MonoTouch.Foundation.NSObject sender);

		[Action ("unwindToFlightList:")]
		partial void unwindToFlightList (MonoTouch.UIKit.UIStoryboardSegue unwindSegue);
		
		void ReleaseDesignerOutlets ()
		{
			if (LatestDepartureTimeButton != null) {
				LatestDepartureTimeButton.Dispose ();
				LatestDepartureTimeButton = null;
			}

			if (LatestDepartureTimeTitleLabel != null) {
				LatestDepartureTimeTitleLabel.Dispose ();
				LatestDepartureTimeTitleLabel = null;
			}

			if (EarliestDepartureTimeButton != null) {
				EarliestDepartureTimeButton.Dispose ();
				EarliestDepartureTimeButton = null;
			}

			if (EarliestDepartureTimeTitleLabel != null) {
				EarliestDepartureTimeTitleLabel.Dispose ();
				EarliestDepartureTimeTitleLabel = null;
			}

			if (DoneButton != null) {
				DoneButton.Dispose ();
				DoneButton = null;
			}

			if (DurationTitleLabel != null) {
				DurationTitleLabel.Dispose ();
				DurationTitleLabel = null;
			}

			if (DurationValueLabel != null) {
				DurationValueLabel.Dispose ();
				DurationValueLabel = null;
			}

			if (DurationValueSlider != null) {
				DurationValueSlider.Dispose ();
				DurationValueSlider = null;
			}

			if (FoodServiceTitleLabel != null) {
				FoodServiceTitleLabel.Dispose ();
				FoodServiceTitleLabel = null;
			}

			if (FoodServiceValueSwitch != null) {
				FoodServiceValueSwitch.Dispose ();
				FoodServiceValueSwitch = null;
			}

			if (PersonalEntertainmentTitleLabel != null) {
				PersonalEntertainmentTitleLabel.Dispose ();
				PersonalEntertainmentTitleLabel = null;
			}

			if (PersonalEntertainmentValueSwitch != null) {
				PersonalEntertainmentValueSwitch.Dispose ();
				PersonalEntertainmentValueSwitch = null;
			}

			if (PriceTitleLabel != null) {
				PriceTitleLabel.Dispose ();
				PriceTitleLabel = null;
			}

			if (PriceValueLabel != null) {
				PriceValueLabel.Dispose ();
				PriceValueLabel = null;
			}

			if (PriceValueSlider != null) {
				PriceValueSlider.Dispose ();
				PriceValueSlider = null;
			}

			if (RedEyeTitleLabel != null) {
				RedEyeTitleLabel.Dispose ();
				RedEyeTitleLabel = null;
			}

			if (RedEyeValueSwitch != null) {
				RedEyeValueSwitch.Dispose ();
				RedEyeValueSwitch = null;
			}

			if (WifiTitleLabel != null) {
				WifiTitleLabel.Dispose ();
				WifiTitleLabel = null;
			}

			if (WifiValueSwitch != null) {
				WifiValueSwitch.Dispose ();
				WifiValueSwitch = null;
			}
		}
	}
}
