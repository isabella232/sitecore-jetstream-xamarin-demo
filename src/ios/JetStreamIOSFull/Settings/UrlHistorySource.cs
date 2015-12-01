using System;
using UIKit;
using Foundation;
using InstanceSettings;

namespace JetStreamIOSFull.Settings
{
  public class UrlHistorySource : UITableViewSource
  {
    private const string CELL_IDENTIFIER = "UrlHistoryCell";
    private InstancesManager historyManager;

    public delegate void UrlSelected(InstanceSettings.InstanceSettings instance);
    public event UrlSelected onUrlSelected;

    public UrlHistorySource(InstancesManager historyManager)
    {
      this.historyManager = historyManager;
    }

    public override nint RowsInSection (UITableView tableview, nint section)
    {
      return (nint)this.historyManager.Count;;
    }

    public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
    {
      UITableViewCell cell = tableView.DequeueReusableCell(CELL_IDENTIFIER);

      InstanceSettings.InstanceSettings instance = this.historyManager.InstanceAtIndex(indexPath.Row);

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
      if (onUrlSelected != null)
      {
        onUrlSelected(this.historyManager.InstanceAtIndex(indexPath.Row));
      }
    }
  }
}