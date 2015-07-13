using System;
using UIKit;

namespace JetStreamIOSFull
{
  public static class ColorHelper
  {
    public static UIColor ColorFromRGB(nfloat r, nfloat g, nfloat b, float alpha)
    {
      nfloat red    = r / 255;
      nfloat green  = g / 255;
      nfloat blue   = b / 255;
      UIColor color = new UIColor (red, green, blue, alpha);

      return color;
    }
  }
}

