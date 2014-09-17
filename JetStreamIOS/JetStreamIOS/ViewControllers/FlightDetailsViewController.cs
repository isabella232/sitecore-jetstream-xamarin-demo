
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
      bool flightPresent = (null != this.SelectedFlight);
      bool optionsPresent = (null != this.Order);
      if (flightPresent && optionsPresent)
      {
        this.FillFlightDetailData ();
      }
    }
      
    private void LocalizeUI()
    {
      string orderButtonTitle = NSBundle.MainBundle.LocalizedString("FLIGHT_DETAILS_ORDER_BUTTON_TITLE", null);
      this.OrderButton.SetTitle(orderButtonTitle, UIControlState.Normal);
    }

    private void FillFlightDetailData()
    {
      string htmlFilePath = NSBundle.MainBundle.PathForResource("flightDetailsTemplate", "html");
      string templateHtmlString = System.IO.File.ReadAllText(htmlFilePath);

      NSUrl resourcesURL = NSBundle.MainBundle.BundleUrl;

      string resultHtmlString = String.Format(templateHtmlString,
        this.SelectedFlight.Flight.FlightNumber,
        this.SelectedFlight.Flight.Price,
        DateConverter.StringFromTimeForUI(SelectedFlight.Flight.DepartureTime),
        this.Order.DepartureAirport.City,
        this.Order.DepartureAirport.Country,
        this.Order.DepartureAirport.Code,
        DateConverter.StringFromTimeForUI(SelectedFlight.Flight.ArrivalTime),
        this.Order.DestinationAirport.City,
        this.Order.DestinationAirport.Country,
        this.Order.DestinationAirport.Code,
        "7", // TODO: @igk duration hours
        "22", // TODO: @igk duration minutes
        //badges hardcoded since item have no such information
        BadgeNames.BagBadgeResourceName(),
        BadgeNames.CrowBadgeResourceName(),
        BadgeNames.DrinkBadgeResourceName()
      );

        this.DetailsWebView.LoadHtmlString(resultHtmlString, resourcesURL);
    }

    partial void OnOrderButtonTouched (MonoTouch.UIKit.UIButton sender)
    {
      this.NavigationController.PopViewControllerAnimated(false);
      this.SelectedFlight.OrderThisFlight();
    }
	}
}
