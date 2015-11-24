using System;
using UIKit;
using Foundation;
using SitecoreInstanceSettingsIOS.Persistence;
using SitecoreInstanceSettings;
using System.Collections;
using System.Collections.Generic;
using InstanceSettingsIOS;

namespace SitecoreInstanceSettingsIOS
{
  public class InstanceSettinsController : ISitecoreInstanceSettings
  {
    private ConfigStorage configStorage;
    private WeakReference weakSettingsViewController;

    //Labels
    private string anonymousLabel = "Anonymous";
    private string instanceLoginLabel = "Login";
    private string instancePasswordLabel = "Password";

    private string instanceUrlLabel = "Instance Url";
    private string instanceSiteNameLabel = "Site Name";
    private string instanceDatabaseLabel = "Database";
    private string instanceLanguageLabel = "Language";

    private string pathToItemsLabel = "Path to items";
    //Placeholders
    private string instanceLoginPlaceholder = "Type login";
    private string instancePasswordPlaceholder = "Type password";

    private string instanceUrlPlaceholder = "Type instance Url";
    private string instanceSiteNamePlaceholder = "Type site name";
    private string instanceDatabasePlaceholder = "Type database";
    private string instanceLanguagePlaceholder = "Type language";

    private string pathToItemsPlaceholder = "Type path to items";

    //Buttons
    private string cancelButtonTitle = "Cancel";
    private string doneButtonTitle = "Done";

    public delegate void OnSettingsChangedHandler(string eventKey);
    public event OnSettingsChangedHandler SettingsChangedInViewControllerEvent;

    public InstanceSettinsController()
    {
      this.configStorage = new ConfigStorage();
    }

    private static bool UserInterfaceIdiomIsPhone
    {
      get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
    }

    public ISitecoreInstanceSettings ShallowCopy()
    {
      //FIXME: implement this!!!
      return this;
    }

    public bool IsAnonymous()
    {
      string password = this.InstancePassword;
      bool isAnonymous = ((password == null) || (password.Equals("")));
      password = null;
      return isAnonymous;
    }

    public bool IsEmpty()
    {
      bool isEmpty = ((this.InstanceUrl == null) || (this.InstanceUrl.Equals("")));
      return isEmpty;
    }

    public void OnSettingsChanged()
    {
      if (this.SettingsChangedInViewControllerEvent != null)
      {
        this.SettingsChangedInViewControllerEvent(null);
      }
    }

    public UIViewController GetSettinsViewControllerAccordingToCurrentSettings()
    {
      SettingsViewController settingsViewController = new SettingsViewController(this);
      this.weakSettingsViewController = new WeakReference(settingsViewController);
      return settingsViewController;
    }

    private bool SettingsViewControllerExists
    {
      get
      { 
        return this.weakSettingsViewController != null && this.weakSettingsViewController.IsAlive;
      }
    }

    #region Config Properties

    public string InstanceUrl
    {
      get
      { 
        return this.configStorage.InstanceUrl;
      }
      set
      { 
        this.configStorage.InstanceUrl = value;
        if (this.SettingsViewControllerExists)
        {
          (this.weakSettingsViewController.Target as SettingsViewController).InstanceUrlTextField.Text = value;
        }
      }
    }

    public string InstanceSiteName
    {
      get
      { 
        return this.configStorage.InstanceSiteName;
      }
      set
      { 
        this.configStorage.InstanceSiteName = value;
        if (this.SettingsViewControllerExists)
        {
          (this.weakSettingsViewController.Target as SettingsViewController).SiteTextField.Text = value;
        }
      }
    }

    public string InstanceDatabase
    {
      get
      { 
        return this.configStorage.InstanceDatabase;
      }
      set
      { 
        this.configStorage.InstanceDatabase = value;
        if (this.SettingsViewControllerExists)
        {
          (this.weakSettingsViewController.Target as SettingsViewController).DatabaseTextField.Text = value;
        }
      }
    }

    public string InstanceLanguage
    {
      get
      { 
        return this.configStorage.InstanceLanguage;
      }
      set
      { 
         this.configStorage.InstanceLanguage = value;
        if (this.SettingsViewControllerExists)
        {
          (this.weakSettingsViewController.Target as SettingsViewController).LanguageTextField.Text = value;
        }
      }
    }

    public string InstanceLogin
    {
      get
      { 
        return this.configStorage.InstanceLogin;
      }
      set
      { 
         this.configStorage.InstanceLogin = value;
        if (this.SettingsViewControllerExists)
        {
          (this.weakSettingsViewController.Target as SettingsViewController).LoginTextField.Text = value;
        }
      }
    }

    public string InstancePassword
    {
      get
      { 
        return this.configStorage.InstancePassword;
      }
      set
      { 
        this.configStorage.InstancePassword = value;
        if (this.SettingsViewControllerExists)
        {
          (this.weakSettingsViewController.Target as SettingsViewController).PasswordTextField.Text = value;
        }
      }
    }

    public string PathToItems
    {
      get
      { 
        return this.configStorage.PathToItems;
      }
      set
      { 
        this.configStorage.PathToItems = value;
        if (this.SettingsViewControllerExists)
        {
          (this.weakSettingsViewController.Target as SettingsViewController).PathToItemsTextField.Text = value;
        }
      }
    }

    #endregion Config Properties

    #region Placeholders

    public string InstanceUrlPlaceholder
    {
      get
      { 
        return NSBundle.MainBundle.LocalizedString(this.instanceUrlPlaceholder, null);
      }
      set
      { 
        this.instanceUrlPlaceholder = value;
        if (this.SettingsViewControllerExists)
        {
          (this.weakSettingsViewController.Target as SettingsViewController).InstanceUrlTextField.Placeholder = this.InstanceUrlPlaceholder;
        }
      }
    }

    public string InstanceSiteNamePlaceholder
    {
      get
      { 
        return NSBundle.MainBundle.LocalizedString(this.instanceSiteNamePlaceholder, null);
      }
      set
      { 
        this.instanceSiteNamePlaceholder = value;
        if (this.SettingsViewControllerExists)
        {
          (this.weakSettingsViewController.Target as SettingsViewController).SiteTextField.Placeholder = this.InstanceSiteNamePlaceholder;
        }
      }
    }

    public string InstanceDatabasePlaceholder
    {
      get
      { 
        return NSBundle.MainBundle.LocalizedString(this.instanceDatabasePlaceholder, null);
      }
      set
      { 
        this.instanceDatabasePlaceholder = value;
        if (this.SettingsViewControllerExists)
        {
          (this.weakSettingsViewController.Target as SettingsViewController).DatabaseTextField.Placeholder = this.InstanceDatabasePlaceholder;
        }
      }
    }

    public string InstanceLanguagePlaceholder
    {
      get
      { 
        return NSBundle.MainBundle.LocalizedString(this.instanceLanguagePlaceholder, null);
      }
      set
      { 
        this.instanceLanguagePlaceholder = value;
        if (this.SettingsViewControllerExists)
        {
          (this.weakSettingsViewController.Target as SettingsViewController).LanguageTextField.Placeholder = this.InstanceLanguagePlaceholder;
        }
      }
    }

    public string InstanceLoginPlaceholder
    {
      get
      { 
        return NSBundle.MainBundle.LocalizedString(this.instanceLoginPlaceholder, null);
      }
      set
      { 
        this.instanceLoginPlaceholder = value;
        if (this.SettingsViewControllerExists)
        {
          (this.weakSettingsViewController.Target as SettingsViewController).LoginTextField.Placeholder = this.InstanceLoginPlaceholder;
        }
      }
    }

    public string InstancePasswordPlaceholder
    {
      get
      { 
        return NSBundle.MainBundle.LocalizedString(this.instancePasswordPlaceholder, null);
      }
      set
      { 
        this.instancePasswordPlaceholder = value;
        if (this.SettingsViewControllerExists)
        {
          (this.weakSettingsViewController.Target as SettingsViewController).PasswordTextField.Placeholder = this.InstancePasswordPlaceholder;
        }
      }
    }

    public string PathToItemsPlaceholder
    {
      get
      { 
        return NSBundle.MainBundle.LocalizedString(this.pathToItemsPlaceholder, null);
      }
      set
      { 
        this.pathToItemsPlaceholder = value;
        if (this.SettingsViewControllerExists)
        {
          (this.weakSettingsViewController.Target as SettingsViewController).PathToItemsTextField.Placeholder = this.PathToItemsPlaceholder;
        }
      }
    }

    #endregion Placeholders

    #region Labels

    public string AnonymousLabel
    {
      get
      { 
        return NSBundle.MainBundle.LocalizedString(this.anonymousLabel, null);
      }
      set
      { 
        this.anonymousLabel = value;
        if (this.SettingsViewControllerExists)
        {
          (this.weakSettingsViewController.Target as SettingsViewController).AnonymousLabel.Text = this.AnonymousLabel;
        }
      }
    }

    public string InstanceUrlLabel
    {
      get
      { 
        return NSBundle.MainBundle.LocalizedString(this.instanceUrlLabel, null);
      }
      set
      { 
        this.instanceUrlLabel = value;
        if (this.SettingsViewControllerExists)
        {
          (this.weakSettingsViewController.Target as SettingsViewController).InstanceUrlLabel.Text = this.InstanceUrlLabel;
        }
      }
    }

    public string InstanceSiteNameLabel
    {
      get
      { 
        return NSBundle.MainBundle.LocalizedString(this.instanceSiteNameLabel, null);
      }
      set
      { 
        this.instanceSiteNameLabel = value;
        if (this.SettingsViewControllerExists)
        {
          (this.weakSettingsViewController.Target as SettingsViewController).SiteLabel.Text = this.InstanceSiteNameLabel;
        }
      }
    }

    public string InstanceDatabaseLabel
    {
      get
      { 
        return NSBundle.MainBundle.LocalizedString(this.instanceDatabaseLabel, null);
      }
      set
      { 
        this.instanceDatabaseLabel = value;
        if (this.SettingsViewControllerExists)
        {
          (this.weakSettingsViewController.Target as SettingsViewController).DatabaseLabel.Text = this.InstanceDatabaseLabel;
        }
      }
    }

    public string InstanceLanguageLabel
    {
      get
      { 
        return NSBundle.MainBundle.LocalizedString(this.instanceLanguageLabel, null);
      }
      set
      { 
        this.instanceLanguageLabel = value;
        if (this.SettingsViewControllerExists)
        {
          (this.weakSettingsViewController.Target as SettingsViewController).LanguageLabel.Text = this.InstanceLanguageLabel;
        }
      }
    }

    public string InstanceLoginLabel
    {
      get
      { 
        return NSBundle.MainBundle.LocalizedString(this.instanceLoginLabel, null);
      }
      set
      { 
        this.instanceLoginLabel = value;
        if (this.SettingsViewControllerExists)
        {
          (this.weakSettingsViewController.Target as SettingsViewController).LoginLabel.Text = this.InstanceLoginLabel;
        }
      }
    }

    public string InstancePasswordLabel
    {
      get
      { 
        return NSBundle.MainBundle.LocalizedString(this.instancePasswordLabel, null);
      }
      set
      { 
        this.instancePasswordLabel = value;
        if (this.SettingsViewControllerExists)
        {
          (this.weakSettingsViewController.Target as SettingsViewController).PasswordLabel.Text = this.InstancePasswordLabel;
        }
      }
    }

    public string PathToItemsLabel
    {
      get
      { 
        return NSBundle.MainBundle.LocalizedString(this.pathToItemsLabel, null);
      }
      set
      { 
        this.instancePasswordLabel = value;
        if (this.SettingsViewControllerExists)
        {
          (this.weakSettingsViewController.Target as SettingsViewController).PathToItemsLabel.Text = this.PathToItemsLabel;
        }
      }
    }

    #endregion Labels

    #region Buttons

    public string DoneButtonTitle
    {
      get
      { 
        return NSBundle.MainBundle.LocalizedString(this.doneButtonTitle, null);
      }
      set
      { 
        this.doneButtonTitle = value;
        if (this.SettingsViewControllerExists)
        {
          UIButton doneButton = (this.weakSettingsViewController.Target as SettingsViewController).DoneButton;
          doneButton.SetTitle(this.DoneButtonTitle, UIControlState.Normal);
        }
      }
    }

    public string CancelButtonTitle
    {
      get
      { 
        return NSBundle.MainBundle.LocalizedString(this.cancelButtonTitle, null);
      }
      set
      { 
        this.cancelButtonTitle = value;
        if (this.SettingsViewControllerExists)
        {
          UIButton doneButton = (this.weakSettingsViewController.Target as SettingsViewController).DoneButton;
          doneButton.SetTitle(this.DoneButtonTitle, UIControlState.Normal);
        }
      }
    }

    #endregion Buttons
  }
}

