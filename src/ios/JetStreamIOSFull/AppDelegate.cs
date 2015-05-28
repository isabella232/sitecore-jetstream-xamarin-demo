using Foundation;
using UIKit;
using JetStreamIOSFull.Helpers;

namespace JetStreamIOSFull
{
  // The UIApplicationDelegate for the application. This class is responsible for launching the
  // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
  [Register("AppDelegate")]
  public class AppDelegate : UIApplicationDelegate, IUISplitViewControllerDelegate
  {
    // class-level declarations
    private IAppearanceHelper appearanceHelper = new AppearanceHelper();

    public override UIWindow Window
    {
      get;
      set;
    }

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
      // Code to start the Xamarin Test Cloud Agent
//      #if ENABLE_TEST_CLOUD
//      Xamarin.Calabash.Start();
//      #endif

      // Override point for customization after application launch.

      var splitViewController = (UISplitViewController)Window.RootViewController;
      splitViewController.PresentsWithGesture = false;
      splitViewController.PreferredDisplayMode = UISplitViewControllerDisplayMode.PrimaryHidden;
      splitViewController.WeakDelegate = this;

      NavigationManagerViewController navigationManager = (NavigationManagerViewController)splitViewController.ViewControllers[1];
      navigationManager.Appearance = this.appearanceHelper;
      navigationManager.MenuButton = splitViewController.DisplayModeButtonItem;
      navigationManager.LoadNavigationFlows();

      return true;
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


