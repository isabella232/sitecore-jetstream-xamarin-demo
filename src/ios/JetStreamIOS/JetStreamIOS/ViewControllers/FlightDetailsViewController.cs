
namespace JetStreamIOS
{
  using System;

  using MonoTouch.Foundation;
  using MonoTouch.UIKit;
  using JetStreamCommons.Flight;
  using JetStreamCommons.FlightSearch;
  using JetStreamCommons;

	public partial class FlightDetailsViewController : UIViewController
	{
    public FlightCell SelectedFlight;
    public JetStreamOrder Order;

		public FlightDetailsViewController (IntPtr handle) : base (handle)
		{
		}

    public override void ViewDidLoad()
    {
      base.ViewDidLoad ();

      this.LocalizeUI();
      IOSFlightDetailsHtmlBuilder detailsHtmlBuilder = new IOSFlightDetailsHtmlBuilder();

      string resultHtmlString = detailsHtmlBuilder.GetHtmlStringWithFlight(this.SelectedFlight.Flight, this.Order);
      NSUrl resourcesURL = NSBundle.MainBundle.BundleUrl;
      this.DetailsWebView.LoadHtmlString(resultHtmlString, resourcesURL);
    }
      
    private void LocalizeUI()
    {
      string orderButtonTitle = NSBundle.MainBundle.LocalizedString("FLIGHT_DETAILS_ORDER_BUTTON_TITLE", null);
      this.OrderButton.SetTitle(orderButtonTitle, UIControlState.Normal);
    }

    partial void OnOrderButtonTouched (MonoTouch.UIKit.UIButton sender)
    {
      this.NavigationController.PopViewControllerAnimated(false);
      this.SelectedFlight.OrderThisFlight();
    }
	}
}
