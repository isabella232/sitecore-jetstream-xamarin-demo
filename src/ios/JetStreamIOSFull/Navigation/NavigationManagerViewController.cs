
using System;

using Foundation;
using UIKit;
using JetStreamIOSFull.Helpers;
using JetStreamIOSFull.BaseVC;
using JetStreamIOSFull.Menu;
using JetStreamIOSFull.MapUI;

namespace JetStreamIOSFull.Navigation
{

  public partial class NavigationManagerViewController : BaseViewController
  {
    public UIBarButtonItem menuButton;

    private UINavigationController CurentActiveFlow;
    private static UIStoryboard infoStoryboard = UIStoryboard.FromName("InfoControllers", null);

    private UIView overlayView = null;
    private bool canShowMenu = true;

    private UIScreenEdgePanGestureRecognizer LeftEdgePanGesture;

    public NavigationManagerViewController(IntPtr handle) : base(handle)
    {
      Title = NSBundle.MainBundle.LocalizedString("NavigationRoot", "NavigationRoot");
    }

    private void InitializeOverlay()
    {
      this.overlayView = new UIView(this.View.Bounds);
    }

    public void ShowOverlay()
    {
      this.overlayView.BackgroundColor = UIColor.Black.ColorWithAlpha(0.0f);

      UIView.Animate(1, () =>
      {
        this.overlayView.BackgroundColor = UIColor.Black.ColorWithAlpha(0.6f);
      });

      this.View.AddSubview(this.overlayView);
    }

    public void HideOverlay()
    {
      this.overlayView.RemoveFromSuperview();
      this.canShowMenu = true;
    }
      
    public UIBarButtonItem MenuButton
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

    public void NavigationItemSelected(MenuItemTypes type)
    {
      this.NavigateToTabAtIndex(type);
    }
      
    public override void DidReceiveMemoryWarning()
    {
      base.DidReceiveMemoryWarning();
    }

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();
      this.InitializeOverlay();

      this.LeftEdgePanGesture = new UIScreenEdgePanGestureRecognizer();
      this.LeftEdgePanGesture.Edges = UIRectEdge.Left;
      this.LeftEdgePanGesture.AddTarget(() => LeftEdgePanDetected(this.LeftEdgePanGesture));
      this.PanView.AddGestureRecognizer(this.LeftEdgePanGesture);

    }

    private void LeftEdgePanDetected (UIScreenEdgePanGestureRecognizer sender)
    {
      if (this.canShowMenu == true)
      {
        this.canShowMenu = false;
        this.ShowMasterPanel();
      }
    }

    private void ShowMasterPanel()
    {
      this.MenuButton.Target.PerformSelector(this.MenuButton.Action, this.MenuButton);
    }

    public void LoadNavigationFlows()
    {
      this.NavigateToTabAtIndex(MenuItemTypes.Destinations);
    }

    private void InitializeFlow(UINavigationController flow)
    {
      BaseViewController root = flow.TopViewController as BaseViewController;
      root.Appearance = this.Appearance;
      root.Endpoint = this.Endpoint;
    }

    private void NavigateToTabAtIndex(MenuItemTypes type)
    {
      string vcName = null;

      switch (type)
      {
      case MenuItemTypes.Destinations:
        {
          vcName = "MapFlowInitialNavigationController";
                
          break;
        }
      case MenuItemTypes.Settings:
        {
          vcName = "SettingsFlowInitialNavigationController";
          break;
        }
      case MenuItemTypes.About:
        {
          vcName = "AboutFlowInitialNavigationController";
          break;
        }
      case MenuItemTypes.OnlineCheckin:
        {
          vcName = "CheckInFlowInitialNavigationController";
          break;
        }
      case MenuItemTypes.FlightStatus:
        {
          vcName = "FlightStatusFlowInitialNavigationController";
          break;
        }
      default:
        {
          vcName = "MapFlowInitialNavigationController";
          break;
        }
      }

      UINavigationController NewFlow = infoStoryboard.InstantiateViewController(vcName) as UINavigationController;;
      this.InitializeFlow(NewFlow);

      BaseViewController vc = NewFlow.TopViewController as BaseViewController;
      vc.NavigationItem.LeftBarButtonItem = this.MenuButton;
      vc.NavigationItem.LeftItemsSupplementBackButton = true;
      vc.RealNavigationItem = this.NavigationItem;
      vc.BaseVC = this;
      if (this.CurentActiveFlow != null)
      {
        this.CurentActiveFlow.View.RemoveFromSuperview();
      }

      this.View.AddSubview(NewFlow.View);
      this.CurentActiveFlow = NewFlow;      
      this.View.BringSubviewToFront(this.PanView);

    }

  }
}

