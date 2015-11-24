
using System;

using Foundation;
using UIKit;

namespace SitecoreInstanceSettingsIOS
{
  public partial class SettingsViewController : UIViewController
  {
    private InstanceSettinsController settingsController;

    static bool UserInterfaceIdiomIsPhone
    {
      get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
    }

    public SettingsViewController(InstanceSettinsController settinsController)
      : base(UserInterfaceIdiomIsPhone ? "SettingsViewController_iPhone" : "SettingsViewController_iPad", null)
    {
      this.settingsController = settinsController;
    }

    private void FillUi()
    {
      //Values
      this.LoginTextField.Text        = this.settingsController.InstanceLogin;
      this.PasswordTextField.Text     = this.settingsController.InstancePassword;

      this.InstanceUrlTextField.Text  = this.settingsController.InstanceUrl;
      this.SiteTextField.Text         = this.settingsController.InstanceSiteName;
      this.DatabaseTextField.Text     = this.settingsController.InstanceDatabase;
      this.LanguageTextField.Text     = this.settingsController.InstanceLanguage;

      this.PathToItemsTextField.Text  = this.settingsController.PathToItems;

      //Placeholders
      this.LoginTextField.Placeholder        = this.settingsController.InstanceLoginPlaceholder;
      this.PasswordTextField.Placeholder     = this.settingsController.InstancePasswordPlaceholder;

      this.InstanceUrlTextField.Placeholder  = this.settingsController.InstanceUrlPlaceholder;
      this.SiteTextField.Placeholder         = this.settingsController.InstanceSiteNamePlaceholder;
      this.DatabaseTextField.Placeholder     = this.settingsController.InstanceDatabasePlaceholder;
      this.LanguageTextField.Placeholder     = this.settingsController.InstanceLanguagePlaceholder;

      this.PathToItemsTextField.Placeholder  = this.settingsController.PathToItemsPlaceholder;

      //Labels
      this.AnonymousLabel.Text    = this.settingsController.AnonymousLabel;
      this.LoginLabel.Text        = this.settingsController.InstanceLoginLabel;
      this.PasswordLabel.Text     = this.settingsController.InstancePasswordLabel;

      this.InstanceUrlLabel.Text  = this.settingsController.InstanceUrlLabel;
      this.SiteLabel.Text         = this.settingsController.InstanceSiteNameLabel;
      this.DatabaseLabel.Text     = this.settingsController.InstanceDatabaseLabel;
      this.LanguageLabel.Text     = this.settingsController.InstanceLanguageLabel;

      this.PathToItemsLabel.Text     = this.settingsController.PathToItemsLabel;

      //Buttons
      this.DoneButton.SetTitle(this.settingsController.DoneButtonTitle, UIControlState.Normal);
      this.CancelButton.SetTitle(this.settingsController.CancelButtonTitle, UIControlState.Normal);
    }

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();

      this.FillUi();
    }

    partial void DoneButtonTouched (UIKit.UIButton sender)
    {
      this.ProceedNewValues();
      this.DismissViewControllerAsync(true);
    }

    partial void CancelButtonTouched (UIKit.UIButton sender)
    {
      this.DismissViewControllerAsync(true);
    }
   
    partial void AnonymousSwitchValueChanged (UIKit.UISwitch sender)
    {
      if (sender.On)
      {
        this.settingsController.InstancePassword = "";
        this.settingsController.InstanceLogin = "";
      }

      this.LoginTextField.Enabled = !sender.On;
      this.PasswordTextField.Enabled = !sender.On;

    }

    private void ProceedNewValues()
    {
      bool settingsChangedFlag = false;

      if (!this.settingsController.InstanceUrl.Equals(this.InstanceUrlTextField.Text))
      {
        settingsChangedFlag = true;
        this.settingsController.InstanceUrl = this.InstanceUrlTextField.Text;
      }

      if (!this.settingsController.InstanceSiteName.Equals(this.SiteTextField.Text))
      {
        settingsChangedFlag = true;
        this.settingsController.InstanceSiteName = this.SiteTextField.Text;
      }

      if (!this.settingsController.InstanceDatabase.Equals(this.DatabaseTextField.Text))
      {
        settingsChangedFlag = true;
        this.settingsController.InstanceDatabase = this.DatabaseTextField.Text;
      }

      if (!this.settingsController.InstanceLanguage.Equals(this.LanguageTextField.Text))
      {
        settingsChangedFlag = true;
        this.settingsController.InstanceLanguage = this.LanguageTextField.Text;
      }

      //order matters!!!
      if (!this.settingsController.InstanceLogin.Equals(this.LoginTextField.Text))
      {
        settingsChangedFlag = true;
        this.settingsController.InstanceLogin = this.LoginTextField.Text;
      }

      if (!this.settingsController.InstancePassword.Equals(this.PasswordTextField.Text))
      {
        settingsChangedFlag = true;
        this.settingsController.InstancePassword = this.PasswordTextField.Text;
      }

      if (!this.settingsController.PathToItems.Equals(this.PathToItemsTextField.Text))
      {
        settingsChangedFlag = true;
        this.settingsController.PathToItems = this.PathToItemsTextField.Text;
      }

      if (settingsChangedFlag)
      {
        this.settingsController.OnSettingsChanged();
      }
    }

  }
}

