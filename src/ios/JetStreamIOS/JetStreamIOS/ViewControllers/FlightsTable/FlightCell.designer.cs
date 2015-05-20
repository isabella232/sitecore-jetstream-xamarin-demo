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
	[Register ("FlightCell")]
	partial class FlightCell
	{
		[Outlet]
		MonoTouch.UIKit.UILabel ArrivalTimeLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel DepartureTimeLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton OrderButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel PriceLabel { get; set; }

		[Action ("OrderButtonPressed:")]
		partial void OrderButtonPressed (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ArrivalTimeLabel != null) {
				ArrivalTimeLabel.Dispose ();
				ArrivalTimeLabel = null;
			}

			if (DepartureTimeLabel != null) {
				DepartureTimeLabel.Dispose ();
				DepartureTimeLabel = null;
			}

			if (OrderButton != null) {
				OrderButton.Dispose ();
				OrderButton = null;
			}

			if (PriceLabel != null) {
				PriceLabel.Dispose ();
				PriceLabel = null;
			}
		}
	}
}
