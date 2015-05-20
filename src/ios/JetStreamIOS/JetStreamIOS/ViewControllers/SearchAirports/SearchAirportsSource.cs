namespace JetStreamIOS
{
  using System;
  using System.Drawing;
  using System.Collections.Generic;

  using MonoTouch.Foundation;
  using MonoTouch.UIKit;

  using JetStreamCommons.Airport;
  using Sitecore.MobileSDK.API.Items;


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

      return this.Items.Count;
    }

    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
    {
      string cellIdentifier = "SearchAirportsSourceCell";
      UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);

      if (cell == null)
      {
        cell = new UITableViewCell (UITableViewCellStyle.Subtitle, cellIdentifier);
      }

      IJetStreamAirport item = this.Items[indexPath.Row];
      cell.TextLabel.Text = item.DisplayName;
      cell.DetailTextLabel.Text = item.City;
      
      return cell;
    }

    public List<IJetStreamAirport> Items { get; set; }
  }
}

