using System;
using UIKit;
using MapKit;
using CoreLocation;
using Foundation;

namespace JetStreamIOSFull.Helpers
{
  public class AppearanceHelper  : IAppearanceHelper
  {
    private UIImage destinationPlaceholder;

    public AppearanceHelper()
    {
      UIImage image = UIImage.FromBundle("Images.xcassets/DestinationPlaceholder.png");
      UIImage resizedImage = ImageHelper.ResizeImage(image, this.DestinationIconSize, this.DestinationIconSize);  

      this.destinationPlaceholder = resizedImage;
    }

    #region MAP_SETTINGS

    public MKCoordinateRegion MapInitialRegion
    {
      get
      { 
        //Europe coordinates
        CLLocationCoordinate2D coord = new CLLocationCoordinate2D(50.14873266, 12.941536949);
        MKCoordinateSpan span = new MKCoordinateSpan(16, 35);
        MKCoordinateRegion region = new MKCoordinateRegion(coord, span);
        return region;
      }
    }

    #endregion MAP_SETTINGS

    #region SIZES

    public NSNumber MainMenuWidth
    {
      get
      { 
        return new NSNumber(180);
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

    #endregion SIZES

    #region COLORS

    public UIImage NavigationBackgroundImage
    {
      get
      { 
        return UIImage.FromBundle("Images.xcassets/NavBarBackground.png");
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
        return this.WhiteColor;
        //return this.MediumGreyColor;
        //return this.OrangeColor;
        //return this.DarkGreyColor;
      }
    }


    public UIColor MenuBackgroundColor
    {
      get
      { 
        //return this.MediumGreyColor;
        return this.WhiteColor;
        //return this.OrangeColor;
        //return this.DarkGreyColor;
      }
    }

    public UIColor MenuTextColor
    {
      get
      { 
        return this.DarkGreyColor;
        //return this.WhiteColor;
      }
    }

    public UIColor SelectionColor
    {
      get
      { 
        //return this.DarkGreyColor;
        //return this.WhiteColor;
        return this.OrangeColor;
      }
    }

    #endregion COLORS

    #region COLORS_CONSTANTS
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

    #endregion COLORS_CONSTANTS
  }
}

