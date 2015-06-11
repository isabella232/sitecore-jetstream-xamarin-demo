using System;

using Foundation;
using UIKit;
using CoreGraphics;

namespace JetStreamIOSFull
{
  public partial class SettingsViewControlle : BaseViewController
	{
    private NSObject keyboardDown;
    private HistoryManager historyManager = new HistoryManager();

		public SettingsViewControlle (IntPtr handle) : base (handle)
		{
      
    }

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();

      this.Title = NSBundle.MainBundle.LocalizedString("SETTINGS_SCREEN_TITLE", null);
      this.BackgroundImageView.Image = this.Appearance.SettingsBackground;

      //Hack to hide separators for empty cells
      this.HistoryTableView.TableFooterView = new UIView (new CGRect (0, 0, 0, 0));

      UrlHistorySource source = new UrlHistorySource(this.historyManager);
      source.onUrlSelected += UrlFromHistorySelected;

      this.HistoryTableView.Source = source;
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

      this.HistoryTableView.ReloadData();
    }

    private void KeyBoardDownNotification(NSNotification notification)
    {
      this.UrlTextField.ResignFirstResponder();
      Console.WriteLine("Keyboard is going down");
    }

    public override void ViewWillDisappear(bool animated)
    {
      base.ViewWillDisappear(animated);
      NSNotificationCenter.DefaultCenter.RemoveObserver (this.keyboardDown);

      this.Endpoint.InstanceUrl = this.UrlTextField.Text;
      this.UrlTextField.BecomeFirstResponder();

      this.historyManager.AddUrlToHistory(this.UrlTextField.Text);
    }
	}
}
