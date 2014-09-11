

namespace JetStreamIOS.ViewControllers.FlightsTable
{
  using System;
  using System.Linq;
  using System.Collections.Generic;

  using MonoTouch.UIKit;
  using MonoTouch.Foundation;

  using JetStreamCommons.Flight;


  public class FlightsTableViewEnumerableDataSource : UITableViewDataSource
  {
    private IEnumerable<IJetStreamFlight> flights;

    public FlightsTableViewEnumerableDataSource(IEnumerable<IJetStreamFlight> flights)
    {
      this.flights = flights;
    }
      
    public override int NumberOfSections(UITableView tableView)
    {
      return 1;
    }

    public override int RowsInSection(UITableView tableView, int section)
    {
      return this.flights.Count();
    }

    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
    {
      UITableViewCell newCell = tableView.DequeueReusableCell("FlightCell");
      if (null == newCell)
      {
        newCell = new UITableViewCell(UITableViewCellStyle.Default, "FlightCell");
      }
      newCell.TextLabel.Text = flights.ToArray()[indexPath.Row].Price.ToString("C");

      return newCell;
    }
  }
}

