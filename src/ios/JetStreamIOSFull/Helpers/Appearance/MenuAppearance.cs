using System;
using Foundation;
using UIKit;

namespace JetStreamIOSFull.Apearance
{
  public class MenuAppearance : BaseAppearance
  {
    public MenuAppearance(IColorTheme colorsTheme) : base(colorsTheme)
    {
    }

    public NSNumber MenuWidth
    {
      get
      { 
        return new NSNumber(260);
      }
    }

    public UIImage ProfileCellBackground
    {
      get
      { 
        return UIImage.FromBundle("Images.xcassets/BackgroundTextures/ProfileBackground.png");
      }
    }

    public UIColor ProfileTextColor
    {
      get
      { 
        return this.ColorsTheme.WhiteColor;
      }
    }

    public UIColor BackgroundColor
    {
      get
      { 
        return this.ColorsTheme.WhiteColor;
      }
    }

    public UIColor IconColor
    {
      get
      { 
        return this.ColorsTheme.DarkGreyColor;
      }
    }

    public UIColor SeparatorColor
    {
      get
      { 
        return UIColor.LightGray;
      }
    }

    public UIColor TextColor
    {
      get
      { 
        return this.ColorsTheme.DarkGreyColor;
      }
    }

    public UIColor SelectionColor
    {
      get
      { 
        return this.ColorsTheme.OrangeColor;
      }
    }
  }
}

