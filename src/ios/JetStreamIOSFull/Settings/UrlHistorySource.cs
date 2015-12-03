using System;
using UIKit;
using Foundation;
using InstanceSettings;

namespace JetStreamIOSFull.Settings
{
  public class UrlHistorySource : UITableViewSource
  {
    private const string CELL_IDENTIFIER = "UrlHistoryCell";
    private InstancesManager instancesManager;

    public event OnInstanceChangedHandler InstanceChangedEvent;

    public UrlHistorySource(InstancesManager instancesManager)
    {
      this.instancesManager = instancesManager;
    }

    public override nint RowsInSection (UITableView tableview, nint section)
    {
      return (nint)this.instancesManager.Count;;
    }

    public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
    {
      UITableViewCell cell = tableView.DequeueReusableCell(CELL_IDENTIFIER);

      InstanceSettings.InstanceSettings instance = this.instancesManager.InstanceAtIndex(indexPath.Row);

      cell.TextLabel.Text = instance.InstanceUrl;
      cell.DetailTextLabel.Text = instance.InstanceDataBase + " " + instance.InstanceLanguage + " " + instance.InstanceSite;

      return cell;
    }

    public override string TitleForHeader(UITableView tableView, nint section)
    {
      return NSBundle.MainBundle.LocalizedString("URL_HISTORY_SECTION_TITLE", null);;
    }

    public override nfloat GetHeightForHeader(UITableView tableView, nint section)
    {
      return 44;
    }

    public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
    {
      this.instancesManager.ActiveIndex = indexPath.Row;
      if (this.InstanceChangedEvent != null)
      {
        this.InstanceChangedEvent(this.instancesManager.ActiveInstance);
      }
    }

  }
}