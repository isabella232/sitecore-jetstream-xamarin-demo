// This file has been autogenerated from a class added in the UI designer.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using JetStreamCommons;
using System.Collections;
using Sitecore.MobileSDK.API.Items;

namespace JetStreamIOS
{
	public partial class SearchViewController : UIViewController
	{
		public SearchViewController (IntPtr handle) : base (handle)
		{
		}

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();

      this.LocalizeUI();
    }

    private void LocalizeUI()
    {
      string searchButtonTitle = NSBundle.MainBundle.LocalizedString("SEARCH_FLIGHTS_BUTTON_TITLE", null);
      this.SearchButton.SetTitle(searchButtonTitle, UIControlState.Normal);

      string businessClassButtonTitle = NSBundle.MainBundle.LocalizedString("BUSINESS_CLASS_BUTTON_TITLE", null);
      string economyClassButtonTitle = NSBundle.MainBundle.LocalizedString("ECONOMY_CLASS_BUTTON_TITLE", null);
      string firstClassButtonTitle = NSBundle.MainBundle.LocalizedString("FIRST_CLASS_BUTTON_TITLE", null);
      this.ClassSegmentedControl.SetTitle(businessClassButtonTitle, 0);
      this.ClassSegmentedControl.SetTitle(economyClassButtonTitle, 1);
      this.ClassSegmentedControl.SetTitle(firstClassButtonTitle, 2);

      this.DepartTitleLabel.Text = NSBundle.MainBundle.LocalizedString("DEPART_TITLE", null);
      this.ReturnTitleLabel.Text = NSBundle.MainBundle.LocalizedString("RETURN_TITLE", null);
      this.ClassTitleLabel.Text = NSBundle.MainBundle.LocalizedString("CLASS_TITLE", null);
      this.CountTitleLabel.Text = NSBundle.MainBundle.LocalizedString("COUNT_TITLE", null);
      this.RoundtripTitleLabel.Text = NSBundle.MainBundle.LocalizedString("ROUNDTRIP_TITLE", null);

      this.FromLocationTextField.Placeholder = NSBundle.MainBundle.LocalizedString("FROM_LOCATION_PLACEHOLDER", null); 
      this.ToLocationTextField.Placeholder = NSBundle.MainBundle.LocalizedString("TO_LOCATION_PLACEHOLDER", null);
    }

    private async void SearchTickets()
    {
      //TODO: show loader
      DateTime departDate = DateTime.Parse(DepartDateButton.TitleLabel.Text);
      DateTime returnDate = DateTime.Parse(ReturnDateButton.TitleLabel.Text);
      int castedTicketCounts = Convert.ToInt32(this.TicketCountStepper.Value);

      SearchTicketsRequest request = new SearchTicketsRequestBuilder ()
        .SetFromAirportName (this.FromLocationTextField.Text)
        .SetToAirportName (this.ToLocationTextField.Text)
        .SetDepartDate (departDate)
        .SetReturnDate (returnDate)
        .SetTicketClass (this.ClassSegmentedControl.SelectedSegment)
        .SetTicketsCount (castedTicketCounts)
        .SetRoundtrip (this.RoundtripSwitch.On)
        .Build ();

      RestManager restManager = new RestManager ();
      ScItemsResponse result = await restManager.GetFullAirportsList ();
      //TODO: hide loader
    }

    private async void GetAirports()
    {
//      this.TitleLabel.Text = "*";
//      RestManager restManager = new RestManager ();
//      ScItemsResponse result = await restManager.GetFullAirportsList ();
//      this.TitleLabel.Text = result.TotalCount.ToString();
    }
	}
}
