укрпочта // This file has been autogenerated from a class added in the UI designer.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using JetStreamCommons.Flight;
using JetStreamCommons.FlightSearch;

namespace JetStreamIOS
{
	public partial class FlightDetailsViewController : UIViewController
	{
    public IJetStreamFlight SelectedFlight { get; set; }
    public IFlightSearchUserInput CurrentSearchOptions { get; set; }

		public FlightDetailsViewController (IntPtr handle) : base (handle)
		{
		}

    public override void ViewDidLoad()
    {
      base.ViewDidLoad ();

      this.LocalizeUI();
      bool flightPresent = (null != this.SelectedFlight);
      bool optionsPresent = (null != this.CurrentSearchOptions);
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
        SelectedFlight.FlightNumber,
        SelectedFlight.Price,
        DateConverter.StringFromTimeForUI(SelectedFlight.DepartureTime),
        CurrentSearchOptions.SourceAirport.City,
        CurrentSearchOptions.SourceAirport.Country,
        CurrentSearchOptions.SourceAirport.Code,
        DateConverter.StringFromTimeForUI(SelectedFlight.ArrivalTime),
        CurrentSearchOptions.DestinationAirport.City,
        CurrentSearchOptions.DestinationAirport.Country,
        CurrentSearchOptions.DestinationAirport.Code,
        "7",
        "22",
        //badges hardcoded since all server item have no information about services
        BadgeNames.BagBadgeResourceName(),
        BadgeNames.CrowBadgeResourceName(),
        BadgeNames.DrinkBadgeResourceName()
      );

      this.DetailsWebView.LoadHtmlString(templateHtmlString, resourcesURL);
      selectedFlight.

//      0 - flight number
//      1 - flight price ($ only)
//      2 - departure time
//      3 - departure city
//      4 - departure country
//      5 - departure airport code
//      6 - arrival time
//      7 - arrival city
//      8 - arrival country
//      9 - arrival airport code
//      10 - duration hours
//      11 - duration minutes
//      12 - first badge resource name
//      13 - second badge resource name
//      14 - third badge resource name

    }
	}
}