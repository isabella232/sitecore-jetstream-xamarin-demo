using System;

using Foundation;
using UIKit;
using CoreGraphics;
using JetStreamIOSFull.BaseVC;
using InstanceSettings;

namespace JetStreamIOSFull.Settings
{
  public partial class SettingsViewControlle : BaseViewController, IUITextFieldDelegate
	{
    private static string instanceSegueIdentifier = "ShowInstancePopover";
    private UrlHistorySource source;

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

      this.source = new UrlHistorySource(this.InstancesManager);
      this.HistoryTableView.Source = source;
    }

    private void LocalizeUI()
    {
      this.AddButton.SetTitle (NSBundle.MainBundle.LocalizedString("ADD_INSTANCE_BUTTON_TITLE", null), UIControlState.Normal);
      this.Title = NSBundle.MainBundle.LocalizedString("SETTINGS_SCREEN_TITLE", null);
    }

    public override void ViewWillAppear(bool animated)
    {
      base.ViewWillAppear(animated);
      this.HistoryTableView.ReloadData();

      if (this.InstancesManager.Count > 0)
      {
        NSIndexPath indexPath = NSIndexPath.FromRowSection(this.InstancesManager.ActiveIndex, 0);
        this.HistoryTableView.SelectRow(indexPath, true, UITableViewScrollPosition.Middle);
      }
    }

    private void InstaneAdded(InstanceSettings.InstanceSettings instance)
    {
      this.InstancesManager.AddInstance(instance);
      this.HistoryTableView.ReloadData();
    }

    public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
    {
      if (segue.Identifier.Equals(instanceSegueIdentifier))
      {
        InstanceViewController instanceVC = segue.DestinationViewController as InstanceViewController;
        instanceVC.InstanceAddedEvent += this.InstaneAdded;
      }
    }

    partial void UnwindToSettingsViewController(UIStoryboardSegue segue)
    {
    }

	}
}
