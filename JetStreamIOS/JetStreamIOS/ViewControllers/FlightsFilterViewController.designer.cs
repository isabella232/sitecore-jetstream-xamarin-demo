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

		[Action ("OnDoneButtonTapped:")]
		partial void OnDoneButtonTapped (MonoTouch.Foundation.NSObject sender);

		[Action ("unwindToFlightList:")]
		partial void unwindToFlightList (MonoTouch.UIKit.UIStoryboardSegue unwindSegue);
		
		void ReleaseDesignerOutlets ()
		{
			if (DoneButton != null) {
				DoneButton.Dispose ();
				DoneButton = null;
			}
		}
	}
}
