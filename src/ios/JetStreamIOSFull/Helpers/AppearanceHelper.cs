using System;
using UIKit;
using MapKit;
using CoreLocation;

namespace JetStreamIOSFull.Helpers
{
  public class AppearanceHelper  : IAppearanceHelper
  {
    private UIImage destinationPlaceholder;

    public AppearanceHelper()
    {
      UIImage image = UIImage.FromBundle("Images.xcassets/DestinationPlaceholder.png");
      UIImage resizedImage = ImageResize.ResizeImage(image, this.DestinationIconSize, this.DestinationIconSize);  

      this.destinationPlaceholder = resizedImage;
    }

    public MKCoordinateRegion MapInitialRegion
    {
      get
      { 
        //Cuba coordinates
        CLLocationCoordinate2D coord = new CLLocationCoordinate2D(20.269922, -74.448036);
        MKCoordinateSpan span = new MKCoordinateSpan(40, 40);
        MKCoordinateRegion region = new MKCoordinateRegion(coord, span);
        return region;
      }
    }

    public UIImage DestinationPlaceholder
    {
      get
      { 
        return destinationPlaceholder;
      }
    }

    public nfloat HiddenLabelSize
    {
      get
      { 
        return 30;
      }
    }

    public nfloat HiddenLabelFontSize
    {
      get
      { 
        return 20;
      }
    }

    public float DestinationIconSize
    {
      get
      { 
        return 70;
      }
    }

    public float DestinationIconBorderSize
    {
      get
      { 
        return 1;
      }
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
      nfloat red    = r / 255;
      nfloat green  = g / 255;
      nfloat blue   = b / 255;
      UIColor color = new UIColor (red, green, blue, alpha);

      return color;
    }
  }
}

