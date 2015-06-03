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

      this.BuildMenu();
    }

    private void BuildMenu()
    {

      List<IMenuItem> objects = new List<IMenuItem> ();

      UIImage image = UIImage.FromBundle("Images.xcassets/ic_airplanemode_active_black.png");

     
      objects.Add(new MenuItem("Destinations", image));
      objects.Add(new MenuItem("Settings", image));

      objects.Add(new MenuItem("Blablabla", image));
      objects.Add(new MenuItem("Some other", image));
      objects.Add(new MenuItem("Search Flight", image));
      objects.Add(new MenuItem("etc", image));

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
      readonly List<IMenuItem> objects;
      readonly MasterViewController controller;

      private MainMenuCell prevSelectedCell = null;

      AppearanceHelper ah = new AppearanceHelper();

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

        castedCell.SelectedTintColor = ah.SelectionColor;
        castedCell.DefaultTintColor = ah.MenuTextColor;
        castedCell.BackgroundColor = ah.MenuBackgroundColor;
          
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

        controller.NavigationManager.NavigationItemSelected(indexPath.Row);

      }
    }
  }
}


