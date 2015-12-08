using System;
using UIKit;
using Foundation;
using JetStreamIOSFull.Navigation;

namespace JetStreamIOSFull
{
  public class SplitViewDelegate : UISplitViewControllerDelegate
  {
    public SplitViewDelegate()
    {
    }

    public override void WillChangeDisplayMode(UISplitViewController svc, UISplitViewControllerDisplayMode displayMode)
    {
      if (svc.ViewControllers.Length > 1 && svc.ViewControllers [1] is NavigationManagerViewController)
      {
        NavigationManagerViewController detailView = svc.ViewControllers [1] as NavigationManagerViewController;

        if (detailView != null)
        {

          switch (displayMode)
          {
          case UISplitViewControllerDisplayMode.PrimaryOverlay:
            detailView.ShowOverlay();
            break;
          case UISplitViewControllerDisplayMode.PrimaryHidden:
            detailView.HideOverlay();
            break;
          default:
            Console.WriteLine("NothingToDo");
            break;
          }
        }
      }
    }

    public override bool ShouldHideViewController(UISplitViewController svc, UIViewController viewController, UIInterfaceOrientation inOrientation)
    {
      return true;
    }
  }
}

