using System;

using Foundation;
using UIKit;
using JetStreamIOSFull.Helpers;
using JetStreamIOSFull.Navigation;
using JetStreamIOSFull.Menu;

namespace JetStreamIOSFull
{
	public partial class MainSplitViewController : UISplitViewController
	{
    private AppearanceHelper appearanceHelper = new AppearanceHelper();
    private InstanceSettings.InstanceSettings endpoint = new InstanceSettings.InstanceSettings();

		public MainSplitViewController (IntPtr handle) : base (handle)
		{
      Console.WriteLine("MainSplitViewController");
      NSString key = NSString.FromData("_masterColumnWidth",NSStringEncoding.ASCIIStringEncoding);
      this.SetValueForKey(this.appearanceHelper.Menu.MenuWidth, key);
		}

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();
      Console.WriteLine("ViewDidLoad");
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
      navigationManager.Endpoint = this.endpoint;

      navigationManager.MenuButton = this.DisplayModeButtonItem;
      navigationManager.LoadNavigationFlows();
    }

    private void InitializeMenu()
    {
      MasterViewController menuController = (MasterViewController)this.ViewControllers[0];
      menuController.Appearance = this.appearanceHelper;
    }
	}
}
