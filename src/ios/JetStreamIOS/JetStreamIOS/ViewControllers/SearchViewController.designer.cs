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
	[Register ("SearchViewController")]
	partial class SearchViewController
	{
		[Outlet]
		MonoTouch.UIKit.UISegmentedControl ClassSegmentedControl { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel ClassTitleLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel CountTitleLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton DepartDateButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel DepartTitleLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton FromLocationButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel FromLocationLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel ResultCountLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton ReturnDateButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel ReturnTitleLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch RoundtripSwitch { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel RoundtripTitleLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton SearchButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIStepper TicketCountStepper { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton ToLocationButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel ToLocationLabel { get; set; }

		[Action ("CountValueChanged:")]
		partial void CountValueChanged (MonoTouch.UIKit.UIStepper sender);

		[Action ("OnDepartDateButtonTouched:")]
		partial void OnDepartDateButtonTouched (MonoTouch.UIKit.UIButton sender);

		[Action ("OnReturnDateButtonTouched:")]
		partial void OnReturnDateButtonTouched (MonoTouch.UIKit.UIButton sender);

		[Action ("OnRoundtripValueChanged:")]
		partial void OnRoundtripValueChanged (MonoTouch.UIKit.UISwitch sender);

		[Action ("OnSearchButtonTouched:")]
		partial void OnSearchButtonTouched (MonoTouch.UIKit.UIButton sender);

		[Action ("OnTicketClassChanged:")]
		partial void OnTicketClassChanged (MonoTouch.UIKit.UISegmentedControl sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ClassSegmentedControl != null) {
				ClassSegmentedControl.Dispose ();
				ClassSegmentedControl = null;
			}

			if (ClassTitleLabel != null) {
				ClassTitleLabel.Dispose ();
				ClassTitleLabel = null;
			}

			if (CountTitleLabel != null) {
				CountTitleLabel.Dispose ();
				CountTitleLabel = null;
			}

			if (DepartDateButton != null) {
				DepartDateButton.Dispose ();
				DepartDateButton = null;
			}

			if (DepartTitleLabel != null) {
				DepartTitleLabel.Dispose ();
				DepartTitleLabel = null;
			}

			if (FromLocationLabel != null) {
				FromLocationLabel.Dispose ();
				FromLocationLabel = null;
			}

			if (ResultCountLabel != null) {
				ResultCountLabel.Dispose ();
				ResultCountLabel = null;
			}

			if (ReturnDateButton != null) {
				ReturnDateButton.Dispose ();
				ReturnDateButton = null;
			}

			if (ReturnTitleLabel != null) {
				ReturnTitleLabel.Dispose ();
				ReturnTitleLabel = null;
			}

			if (RoundtripSwitch != null) {
				RoundtripSwitch.Dispose ();
				RoundtripSwitch = null;
			}

			if (RoundtripTitleLabel != null) {
				RoundtripTitleLabel.Dispose ();
				RoundtripTitleLabel = null;
			}

			if (SearchButton != null) {
				SearchButton.Dispose ();
				SearchButton = null;
			}

			if (TicketCountStepper != null) {
				TicketCountStepper.Dispose ();
				TicketCountStepper = null;
			}

			if (ToLocationButton != null) {
				ToLocationButton.Dispose ();
				ToLocationButton = null;
			}

			if (ToLocationLabel != null) {
				ToLocationLabel.Dispose ();
				ToLocationLabel = null;
			}

			if (FromLocationButton != null) {
				FromLocationButton.Dispose ();
				FromLocationButton = null;
			}
		}
	}
}
