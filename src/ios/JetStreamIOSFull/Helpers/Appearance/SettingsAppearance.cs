using System;
using UIKit;

namespace JetStreamIOSFull.Apearance
{
  public class SettingsAppearance : BaseAppearance
  {
    public SettingsAppearance(IColorTheme colorsTheme) : base(colorsTheme)
    {
    }

    public UIImage SettingsBackground
    {
      get
      { 
        return UIImage.FromBundle("SettingsBackground");
      }
    }
  }
}

