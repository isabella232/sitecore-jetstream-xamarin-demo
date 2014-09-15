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
		MonoTouch.UIKit.UIButton ArrivalTimeButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel ArrivalTimeTitleLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton DepartureTimeButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel DepartureTimeTitleLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton DoneButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel DurationTitleLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel DurationValueLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISlider DurationValueSlider { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel FoodServiceTitleLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch FoodServiceValueSwitch { get; set; }

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

		[Action ("OnArrivaleTimeButtonTapped:")]
		partial void OnArrivaleTimeButtonTapped (MonoTouch.Foundation.NSObject sender);

		[Action ("OnDepartureTimeButtonTapped:")]
		partial void OnDepartureTimeButtonTapped (MonoTouch.Foundation.NSObject sender);

		[Action ("OnDoneButtonTapped:")]
		partial void OnDoneButtonTapped (MonoTouch.Foundation.NSObject sender);

		[Action ("OnDurationValueChanged:")]
		partial void OnDurationValueChanged (MonoTouch.Foundation.NSObject sender);

		[Action ("OnFoodServiceSwitchValueChanged:")]
		partial void OnFoodServiceSwitchValueChanged (MonoTouch.Foundation.NSObject sender);

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
			if (DoneButton != null) {
				DoneButton.Dispose ();
				DoneButton = null;
			}

			if (PriceTitleLabel != null) {
				PriceTitleLabel.Dispose ();
				PriceTitleLabel = null;
			}

			if (PriceValueSlider != null) {
				PriceValueSlider.Dispose ();
				PriceValueSlider = null;
			}

			if (PriceValueLabel != null) {
				PriceValueLabel.Dispose ();
				PriceValueLabel = null;
			}

			if (DepartureTimeTitleLabel != null) {
				DepartureTimeTitleLabel.Dispose ();
				DepartureTimeTitleLabel = null;
			}

			if (DepartureTimeButton != null) {
				DepartureTimeButton.Dispose ();
				DepartureTimeButton = null;
			}

			if (ArrivalTimeTitleLabel != null) {
				ArrivalTimeTitleLabel.Dispose ();
				ArrivalTimeTitleLabel = null;
			}

			if (ArrivalTimeButton != null) {
				ArrivalTimeButton.Dispose ();
				ArrivalTimeButton = null;
			}

			if (DurationTitleLabel != null) {
				DurationTitleLabel.Dispose ();
				DurationTitleLabel = null;
			}

			if (DurationValueSlider != null) {
				DurationValueSlider.Dispose ();
				DurationValueSlider = null;
			}

			if (DurationValueLabel != null) {
				DurationValueLabel.Dispose ();
				DurationValueLabel = null;
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

			if (PersonalEntertainmentTitleLabel != null) {
				PersonalEntertainmentTitleLabel.Dispose ();
				PersonalEntertainmentTitleLabel = null;
			}

			if (PersonalEntertainmentValueSwitch != null) {
				PersonalEntertainmentValueSwitch.Dispose ();
				PersonalEntertainmentValueSwitch = null;
			}

			if (FoodServiceTitleLabel != null) {
				FoodServiceTitleLabel.Dispose ();
				FoodServiceTitleLabel = null;
			}

			if (FoodServiceValueSwitch != null) {
				FoodServiceValueSwitch.Dispose ();
				FoodServiceValueSwitch = null;
			}
		}
	}
}
