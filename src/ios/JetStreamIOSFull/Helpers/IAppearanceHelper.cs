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

    UIImage NavigationBarLogo
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

