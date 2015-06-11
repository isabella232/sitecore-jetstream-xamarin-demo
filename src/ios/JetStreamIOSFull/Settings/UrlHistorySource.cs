using System;
using UIKit;
using Foundation;

namespace JetStreamIOSFull
{
  public class UrlHistorySource : UITableViewSource
  {
    private const string CELL_IDENTIFIER = "UrlHistoryCell";
    private HistoryManager historyManager;

    public delegate void UrlSelected(string url);
    public event UrlSelected onUrlSelected;

    public UrlHistorySource(HistoryManager historyManager)
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

      if (cell == null)
      {
        cell = new UITableViewCell(UITableViewCellStyle.Default, CELL_IDENTIFIER); 
      }

      string item = this.historyManager.UrlAtIndex((nuint)indexPath.Row);

      cell.TextLabel.Text = item;

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
        onUrlSelected(this.historyManager.UrlAtIndex((nuint)indexPath.Row));
      }
    }
  }
}