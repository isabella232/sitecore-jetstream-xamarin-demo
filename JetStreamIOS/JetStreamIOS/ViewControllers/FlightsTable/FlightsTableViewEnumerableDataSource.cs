

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
    private FlightCell.OnFlightSelectedDelegate onFlightSelectedCallback;

    public FlightsTableViewEnumerableDataSource(
      IEnumerable<IJetStreamFlight> flights,
      FlightCell.OnFlightSelectedDelegate onFlightSelectedCallback)
    {
      this.flights = flights;
      this.onFlightSelectedCallback = onFlightSelectedCallback;
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
      NSString reuseIdentifier = FlightCell.StaticReuseIdentifier();
      FlightCell newCell = tableView.DequeueReusableCell(reuseIdentifier) as FlightCell;
      if (null == newCell)
      {
        newCell = new FlightCell(UITableViewCellStyle.Default, reuseIdentifier);
      }

      IJetStreamFlight flight = flights.ToArray()[indexPath.Row];
      newCell.OnFlightSelected = this.onFlightSelectedCallback;
      newCell.SetModel(flight);

      return newCell;
    }
  }
}

