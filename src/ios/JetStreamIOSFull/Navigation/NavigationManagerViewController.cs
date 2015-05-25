
using System;

using Foundation;
using UIKit;

namespace JetStreamIOSFull
{
  public partial class NavigationManagerViewController : UIViewController
  {
    public UIBarButtonItem menuButton;

    private UINavigationController MapFlow;
    private UINavigationController SettingsFlow;
    private UINavigationController CurentActiveFlow;

    public NavigationManagerViewController(IntPtr handle) : base(handle)
    {
      Title = NSBundle.MainBundle.LocalizedString("NavigationRoot", "NavigationRoot");
    }

    public  UIBarButtonItem MenuButton
    {
      get
      { 
        return this.menuButton;
      }
      set
      {
        this.menuButton = value;
        if (this.CurentActiveFlow != null)
        {
          UIViewController vc = this.CurentActiveFlow.TopViewController;
          vc.NavigationItem.LeftBarButtonItem = this.MenuButton;
          vc.NavigationItem.LeftItemsSupplementBackButton = false;
        }
      }
    }
    public void NavigationItemSelected(int index)
    {
      this.NavigateToTabAtIndex(index);
    }
      
    public override void DidReceiveMemoryWarning()
    {
      base.DidReceiveMemoryWarning();
      
    }

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();

      this.MapFlow = (UINavigationController)this.Storyboard.InstantiateViewController("MapFlowInitialNavigationController");
      this.SettingsFlow = (UINavigationController)this.Storyboard.InstantiateViewController("SettingsFlowInitialNavigationController");

      this.NavigateToTabAtIndex(0);

    }

    private void NavigateToTabAtIndex(int index)
    {
      if (this.CurentActiveFlow != null)
      {
        this.CurentActiveFlow.View.RemoveFromSuperview();
        this.CurentActiveFlow = null;
      }

      switch (index)
      {
        case 0:
        {
          this.CurentActiveFlow = this.MapFlow;
          break;
        }
      case 1:
        {
          this.CurentActiveFlow = this.SettingsFlow;
          break;
        }
      default:
        {
          CurentActiveFlow = this.MapFlow;
          break;
        }
      }

      this.View.AddSubview(this.CurentActiveFlow.View);
      UIViewController vc = this.CurentActiveFlow.TopViewController;
      vc.NavigationItem.LeftBarButtonItem = this.MenuButton;
      vc.NavigationItem.LeftItemsSupplementBackButton = false;
    }

  }
}

