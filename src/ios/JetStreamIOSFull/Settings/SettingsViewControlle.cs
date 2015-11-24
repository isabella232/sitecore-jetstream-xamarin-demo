using System;

using Foundation;
using UIKit;
using CoreGraphics;
using JetStreamIOSFull.BaseVC;

namespace JetStreamIOSFull.Settings
{
  public partial class SettingsViewControlle : BaseViewController, IUITextFieldDelegate
	{
    private NSObject keyboardDown;
    private HistoryManager historyManager = new HistoryManager();

		public SettingsViewControlle (IntPtr handle) : base (handle)
		{
      
    }

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();

      this.LocalizeUI();

      this.BackgroundImageView.Image = this.Appearance.Settings.SettingsBackground;

      //Hack to hide separators for empty cells
      this.HistoryTableView.TableFooterView = new UIView (new CGRect (0, 0, 0, 0));

      UrlHistorySource source = new UrlHistorySource(this.historyManager);
      source.onUrlSelected += UrlFromHistorySelected;

      this.HistoryTableView.Source = source;

      this.UrlTextField.WeakDelegate = this;
    }

    private void LocalizeUI()
    {
      this.Title = NSBundle.MainBundle.LocalizedString("SETTINGS_SCREEN_TITLE", null);
      this.UrlTextField.Placeholder = NSBundle.MainBundle.LocalizedString("URL_TEXT_FIELD_PLACEHOLDER", null);
    }

    private void UrlFromHistorySelected(string url)
    {
      this.UrlTextField.Text = url;
    }

    public override void ViewWillAppear(bool animated)
    {
      base.ViewWillAppear(animated);

      this.keyboardDown = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyBoardDownNotification);

      this.UrlTextField.Text = this.Endpoint.InstanceUrl;
     
      //hack to append history with default url
      if (this.historyManager.Count == 0 && this.UrlTextField.Text.Length > 0)
      {
        this.historyManager.AddUrlToHistory(this.UrlTextField.Text);
      }

      this.HistoryTableView.ReloadData();
    }

    private void KeyBoardDownNotification(NSNotification notification)
    {
      this.UrlTextField.ResignFirstResponder();
    }

    public override void ViewWillDisappear(bool animated)
    {
      base.ViewWillDisappear(animated);
      NSNotificationCenter.DefaultCenter.RemoveObserver (this.keyboardDown);

      this.Endpoint.InstanceUrl = this.UrlTextField.Text;
      this.UrlTextField.ResignFirstResponder();

      this.historyManager.AddUrlToHistory(this.UrlTextField.Text);
    }

    [Export("textFieldShouldReturn:")]
    public bool ShouldReturn(UITextField textField)
    {
      this.UrlTextField.ResignFirstResponder();

      return false;
    }
	}
}
