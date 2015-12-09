using System;
using UIKit;

namespace JetStreamIOSFull.Apearance
{
  public class FlightStatusAppearance : BaseAppearance
  {
    public FlightStatusAppearance(IColorTheme colorsTheme) : base(colorsTheme)
    {
    }

    public UIImage TopImage
    {
      get
      { 
        return UIImage.FromBundle("Palms");
      }
    }
  }
}

