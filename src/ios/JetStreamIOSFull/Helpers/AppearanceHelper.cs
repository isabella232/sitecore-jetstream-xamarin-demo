using System;
using UIKit;

namespace JetStreamIOSFull.Helpers
{
  public class AppearanceHelper
  {
    public nfloat HiddenLabelSize = 30;
    public nfloat HiddenLabelFontSize = 20;
    public float DestinationIconSize = 90;
    public float DestinationIconBorderSize = 1;

    public AppearanceHelper()
    {
    }

    public UIColor MenuBackgroundColor
    {
      get
      { 
        return this.DarkGreyColor;
      }
    }

    public UIColor MenuTextColor
    {
      get
      { 
        return this.WhiteColor;
      }
    }

    public UIColor SelectionColor
    {
      get
      { 
        return this.OrangeColor;
      }
    }

    public UIColor OrangeColor
    {
      get
      { 
        return this.ColorFromRGB(231, 132, 41, 1);
      }
    }

    public UIColor DarkGreyColor
    {
      get
      { 
        return this.ColorFromRGB(56, 56, 56, 1);
      }
    }

    public UIColor MediumGreyColor
    {
      get
      { 
        return this.ColorFromRGB(95, 95, 95, 1);
      }
    }

    public UIColor WhiteColor
    {
      get
      { 
        return this.ColorFromRGB(255, 255, 255, 1);
      }
    }

    public UIColor BlackColor
    {
      get
      { 
        return this.ColorFromRGB(0, 0, 0, 1);
      }
    }

    private UIColor ColorFromRGB(nfloat r, nfloat g, nfloat b, float alpha)
    {
      nfloat red = r / 255;
      nfloat green = g / 255;
      nfloat blue = b / 255;
      UIColor color =  new UIColor (red, green, blue, alpha);
      return color;
    }
  }
}

