using System;
using UIKit;
using MapKit;
using CoreLocation;
using Foundation;
using JetStreamIOSFull.Apearance;

namespace JetStreamIOSFull.Helpers
{
  public class AppearanceHelper
  {
    public readonly AboutAppearance About;
    public readonly CommonAppearance Common; 
    public readonly MapAppearance Map;
    public readonly MenuAppearance Menu;
    public readonly SettingsAppearance Settings;
   
    public AppearanceHelper()
    {
      IColorTheme colorTheme = new BaseColorsTheme();

      this.About = new AboutAppearance(colorTheme);
      this.Common = new CommonAppearance(colorTheme); 
      this.Map = new MapAppearance(colorTheme);
      this.Menu = new MenuAppearance(colorTheme);
      this.Settings = new SettingsAppearance(colorTheme);
    }
  }
}

