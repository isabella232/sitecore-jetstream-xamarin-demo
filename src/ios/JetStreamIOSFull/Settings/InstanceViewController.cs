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
  partial class InstanceViewController : BaseViewController
	{
    private static CGSize popoverSize = new CGSize(400, 300);
    private static string unwindSegieIdentifier = "UnwindToSettingsViewController";

    private NSObject keyboardDown;
    private NSObject keyboardUp;

    public delegate void OnInstanceAddedHandler(InstanceSettings.InstanceSettings instance);
    public event OnInstanceAddedHandler InstanceAddedEvent;

		public InstanceViewController (IntPtr handle) : base (handle)
		{
		}

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();
      this.LocalizeUI();
      this.AddKeyboardObservers();
    }
      
    public override void ViewWillAppear(bool animated)
    {
      base.ViewWillAppear(animated);

      this.PreferredContentSize = popoverSize;
    }

    public override void ViewWillDisappear(bool animated)
    {
      base.ViewWillDisappear(animated);
      this.RemoveKeyboardObservers();
    }

    private void LocalizeUI()
    {
      this.ApplyButton.SetTitle (NSBundle.MainBundle.LocalizedString("APPLY_BUTTON_TITLE", null), UIControlState.Normal);
      this.CancelButton.SetTitle(NSBundle.MainBundle.LocalizedString("CANCEL_BUTTON_TITLE", null), UIControlState.Normal);

      this.DatabaseLabel.Text    = NSBundle.MainBundle.LocalizedString("DATABASE_LABEL_TITLE", null);
      this.InstanceUrlLabel.Text = NSBundle.MainBundle.LocalizedString("INSTANCE_URL_LABEL_TITLE", null);
      this.LanguageLabel.Text    = NSBundle.MainBundle.LocalizedString("LANGUAGE_LABEL_TITLE", null);
      this.LoginLabel.Text       = NSBundle.MainBundle.LocalizedString("LOGIN_LABEL_TITLE", null);
      this.PasswordLabel.Text    = NSBundle.MainBundle.LocalizedString("PASSWORD_LABEL_TITLE", null);

      this.PasswordTextField.Placeholder    = NSBundle.MainBundle.LocalizedString("PASSWORD_PLACEHOLDER_VALUE", null);
      this.DatabaseTextField.Placeholder    = NSBundle.MainBundle.LocalizedString("DATABASE_PLACEHOLDER_VALUE", null);
      this.InstanceUrlTextField.Placeholder = NSBundle.MainBundle.LocalizedString("INSTANCE_URL_PLACEHOLDER_VALUE", null);
      this.LanguageTextField.Placeholder    = NSBundle.MainBundle.LocalizedString("LANGUAGE_PLACEHOLDER_VALUE", null);
      this.LoginTextField.Placeholder       = NSBundle.MainBundle.LocalizedString("LOGIN_PLACEHOLDER_VALUE", null);
    }

    partial void ApplyButtonTouched (Foundation.NSObject sender)
    {
      InstanceSettings.InstanceSettings instanceSettings = new InstanceSettings.InstanceSettings();
      instanceSettings.InstanceUrl      = this.InstanceUrlTextField.Text;
      instanceSettings.InstanceDataBase = this.DatabaseTextField.Text;
      instanceSettings.InstanceLanguage = this.LanguageTextField.Text;
      instanceSettings.InstanceLogin    = this.LoginTextField.Text;
      instanceSettings.InstancePassword = this.PasswordTextField.Text;

      this.SendAuthRequest(instanceSettings);
    }

    private async void SendAuthRequest(InstanceSettings.InstanceSettings instanceSettings)
    {
      
      try
      {
        ISitecoreWebApiSession session = instanceSettings.GetSession();

        this.ShowLoader();

        bool authenticated = await session.AuthenticateAsync();

        if (authenticated)
        {
          if (this.InstanceAddedEvent != null)
          {
            this.InstanceAddedEvent(instanceSettings);
          }

          this.PerformSegue(unwindSegieIdentifier, null);
        }
        else
        {
          this.ShowCommoNetworkErrorAlert();
        }

      }
      catch(Exception e) 
      {
        this.ShowCommoNetworkErrorAlert();
      }
      finally
      {
        this.HideLoader();
      }
	  }

    private void ShowCommoNetworkErrorAlert()
    {
      AlertHelper.ShowLocalizedAlertWithOkOption("MESSAGE", "INSTANCE_UNAVAILABLE");
    }

    #region Keyboard Processing

    private void AddKeyboardObservers()
    {
      UIDevice thisDevice = UIDevice.CurrentDevice;

      if (thisDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone)
      {
        this.keyboardDown = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyBoardDownNotification);
        this.keyboardUp = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidShowNotification, KeyBoardUpNotification);
      }
    }

    private void RemoveKeyboardObservers()
    {
      if (this.keyboardDown != null)
      {
        NSNotificationCenter.DefaultCenter.RemoveObserver(this.keyboardDown);
      }
      if (this.keyboardUp != null)
      {
        NSNotificationCenter.DefaultCenter.RemoveObserver(this.keyboardUp);
      }
    }

    private void KeyBoardDownNotification(NSNotification notification)
    {
      UIEdgeInsets contentInsets = UIEdgeInsets.Zero;
      this.ScrollView.ContentInset = contentInsets;
      this.ScrollView.ScrollIndicatorInsets = contentInsets;
    }

    private void KeyBoardUpNotification(NSNotification notification)
    {
      NSValue keyboardSizeValue = notification.UserInfo.ObjectForKey(new NSString("UIKeyboardFrameBeginUserInfoKey")) as NSValue;
      CGRect keyboardSize = keyboardSizeValue.CGRectValue;
      UIEdgeInsets contentInsets = new UIEdgeInsets (0, 0, keyboardSize.Height, 0);
      this.ScrollView.ContentInset = contentInsets;
      this.ScrollView.ScrollIndicatorInsets = contentInsets;
    }

    #endregion Keyboard Processing
  }
}
