using System;
using UIKit;
using MapKit;
using Foundation;

namespace JetStreamIOSFull
{
  public interface IAppearanceHelper
  {
    NSNumber MainMenuWidth
    {
      get;
    }

    MKCoordinateRegion MapInitialRegion
    {
      get;
    }

    UIImage DestinationPlaceholder
    {
      get;
    }

    nfloat HiddenLabelSize
    {
      get;
    }

    nfloat HiddenLabelFontSize
    {
      get;
    }

    float DestinationIconSize
    {
      get;
    }

    float DestinationIconBorderSize
    {
      get;
    }

    UIImage NavigationBackgroundImage
    {
      get;
    }

    UIImage ProfileCellBackground
    {
      get;
    }

    UIImage NavigationBarLogo
    {
      get;
    }

    UIImage SettingsBackground
    {
      get;
    }

    UIColor NavigationTextColor
    {
      get;
    }

    UIColor MenuBackgroundColor
    {
      get;
    }

    UIColor MenuSeparatorColor
    {
      get;
    }

    UIColor MenuIconColor
    {
      get;
    }

    UIColor MenuTextColor
    {
      get;
    }

    UIColor SelectionColor
    {
      get;
    }

    UIColor OrangeColor
    {
      get;
    }

    UIColor DarkGreyColor
    {
      get;
    }

    UIColor MediumGreyColor
    {
      get;
    }

    UIColor WhiteColor
    {
      get;
    }

    UIColor BlackColor
    {
      get;
    }
  }
}

