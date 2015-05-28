using System;
using System.Collections.Generic;

using Foundation;
using CoreGraphics;
using UIKit;

namespace JetStreamIOSFull
{
  public partial class MasterViewController : UITableViewController
  {
    public NavigationManagerViewController NavigationManager { get; set; }

    private DataSource dataSource;

    public MasterViewController(IntPtr handle) : base(handle)
    {
      PreferredContentSize = new CGSize (100f, 600f);
      ClearsSelectionOnViewWillAppear = true;
    }

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();

      NavigationManager = (NavigationManagerViewController)SplitViewController.ViewControllers[1];

      List<object> objects = new List<object> ();
      objects.Add("Destinations");
      objects.Add("Settings");

      this.dataSource = new DataSource (this, objects);
      TableView.Source = this.dataSource;
    }

    public override void DidReceiveMemoryWarning()
    {
      base.DidReceiveMemoryWarning();
    }

    class DataSource : UITableViewSource
    {
      static readonly NSString CellIdentifier = new NSString ("Cell");

      readonly List<object> objects = new List<object> ();

      readonly MasterViewController controller;

      public DataSource(MasterViewController controller, List<object> objects)
      {
        this.objects = objects;
        this.controller = controller;
      }

      public IList<object> Objects
      {
        get { return objects; }
      }

      public override nint NumberOfSections(UITableView tableView)
      {
        return 1;
      }

      public override nint RowsInSection(UITableView tableview, nint section)
      {
        return objects.Count;
      }
        
      // Customize the appearance of table view cells.
      public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
      {
        var cell = tableView.DequeueReusableCell(CellIdentifier, indexPath);

        cell.TextLabel.Text = objects[indexPath.Row].ToString();

        return cell;
      }

      public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
      {
        UIBarButtonItem hideButton = this.controller.SplitViewController.DisplayModeButtonItem;
        UIApplication.SharedApplication.SendAction(hideButton.Action, hideButton.Target, null, null);

        controller.NavigationManager.NavigationItemSelected(indexPath.Row);

      }
    }
  }
}


