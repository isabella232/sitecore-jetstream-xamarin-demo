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
	[Register ("FlightDetailsViewController")]
	partial class FlightDetailsViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIWebView DetailsWebView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton OrderButton { get; set; }

		[Action ("OnOrderButtonTouched:")]
		partial void OnOrderButtonTouched (MonoTouch.UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (DetailsWebView != null) {
				DetailsWebView.Dispose ();
				DetailsWebView = null;
			}

			if (OrderButton != null) {
				OrderButton.Dispose ();
				OrderButton = null;
			}
		}
	}
}
