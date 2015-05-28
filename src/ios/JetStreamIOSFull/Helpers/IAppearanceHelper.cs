using System;
using UIKit;

namespace JetStreamIOSFull
{
  public interface IAppearanceHelper
  {

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
  }
}

