using System;

using Foundation;
using UIKit;

namespace JetStreamIOSFull
{
  public partial class SettingsViewControlle : BaseViewController
	{
		public SettingsViewControlle (IntPtr handle) : base (handle)
		{
      
    }

    public override void ViewDidAppear(bool animated)
    {
      base.ViewDidAppear(animated);

      this.UrlTextField.Text = this.Endpoint.InstanceUrl;
    }

    public override void ViewWillDisappear(bool animated)
    {
      base.ViewWillDisappear(animated);

      this.Endpoint.InstanceUrl = this.UrlTextField.Text;
    }

	}
}
