using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using CoreGraphics;
using JetStreamIOSFull.BaseVC;
using Sitecore.MobileSDK.API.Session;
using JetStreamIOSFull.Helpers;

namespace JetStreamIOSFull
{
  partial class InstanseViewController : BaseViewController
	{
    private static CGSize popoverSize = new CGSize(400, 300);
    private static string unwindSegieIdentifier = "UnwindToSettingsViewController";

    public InstanceSettings.InstanceSettings currentInstanceSettings = null;

		public InstanseViewController (IntPtr handle) : base (handle)
		{
		}

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();

      this.LocalizeUI();
    }

    public override void ViewWillAppear(bool animated)
    {
      base.ViewWillAppear(animated);

      this.PreferredContentSize = popoverSize;
    }

    private void LocalizeUI()
    {
      
    }

    partial void ApplyButtonTouched (Foundation.NSObject sender)
    {
      this.currentInstanceSettings = new InstanceSettings.InstanceSettings();
      this.currentInstanceSettings.InstanceUrl = this.InstanceUrlTextField.Text;
      this.currentInstanceSettings.InstanceDataBase = this.DatabaseTextField.Text;
      this.currentInstanceSettings.InstanceLanguage = this.LanguageTextField.Text;

      ISitecoreWebApiSession session = this.currentInstanceSettings.GetSession();

      this.PerformSegue(unwindSegieIdentifier, sender);
    }

    private async void SendAuthRequest(ISitecoreWebApiSession session)
    {
      try
      {
//          this.ShowLoader();

        bool response = await session.AuthenticateAsync();

          if (response)
          {
            AlertHelper.ShowLocalizedAlertWithOkOption("Message", "This user exist");
          }
          else
          {
            AlertHelper.ShowLocalizedAlertWithOkOption("Message", "This user not exist");
          }

      }
      catch(Exception e) 
      {
        AlertHelper.ShowLocalizedAlertWithOkOption("Error", e.Message);
      }
      finally
      {
//        BeginInvokeOnMainThread(delegate
//        {
//          this.HideLoader();
//        });
      }
	}

  }
}
