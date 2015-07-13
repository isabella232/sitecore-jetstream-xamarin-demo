using System;
using UIKit;

namespace JetStreamIOSFull.Apearance
{
  public class AboutAppearance : BaseAppearance
  {
    public AboutAppearance(IColorTheme colorsTheme) : base(colorsTheme)
    {
    }

    public UIImage Background
    {
      get
      { 
        return UIImage.FromBundle("Images.xcassets/BackgroundTextures/AboutBackground.jpg");
      }
    }

    public UIFont DescriptionFont
    {
      get
      { 
        return UIFont.SystemFontOfSize(18.0f);
      }
    }
  }
}

