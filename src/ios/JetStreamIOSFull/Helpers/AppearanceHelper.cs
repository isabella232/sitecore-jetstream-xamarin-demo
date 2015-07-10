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
    public readonly CheckInAppearance CheckIn;
    public readonly FlightStatusAppearance FlightStatus;
   
    public AppearanceHelper()
    {
      IColorTheme colorTheme = new BaseColorsTheme();

      this.About        = new AboutAppearance(colorTheme);
      this.Common       = new CommonAppearance(colorTheme); 
      this.Map          = new MapAppearance(colorTheme);
      this.Menu         = new MenuAppearance(colorTheme);
      this.Settings     = new SettingsAppearance(colorTheme);
      this.CheckIn      = new CheckInAppearance(colorTheme);
      this.FlightStatus = new FlightStatusAppearance(colorTheme);
    }
  }
}

