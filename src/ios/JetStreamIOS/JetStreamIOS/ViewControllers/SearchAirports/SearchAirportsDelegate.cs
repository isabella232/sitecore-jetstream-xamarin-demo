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
      this.AirportsTable.RowSelected (indexPath.Row);
    }

    public override void DecelerationStarted(UIScrollView scrollView)
    {
      this.AirportsTable.HideSearchKeyboard();
    }

    public SearchAirportTableViewController AirportsTable {get; set;}

  }
}

