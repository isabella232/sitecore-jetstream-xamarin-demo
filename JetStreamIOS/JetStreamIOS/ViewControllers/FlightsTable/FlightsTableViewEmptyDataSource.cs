using MonoTouch.Foundation;

namespace JetStreamIOS
{
  using System;
  using MonoTouch.UIKit;

  public class FlightsTableViewEmptyDataSource : UITableViewDataSource
  {
    public override int NumberOfSections(UITableView tableView)
    {
      return 1;
    }

    public override int RowsInSection(UITableView tableView, int section)
    {
        return 1;
    }

    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
    {
      return this.GetEmptyCell(tableView, indexPath);
    }

    private UITableViewCell GetEmptyCell(UITableView tableView, NSIndexPath indexPath)
    {
      NSString reuseIdentifier = new NSString("EmptyCell");
      UITableViewCell newCell = tableView.DequeueReusableCell(reuseIdentifier);
      if (null == newCell)
      {
        newCell = new UITableViewCell(UITableViewCellStyle.Default, reuseIdentifier);
      }

      newCell.TextLabel.Text = NSBundle.MainBundle.LocalizedString("NO_FLIGHTS_FOUND", null);
      return newCell;
    }

  }
}

