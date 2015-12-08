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
    private NSIndexPath lastCheckedIndexPath = null;

    public UrlHistorySource(InstancesManager instancesManager)
    {
      this.instancesManager = instancesManager;
    }

    public override nint RowsInSection (UITableView tableview, nint section)
    {
      return (nint)this.instancesManager.Count;
    }

    public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
    {
      HistoryTableCell cell = tableView.DequeueReusableCell(CELL_IDENTIFIER) as HistoryTableCell;

      InstanceSettings.InstanceSettings instance = this.instancesManager.InstanceAtIndex(indexPath.Row);

      cell.TextLabel.Text = instance.InstanceUrl;
      cell.DetailTextLabel.Text = instance.InstanceDataBase + " " + instance.InstanceLanguage + " " + instance.InstanceSite;

      bool isCellChecked = (indexPath.Row == this.instancesManager.ActiveIndex);
      cell.SetChecked(isCellChecked);
      if (isCellChecked)
      {
        this.lastCheckedIndexPath = indexPath;
      }

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
      this.RowChecked(tableView, indexPath);
    }

    private void RowChecked(UITableView tableView, NSIndexPath indexPath)
    {
      if (this.lastCheckedIndexPath != null)
      {
        HistoryTableCell lastCheckedCell = tableView.CellAt(this.lastCheckedIndexPath) as HistoryTableCell;
        if (lastCheckedCell != null)
        {
          lastCheckedCell.SetChecked(false);
        }
      }

      HistoryTableCell cell = tableView.CellAt(indexPath) as HistoryTableCell;
      cell.SetChecked(true);
      this.lastCheckedIndexPath = indexPath;

      tableView.DeselectRow(indexPath, true);
    }

    #region Deleting

    public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, Foundation.NSIndexPath indexPath)
    {
      switch (editingStyle) {
      case UITableViewCellEditingStyle.Delete:
        
        this.instancesManager.DeleteInstanceAtIndex(indexPath.Row);
        tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);

        if (this.instancesManager.ActiveIndex > 0)
        {
          NSIndexPath activeIndexPath = NSIndexPath.FromRowSection(this.instancesManager.ActiveIndex, 0);
          this.RowChecked(tableView, activeIndexPath);
        }
        break;
      case UITableViewCellEditingStyle.None:
        Console.WriteLine ("CommitEditingStyle:None called");
        break;
      }

    }

    public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
    {
      return true;
    }

    #endregion Deleting

  }
}