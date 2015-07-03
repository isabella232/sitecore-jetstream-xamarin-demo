using System;

using Foundation;
using UIKit;
using JetStreamCommons;
using JetStreamIOSFull.Helpers;
using JetStreamCommons.About;
using JetStreamIOSFull.BaseVC;

namespace JetStreamIOSFull.About
{
  public partial class AboutViewController : BaseViewController
	{
		public AboutViewController (IntPtr handle) : base (handle)
		{
		}

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();

      this.TitleLabel.Text = "";
      this.WelcomeLabel.Text = "";
      this.MaainTextField.Text = "";


      this.TopImageView.Image = this.Appearance.About.Background;
    }

    public override void ViewWillAppear(bool animated)
    {
      base.ViewWillAppear(animated);

      this.LoadInfo();
    }

    private async void LoadInfo()
    {
      UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;

      try
      {
        using (var session = this.Endpoint.GetSession())
        {
          using (var loader = new ContentLoader (session))
          {
            try
            {
              IAboutPageInfo info = await loader.LoadAboutInfo();
              this.FillScreenWithAboutInfo(info);
            }
            catch(Exception ex)
            {
              AlertHelper.ShowLocalizedAlertWithOkOption("NETWORK_ERROR_TITLE", "CANNOT_DOWNLOAD_ABOUT_ITEM_ERROR");
              throw ex;
            }
          }
        }
      }
      catch
      {
        Console.WriteLine("network error");
      }
      finally
      {
        UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
      }
    }

    private void FillScreenWithAboutInfo(IAboutPageInfo info)
    {
      this.TitleLabel.Text = info.TitlePlainText;
      this.WelcomeLabel.Text = info.SummaryPlainText;

      string clearedText = info.BodyPlainText.Replace(System.Environment.NewLine, " ");
      this.MaainTextField.Text = clearedText;
      this.MaainTextField.Font = this.Appearance.About.DescriptionFont;
      this.MaainTextField.TextAlignment = UITextAlignment.Justified;
    }
     
	}
}
