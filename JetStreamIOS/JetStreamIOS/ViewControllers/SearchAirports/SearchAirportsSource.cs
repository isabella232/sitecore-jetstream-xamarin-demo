﻿
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace JetStreamIOS
{
  public class blaSource : UITableViewSource
  {
    public blaSource()
    {
    }

    public override int NumberOfSections(UITableView tableView)
    {
      // TODO: return the actual number of sections
      return 1;
    }

    public override int RowsInSection(UITableView tableview, int section)
    {
      // TODO: return the actual number of items in the section
      return 1;
    }

    public override string TitleForHeader(UITableView tableView, int section)
    {
      return "Header";
    }

    public override string TitleForFooter(UITableView tableView, int section)
    {
      return "Footer";
    }

    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
    {
      var cell = tableView.DequeueReusableCell (blaCell.Key) as blaCell;
      if (cell == null)
        cell = new blaCell ();
      
      // TODO: populate the cell with the appropriate data based on the indexPath
      cell.DetailTextLabel.Text = "DetailsTextLabel";
      
      return cell;
    }
  }
}

