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
        return UIImage.FromBundle("DestinationIcon");
      }
    }

    public static UIImage MenuSettingsIcon
    {
      get
      { 
        return UIImage.FromBundle("SettingsIcon");
      }
    }

    public static UIImage MenuProfileIcon
    {
      get
      { 
        return UIImage.FromBundle("ProfileIcon");
      }
    }

    public static UIImage MenuAboutIcon
    {
      get
      { 
        return UIImage.FromBundle("AboutIcon");
      }
    }

    public static UIImage MenuFlightStatusIcon
    {
      get
      { 
        return UIImage.FromBundle("FlightStatusIcon");
      }
    }

    public static UIImage MenuOnlineCheckinIcon
    {
      get
      { 
        return UIImage.FromBundle("OnlineCheckInIcon");
      }
    }

    public static UIImage SelectedInstanceIcon
    {
      get
      { 
        return UIImage.FromBundle("InstanceCheckmark");
      }
    }

    public static UIImage UnselectedInstanceIcon
    {
      get
      { 
        return UIImage.FromBundle("InstanceUnchecked");
      }
    }
  }
}

