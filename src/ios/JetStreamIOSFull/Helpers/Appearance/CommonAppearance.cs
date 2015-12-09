using System;
using UIKit;

namespace JetStreamIOSFull.Apearance
{
  public class CommonAppearance : BaseAppearance
  {
    private UIDevice thisDevice = UIDevice.CurrentDevice;

    public CommonAppearance(IColorTheme colorsTheme) : base(colorsTheme)
    {
    }

    public UIImage NavigationBackgroundImage
    {
      get
      { 
        return UIImage.FromBundle("NavBarBackgroundDG");
      }
    }

    public UIImage NavigationBarLogo
    {
      get
      { 
        if (this.thisDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone)
        {
          return UIImage.FromBundle("JetStreamLogoPhone");
        }

        return UIImage.FromBundle("JetStreamLogo");
      }
    }

    public UIColor NavigationTextColor
    {
      get
      { 
        return this.ColorsTheme.WhiteColor;
      }
    }
  }
}

