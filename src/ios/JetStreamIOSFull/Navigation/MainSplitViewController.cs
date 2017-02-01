using System;

using Foundation;
using UIKit;
using JetStreamIOSFull.Helpers;
using JetStreamIOSFull.Navigation;
using JetStreamIOSFull.Menu;
using InstanceSettings;

namespace JetStreamIOSFull
{
	public partial class MainSplitViewController : UISplitViewController
	{
    private AppearanceHelper appearanceHelper = new AppearanceHelper();
    private InstancesManager instancesManager = new InstancesManager();

		public MainSplitViewController (IntPtr handle) : base (handle)
		{
      Console.WriteLine("MainSplitViewController");
      //NSString key = NSString.FromData("_masterColumnWidth",NSStringEncoding.ASCIIStringEncoding);
      //this.SetValueForKey(this.appearanceHelper.Menu.MenuWidth, key);
		}

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();

      this.SetupUI();

      this.InitializeNavigation();
      this.InitializeMenu();
    }

    public void SetupUI()
    {
      UIColor textColor = this.appearanceHelper.Common.NavigationTextColor;

      UINavigationBar.Appearance.TintColor = textColor;
      UIBarButtonItem.Appearance.TintColor = textColor;

      UIImage sourceImage = this.appearanceHelper.Common.NavigationBackgroundImage;
      UIEdgeInsets insets = new UIEdgeInsets(0, 0, 0, 0);
      UIImage backgroundImage = sourceImage.CreateResizableImage(insets);

      UINavigationBar.Appearance.SetBackgroundImage(backgroundImage, UIBarMetrics.Default);
    }

    private void InitializeNavigation()
    {
      NavigationManagerViewController navigationManager = (NavigationManagerViewController)this.ViewControllers[1];
      navigationManager.Appearance = this.appearanceHelper;
      navigationManager.InstancesManager = this.instancesManager;

      navigationManager.MenuButton = this.DisplayModeButtonItem;
      navigationManager.LoadNavigationFlows();
    }

    private void InitializeMenu()
    {
      MasterViewController menuController;

      if (this.ViewControllers [0] is MasterViewController)
      {
        //iPad
        menuController = this.ViewControllers [0] as MasterViewController; 
      }
      else
      {
        //iPhone
        UINavigationController navController = this.ViewControllers [0] as UINavigationController; 
        menuController = navController.ViewControllers [0] as MasterViewController; 
      }

      menuController.Appearance = this.appearanceHelper;
    }
	}
}
