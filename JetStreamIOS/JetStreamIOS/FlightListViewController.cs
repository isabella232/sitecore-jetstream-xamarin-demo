using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using JetStreamCommons.FlightSearch;

namespace JetStreamIOS
{
	public partial class FlightListViewController : UIViewController
	{
		public FlightListViewController (IntPtr handle) : base (handle)
		{
		}

    public IFlightSearchUserInput SearchOptionsFromUser { get; set; }
	}
}
