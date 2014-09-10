namespace JetStreamIOS
{
  using System;

  using MonoTouch.Foundation;
  using MonoTouch.UIKit;


	public partial class AboutViewController : UIViewController
	{
		public AboutViewController (IntPtr handle) : base (handle)
		{
		}

    public override void ViewDidLoad()
    {
      // @adk : static content only
      // Start with the "JetStreamCommons.About" namespace to fetch data from the server

      this.ViewControllerNameLabel.Text = NSBundle.MainBundle.LocalizedString("ABOUT_SCREEN_NAME", null);
      this.AboutTextContainer.Text = NSBundle.MainBundle.LocalizedString("STATIC_ABOUT_SCREEN_TEXT", null);
    }
	}
}
