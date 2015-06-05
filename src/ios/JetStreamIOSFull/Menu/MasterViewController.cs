using System;
using System.Collections.Generic;

using Foundation;
using CoreGraphics;
using UIKit;
using JetStreamIOSFull.Helpers;

namespace JetStreamIOSFull
{
  public partial class MasterViewController : UITableViewController
  {
    public NavigationManagerViewController NavigationManager { get; set; }
    public AppearanceHelper ah = new AppearanceHelper();
    private DataSource dataSource;

    public MasterViewController(IntPtr handle) : base(handle)
    {
      PreferredContentSize = new CGSize (100f, 600f);
      ClearsSelectionOnViewWillAppear = true;
      this.Title = "";
    }

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();

      NavigationManager = (NavigationManagerViewController)SplitViewController.ViewControllers[1];

      this.TableView.SeparatorColor = ah.MenuTextColor;

      this.BuildMenu();
    }

    private void BuildMenu()
    {
      List<IMenuItem> menuItems = new List<IMenuItem> ();

      menuItems.Add(new MenuItem("About", IconsHelper.MenuAboutIcon, MenuItemTypes.About));
      menuItems.Add(new MenuItem("Destinations", IconsHelper.MenuDestinationIcon, MenuItemTypes.Destinations));
      menuItems.Add(new MenuItem("Flight Status", IconsHelper.MenuFlightStatusIcon, MenuItemTypes.FlightStatus));
      menuItems.Add(new MenuItem("Online Checkin", IconsHelper.MenuOnlineCheckinIcon, MenuItemTypes.OnlineCheckin));
      menuItems.Add(new MenuItem("Settings", IconsHelper.MenuSettingsIcon, MenuItemTypes.Settings));

      this.dataSource = new DataSource (this, menuItems);
      this.TableView.Source = this.dataSource;

//      this.SelectMenuItem(true, 0);
    }

    private void SelectMenuItem(bool selected, int row)
    {
      NSIndexPath path = NSIndexPath.FromRowSection(row, 0);
      UITableViewCell cell = this.TableView.CellAt(path);
      cell.SetSelected(selected, true);
    }

    public override void DidReceiveMemoryWarning()
    {
      base.DidReceiveMemoryWarning();
    }

    class DataSource : UITableViewSource
    {
      static readonly NSString CellIdentifier = new NSString ("Cell");
      readonly List<IMenuItem> objects;
      readonly MasterViewController controller;

      private MainMenuCell prevSelectedCell = null;

      public DataSource(MasterViewController controller, List<IMenuItem> objects)
      {
        this.objects = objects;
        this.controller = controller;
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
        MainMenuCell castedCell = cell as MainMenuCell;

        castedCell.SelectedTintColor = this.controller.ah.SelectionColor;
        castedCell.DefaultTintColor = this.controller.ah.MenuTextColor;
        castedCell.BackgroundColor = this.controller.ah.MenuBackgroundColor;
          
        IMenuItem menuItem = objects[indexPath.Row];

        castedCell.Title = menuItem.Title;
        castedCell.Image = menuItem.Image;

        return cell;
      }

      public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
      {
        if (prevSelectedCell != null)
        {
          prevSelectedCell.SetSelected(false, true);
        }

        var cell = tableView.CellAt(indexPath);
        MainMenuCell castedCell = cell as MainMenuCell;
        prevSelectedCell = castedCell;
        castedCell.SetSelected(true, true);

        UIBarButtonItem hideButton = this.controller.SplitViewController.DisplayModeButtonItem;
        UIApplication.SharedApplication.SendAction(hideButton.Action, hideButton.Target, null, null);

        IMenuItem menuItem = this.objects[indexPath.Row];

        controller.NavigationManager.NavigationItemSelected(menuItem.Type);

      }
    }
  }
}


