namespace JetStreamIOS
{
  using System;

  using MonoTouch.Foundation;
  using MonoTouch.UIKit;


	public partial class FlightsFilterViewController : UIViewController
	{
		public FlightsFilterViewController (IntPtr handle) : base (handle)
		{
		}

    partial void OnDoneButtonTapped(MonoTouch.Foundation.NSObject sender)
    {
      // IDLE for Interface Builder
    }

    partial void unwindToFlightList(MonoTouch.UIKit.UIStoryboardSegue unwindSegue)
    {
      // IDLE for Interface Builder
    }
	}
}
