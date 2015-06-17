using Foundation;
using UIKit;
using JetStreamIOSFull.Helpers;
using System.Drawing;
using System;

namespace JetStreamIOSFull
{
  // The UIApplicationDelegate for the application. This class is responsible for launching the
  // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
  [Register("AppDelegate")]
  public class AppDelegate : UIApplicationDelegate, IUISplitViewControllerDelegate
  {
    // class-level declarations
    private IAppearanceHelper appearanceHelper = new AppearanceHelper();
    private InstanceSettings.InstanceSettings endpoint = new InstanceSettings.InstanceSettings();

    public override UIWindow Window
    {
      get;
      set;
    }
            
    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
      var splitViewController = (UISplitViewController)Window.RootViewController;
      splitViewController.PresentsWithGesture = false;
      splitViewController.PreferredDisplayMode = UISplitViewControllerDisplayMode.PrimaryHidden;
      splitViewController.Delegate = new SplitViewDelegate();
      NSString key = NSString.FromData("_masterColumnWidth",NSStringEncoding.ASCIIStringEncoding);
      splitViewController.SetValueForKey(this.appearanceHelper.MainMenuWidth, key);

      NavigationManagerViewController navigationManager = (NavigationManagerViewController)splitViewController.ViewControllers[1];
      navigationManager.Appearance = this.appearanceHelper;
      navigationManager.Endpoint = this.endpoint;

      navigationManager.MenuButton = splitViewController.DisplayModeButtonItem;
      navigationManager.LoadNavigationFlows();

      this.SetupUI();

      application.SetStatusBarStyle (UIStatusBarStyle.LightContent, false);

      return true;
    }
      
    public void SetupUI()
    {
      UIColor textColor = this.appearanceHelper.NavigationTextColor;

      UINavigationBar.Appearance.TintColor = textColor;
      UIBarButtonItem.Appearance.TintColor = textColor;

      UIImage sourceImage = this.appearanceHelper.NavigationBackgroundImage;
      UIEdgeInsets insets = new UIEdgeInsets(0, 0, 0, 0);
      UIImage backgroundImage = sourceImage.CreateResizableImage(insets);

      UINavigationBar.Appearance.SetBackgroundImage(backgroundImage, UIBarMetrics.Default);
    }

    public override void OnResignActivation(UIApplication application)
    {
      // Invoked when the application is about to move from active to inactive state.
      // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
      // or when the user quits the application and it begins the transition to the background state.
      // Games should use this method to pause the game.
    }

    public override void DidEnterBackground(UIApplication application)
    {
      // Use this method to release shared resources, save user data, invalidate timers and store the application state.
      // If your application supports background exection this method is called instead of WillTerminate when the user quits.
    }

    public override void WillEnterForeground(UIApplication application)
    {
      // Called as part of the transiton from background to active state.
      // Here you can undo many of the changes made on entering the background.
    }

    public override void OnActivated(UIApplication application)
    {
      // Restart any tasks that were paused (or not yet started) while the application was inactive. 
      // If the application was previously in the background, optionally refresh the user interface.
    }

    public override void WillTerminate(UIApplication application)
    {
      // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
    }

    [Export("splitViewController:collapseSecondaryViewController:ontoPrimaryViewController:")]
    public bool CollapseSecondViewController(UISplitViewController splitViewController, UIViewController secondaryViewController, UIViewController primaryViewController)
    {
      if (secondaryViewController.GetType() == typeof(UINavigationController) &&
       ((UINavigationController)secondaryViewController).TopViewController.GetType() == typeof(DetailViewController))
      {
        // Return YES to indicate that we have handled the collapse by doing nothing; the secondary controller will be discarded.
        return true;
      }
      return false;
    }
  }
}


