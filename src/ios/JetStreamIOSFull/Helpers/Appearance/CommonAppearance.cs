using System;
using UIKit;

namespace JetStreamIOSFull.Apearance
{
  public class CommonAppearance : BaseAppearance
  {
    public CommonAppearance(IColorTheme colorsTheme) : base(colorsTheme)
    {
    }

    public UIImage NavigationBackgroundImage
    {
      get
      { 
        return UIImage.FromBundle("Images.xcassets/BackgroundTextures/NavBarBackgroundDG.png");
      }
    }

    public UIImage NavigationBarLogo
    {
      get
      { 
        return UIImage.FromBundle("Images.xcassets/JetStreamLogo.png");
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

