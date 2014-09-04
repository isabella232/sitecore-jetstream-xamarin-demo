using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace JetStreamIOS
{
  public class SearchAirportsDelegate : UITableViewDelegate
  {
    public SearchAirportsDelegate()
    {
    }

    public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
    {
      AirportsTable.RowSelected (indexPath.Row);
    }

    public SearchAirportTableViewController AirportsTable;

  }
}

