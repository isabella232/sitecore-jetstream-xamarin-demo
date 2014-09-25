namespace JetStreamIOS
{
  using System;
  using System.Threading.Tasks;

  using MonoTouch.Foundation;
  using MonoTouch.UIKit;

  using JetStreamIOS.Helpers;
  using JetStreamCommons;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Session;

  using  SecureStringPasswordProvider.iOS;


	public partial class SettingsViewController : UIViewController
	{
    private InstanceSettings instanceSettings;
    private bool isAuthValid = false;

    public SettingsViewController (IntPtr handle) : base (handle)
    {
      this.Title = NSBundle.MainBundle.LocalizedString ("Settings", "Settings");

      // @adk : they use NSUserDefaults singleton
      this.instanceSettings = new InstanceSettings();
    }

    private void HideAllKeyboards()
    {
      this.InstanceUrlField.ResignFirstResponder();
      this.PasswordField.ResignFirstResponder();
      this.LoginField.ResignFirstResponder();
      this.SiteField.ResignFirstResponder();
      this.DatabaseField.ResignFirstResponder();
      this.LanguageField.ResignFirstResponder();
    }

    public bool HideKeyboard(MonoTouch.UIKit.UITextField sender)
    {
      sender.ResignFirstResponder();
      return true;
    }

    public override void ViewDidLoad ()
    {
      base.ViewDidLoad ();
      // Perform any additional setup after loading the view, typically from a nib.

      this.DatabaseField.Hidden = true;
      this.SiteField.Hidden = true;
      this.LanguageField.Hidden = true;


      this.InstanceUrlField.Placeholder = NSBundle.MainBundle.LocalizedString("Instance Url", null);
      this.PasswordField.Placeholder    = NSBundle.MainBundle.LocalizedString("Password", null);
      this.LoginField.Placeholder       = NSBundle.MainBundle.LocalizedString("Login", null);
      this.SiteField.Placeholder        = NSBundle.MainBundle.LocalizedString("Site", null);
      this.DatabaseField.Placeholder    = NSBundle.MainBundle.LocalizedString("Database", null);
      this.LanguageField.Placeholder    = NSBundle.MainBundle.LocalizedString("Language", null);

      this.InstanceUrlField.ShouldReturn  = this.HideKeyboard;
      this.PasswordField.ShouldReturn    = this.HideKeyboard;
      this.LoginField.ShouldReturn      = this.HideKeyboard;
      this.SiteField.ShouldReturn       = this.HideKeyboard;
      this.DatabaseField.ShouldReturn   = this.HideKeyboard;
      this.LanguageField.ShouldReturn   = this.HideKeyboard;
    }

    public override void ViewWillAppear(bool animated)
    {
      base.ViewWillAppear(animated);
      this.isAuthValid = false;

      this.InstanceUrlField.Text = this.instanceSettings.InstanceUrl;
      this.PasswordField.Text = this.instanceSettings.InstancePassword;
      this.LoginField.Text = this.instanceSettings.InstanceLogin;
      this.SiteField.Text = this.instanceSettings.InstanceSite;
      this.DatabaseField.Text = this.instanceSettings.InstanceDataBase;
      this.LanguageField.Text = this.instanceSettings.InstanceLanguage;
    }

    public override void ViewWillDisappear(bool animated)
    {
      base.ViewWillDisappear(animated);

      if (!this.IsUserChangedSettings())
      {
        return;
      }
      else if (this.isAuthValid)
      {
        InstanceSettings settings = this.instanceSettings;

        settings.InstanceUrl = this.InstanceUrlField.Text;
        settings.InstancePassword = this.PasswordField.Text;
        settings.InstanceLogin = this.LoginField.Text;
        settings.InstanceSite = this.SiteField.Text;
        settings.InstanceDataBase = this.DatabaseField.Text;
        settings.InstanceLanguage = this.LanguageField.Text;
      }
      else
      {
        AlertHelper.ShowLocalizedAlertWithOkOption("SETTINGS_NOT_APPLIED_ALERT_TITLE", "SETTINGS_NOT_APPLIED_ALERT_MESSAGE");
      }
    }

    private async Task<bool> TryAuthenticateSessionWithAlert(ISitecoreWebApiReadonlySession session)
    {
      bool isAuthenticatedSuccessfully = await session.AuthenticateAsync();
      this.isAuthValid = isAuthenticatedSuccessfully;

      if (isAuthenticatedSuccessfully)
      {
        AlertHelper.ShowLocalizedAlertWithOkOption("SETTINGS_AUTH_SUCCEEDED_ALERT_TITLE", "SETTINGS_AUTH_SUCCEEDED_ALERT_MESSAGE");
      }
      else
      {
        AlertHelper.ShowLocalizedAlertWithOkOption("SETTINGS_AUTH_FAILED_ALERT_TITLE", "SETTINGS_AUTH_FAILED_ALERT_MESSAGE");
      }

      return isAuthenticatedSuccessfully; 
    }

    async partial void OnCheckConnectionButtonTapped(MonoTouch.Foundation.NSObject sender)
    {
      this.HideAllKeyboards();

      string userName = this.LoginField.Text;
      string password = this.PasswordField.Text;

      bool isSessionAssumedAnonymous = string.IsNullOrWhiteSpace(userName);
      if (isSessionAssumedAnonymous)
      {
        this.isAuthValid = true;
        AlertHelper.ShowLocalizedAlertWithOkOption("SETTINGS_ANONYMOUS_USER_ALERT_TITLE", "SETTINGS_ANONYMOUS_USER_ALERT_MESSAGE");
      }
      else
      {
        try
        {
          using (var credentials = new SecureStringPasswordProvider(userName, password))
          using (
            var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(this.InstanceUrlField.Text)
            .Credentials(credentials)
            .DefaultDatabase(this.DatabaseField.Text)
            .DefaultLanguage(this.LanguageField.Text)
            .Site(this.SiteField.Text)
            .BuildReadonlySession())
          {
            await this.TryAuthenticateSessionWithAlert(session);
          }
        }
        catch 
        {
          AlertHelper.ShowLocalizedAlertWithOkOption("FAILURE_ALERT_TITLE", "SETTINGS_AUTH_FAILED_ALERT_MESSAGE");
        }
      }
    }

    private bool IsUserChangedSettings()
    {
      InstanceSettings settings = this.instanceSettings;

      bool isNoChangeOccured = 
        string.Equals(settings.InstanceUrl, this.InstanceUrlField.Text) &&
        string.Equals(settings.InstancePassword, this.PasswordField.Text) &&
        string.Equals(settings.InstanceLogin, this.LoginField.Text) &&
        string.Equals(settings.InstanceSite, this.SiteField.Text) &&
        string.Equals(settings.InstanceDataBase, this.DatabaseField.Text) &&
        string.Equals(settings.InstanceLanguage, this.LanguageField.Text);

      return !isNoChangeOccured;
    }
  }
}
