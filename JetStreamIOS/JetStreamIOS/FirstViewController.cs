using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using JetStreamCommons;
using Sitecore.MobileSDK.API.Items;

namespace JetStreamIOS
{
  public partial class FirstViewController : UIViewController
  {
    public FirstViewController(IntPtr handle) : base (handle)
    {
      Title = NSBundle.MainBundle.LocalizedString ("First", "First");
      TabBarItem.Image = UIImage.FromBundle ("first");
    }
      
    public override void DidReceiveMemoryWarning()
    {
      // Releases the view if it doesn't have a superview.
      base.DidReceiveMemoryWarning ();
      
      // Release any cached data, images, etc that aren't in use.
    }

   

    #region View lifecycle

    public override void ViewDidLoad()
    {
      base.ViewDidLoad ();
      this.LoadAirports ();
    }

    private async void LoadAirports()
    {
      RestManager restManager = new RestManager ();
      ScItemsResponse airports = await restManager.GetFullAirportsList ();
      this.TitleLabel.Text = airports.ResultCount.ToString();
    }

    public override void ViewWillAppear(bool animated)
    {
      base.ViewWillAppear (animated);
    }

    public override void ViewDidAppear(bool animated)
    {
      base.ViewDidAppear (animated);
    }

    public override void ViewWillDisappear(bool animated)
    {
      base.ViewWillDisappear (animated);
    }

    public override void ViewDidDisappear(bool animated)
    {
      base.ViewDidDisappear (animated);
    }

    #endregion
  }
}

