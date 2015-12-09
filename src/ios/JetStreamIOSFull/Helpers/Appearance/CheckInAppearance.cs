using System;
using UIKit;

namespace JetStreamIOSFull.Apearance
{
  public class CheckInAppearance : BaseAppearance
  {
    public CheckInAppearance(IColorTheme colorsTheme) : base(colorsTheme)
    {
    }

    public UIImage TopImage
    {
      get
      { 
        return UIImage.FromBundle("Skiing");
      }
    }
  }
}

