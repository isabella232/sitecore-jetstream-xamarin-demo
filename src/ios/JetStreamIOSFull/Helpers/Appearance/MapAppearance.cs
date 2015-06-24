using System;
using MapKit;
using CoreLocation;
using UIKit;
using JetStreamIOSFull.Helpers;

namespace JetStreamIOSFull.Apearance
{
  public class MapAppearance : BaseAppearance
  {
    private UIImage destinationPlaceholder;

    public MapAppearance(IColorTheme colorsTheme) : base(colorsTheme)
    {
      UIImage image = UIImage.FromBundle("Images.xcassets/DestinationPlaceholder.png");
      UIImage resizedImage = ImageHelper.ResizeImage(image, this.DestinationIconSize, this.DestinationIconSize);  
      if (resizedImage != null)
      {
        this.destinationPlaceholder = resizedImage;
      }
    }
      
    public MKCoordinateRegion InitialRegion
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

    #region DESTINATION_ICON

    public UIImage DestinationPlaceholder
    {
      get
      { 
        return this.destinationPlaceholder;
      }
    }

    public UIColor DestinationTextColor
    {
      get
      { 
        return this.ColorsTheme.WhiteColor;
      }
    }

    public UIColor DestinationBorderColor
    {
      get
      { 
        return this.ColorsTheme.MediumGreyColor;
      }
    }

    public UIColor DestinationSelectedBorderColor
    {
      get
      { 
        return this.ColorsTheme.MediumGreyColor;
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

    public UIFont DestinationDetailsDescriptionFont
    {
      get
      { 
        return UIFont.SystemFontOfSize(18);
      }
    }

    #endregion DESTINATION_ICON

    #region GROUP_LABEL

    public UIColor GroupLabelColor
    {
      get
      { 
        return this.ColorsTheme.OrangeColor;
      }
    }

    public UIColor GroupTextColor
    {
      get
      { 
        return this.ColorsTheme.DarkGreyColor;
      }
    }

    public nfloat GroupLabelSize
    {
      get
      { 
        return 30;
      }
    }

    public nfloat GroupLabelFontSize
    {
      get
      { 
        return 20;
      }
    }
    #endregion GROUP_LABEL
  }
}

