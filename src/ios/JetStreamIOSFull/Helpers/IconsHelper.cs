using System;
using UIKit;

namespace JetStreamIOSFull.Helpers
{
  public static class IconsHelper
  {
    public static UIImage MenuDestinationIcon
    {
      get
      { 
        return UIImage.FromBundle("Images.xcassets/MenuIcons/DestinationIcon.png");
      }
    }

    public static UIImage MenuSettingsIcon
    {
      get
      { 
        return UIImage.FromBundle("Images.xcassets/MenuIcons/SettingsIcon.png");
      }
    }

    public static UIImage MenuAboutIcon
    {
      get
      { 
        return UIImage.FromBundle("Images.xcassets/MenuIcons/AboutIcon.png");
      }
    }

    public static UIImage MenuFlightStatusIcon
    {
      get
      { 
        return UIImage.FromBundle("Images.xcassets/MenuIcons/FlightStatusIcon.png");
      }
    }

    public static UIImage MenuOnlineCheckinIcon
    {
      get
      { 
        return UIImage.FromBundle("Images.xcassets/MenuIcons/OnlineCheckinIcon.png");
      }
    }
  }
}

