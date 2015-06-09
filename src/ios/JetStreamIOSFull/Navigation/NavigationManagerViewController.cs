﻿
using System;

using Foundation;
using UIKit;
using JetStreamIOSFull.Helpers;

namespace JetStreamIOSFull
{
  public partial class NavigationManagerViewController : BaseViewController
  {
    public UIBarButtonItem menuButton;

    private UINavigationController MapFlow;
    private UINavigationController SettingsFlow;
    private UINavigationController CurentActiveFlow;

    private UIView overlayView = null;

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
    }

    public void LoadNavigationFlows()
    {
      this.MapFlow = (UINavigationController)this.Storyboard.InstantiateViewController("MapFlowInitialNavigationController");
      this.SettingsFlow = (UINavigationController)this.Storyboard.InstantiateViewController("SettingsFlowInitialNavigationController");

      this.InitializeFlow(this.MapFlow);
      this.InitializeFlow(this.SettingsFlow);

      this.NavigateToTabAtIndex(0);
    }

    private void InitializeFlow(UINavigationController flow)
    {
      BaseViewController root = flow.TopViewController as BaseViewController;
      root.Appearance = this.Appearance;
      root.Endpoint = this.Endpoint;
    }

    private void NavigateToTabAtIndex(MenuItemTypes type)
    {
      UINavigationController NewFlow = null;

      switch (type)
      {
      case MenuItemTypes.Destinations:
        {
          NewFlow = this.MapFlow;
          break;
        }
      case MenuItemTypes.Settings:
        {
          NewFlow = this.SettingsFlow;
          break;
        }
      default:
        {
          NewFlow = this.MapFlow;
          break;
        }
      }

      UIViewController vc = NewFlow.TopViewController;
      vc.NavigationItem.LeftBarButtonItem = this.MenuButton;
      vc.NavigationItem.LeftItemsSupplementBackButton = false;

      if (this.CurentActiveFlow != null)
      {
        this.CurentActiveFlow.View.RemoveFromSuperview();
      }

      this.View.AddSubview(NewFlow.View);
      this.CurentActiveFlow = NewFlow;

    }

  }
}

