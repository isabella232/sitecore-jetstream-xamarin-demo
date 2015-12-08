using System;
using UIKit;
using JetStreamIOSFull.Helpers;
using CoreGraphics;
using Foundation;
using InstanceSettings;

namespace JetStreamIOSFull.BaseVC
{
  public partial class BaseViewController  : UIViewController
  {
    private AppearanceHelper appearanceHelper;
    private InstancesManager instanceManager;

    public UINavigationItem RealNavigationItem;
    public UIViewController BaseVC;

    private LoadingOverlay loadingOverlay;

    public BaseViewController(IntPtr handle) : base(handle)
    {

    }

    public void ShowLoader()
    {
      BeginInvokeOnMainThread(delegate
      { 
        UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
        this.loadingOverlay = new LoadingOverlay (this.View.Bounds, NSBundle.MainBundle.LocalizedString("Loading...", null));
        View.Add (loadingOverlay);
      });
    }

    public void HideLoader()
    {
      BeginInvokeOnMainThread(delegate
      { 
        UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
        if (this.loadingOverlay != null)
        {
          this.loadingOverlay.Hide ();
        }
      });
    }

    public virtual InstancesManager InstancesManager
    {
      get
      {
        if (this.instanceManager == null)
        {
          throw new NullReferenceException("Endpoint value must be initialized");
        }

        return this.instanceManager;
      }
      set
      { 
        this.instanceManager = value;
      }
    }

    public AppearanceHelper Appearance
    {
      get
      {
        if (this.appearanceHelper == null)
        {
          throw new NullReferenceException("Appearance value must be initialized");

        }

        return this.appearanceHelper;
      }
      set
      { 
        this.appearanceHelper = value;
        this.InitializeNavBarLogo();
      }
    }

    private void InitializeNavBarLogo()
    {
      if (this.NavigationItem != null)
      {
        UIImage image = this.appearanceHelper.Common.NavigationBarLogo;
        UIImageView imageView = new UIImageView(new CGRect(0, 0, image.Size.Width, image.Size.Height));
        imageView.Image = image;
        this.NavigationItem.TitleView = imageView;
      }
    }
  }
}

