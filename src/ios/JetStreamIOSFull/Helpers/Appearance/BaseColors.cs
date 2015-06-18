using System;
using UIKit;

namespace JetStreamIOSFull.Apearance
{
  public class BaseColorsTheme : IColorTheme
  {
    public BaseColorsTheme()
    {
    }

    public UIColor OrangeColor
    {
      get
      { 
        return ColorHelper.ColorFromRGB(231, 132, 41, 1);
      }
    }

    public UIColor DarkGreyColor
    {
      get
      { 
        return ColorHelper.ColorFromRGB(56, 56, 56, 1);
      }
    }

    public UIColor MediumGreyColor
    {
      get
      { 
        return ColorHelper.ColorFromRGB(95, 95, 95, 1);
      }
    }

    public UIColor WhiteColor
    {
      get
      { 
        return ColorHelper.ColorFromRGB(255, 255, 255, 1);
      }
    }

    public UIColor BlackColor
    {
      get
      { 
        return ColorHelper.ColorFromRGB(0, 0, 0, 1);
      }
    }
  }
}

