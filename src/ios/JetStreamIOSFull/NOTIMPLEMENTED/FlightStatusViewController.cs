using System;

using Foundation;
using UIKit;
using JetStreamIOSFull.BaseVC;

namespace JetStreamIOSFull.NOTIMPLEMENTED
{
  public partial class FlightStatusViewController : BaseViewController
	{
		public FlightStatusViewController (IntPtr handle) : base (handle)
		{
		}

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();

      this.TopImageView.Image = this.Appearance.FlightStatus.TopImage;
      string name = NSBundle.MainBundle.LocalizedString("FLIGHT_STATUS_PLACEHOLDER_TITLE", null);
      this.TitleLabel.Text = name;
    }
	}
}
