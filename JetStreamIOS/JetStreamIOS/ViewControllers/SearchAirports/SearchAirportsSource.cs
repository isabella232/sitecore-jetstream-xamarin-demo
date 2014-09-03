
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Sitecore.MobileSDK.API.Items;
using System.Collections.Generic;

namespace JetStreamIOS
{
  public class SearchAirportsSource : UITableViewSource
  {
    public SearchAirportsSource()
    {
    }

    public override int NumberOfSections(UITableView tableView)
    {
      // TODO: return the actual number of sections
      return 1;
    }

    public override int RowsInSection(UITableView tableview, int section)
    {
      if (null == this.Items)
      {
        return 0;
      }

      return this.Items.ResultCount;
    }

    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
    {
      var cell = tableView.DequeueReusableCell ("SearchAirportsSourceCell") as UITableViewCell;
      if (cell == null)
        cell = new UITableViewCell ();
      
      ISitecoreItem item = Items[indexPath.Row];
      cell.TextLabel.Text = item.DisplayName;
      
      return cell;
    }

    public ScItemsResponse Items;
  }
}

