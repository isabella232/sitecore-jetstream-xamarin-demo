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
	[Register ("OrderSummaryViewController")]
	partial class OrderSummaryViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton PurchaseButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIWebView SummaryInfoWebView { get; set; }

		[Action ("OnPurchaseButtonTouched:")]
		partial void OnPurchaseButtonTouched (MonoTouch.UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (PurchaseButton != null) {
				PurchaseButton.Dispose ();
				PurchaseButton = null;
			}

			if (SummaryInfoWebView != null) {
				SummaryInfoWebView.Dispose ();
				SummaryInfoWebView = null;
			}
		}
	}
}
