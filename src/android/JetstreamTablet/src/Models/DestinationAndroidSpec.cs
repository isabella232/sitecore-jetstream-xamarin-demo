namespace Jetstream.Models
{
  /// <summary>
  /// This class is created for passing data between Android components.
  /// It merges all it's members values into one string and parses back after transfer. 
  /// </summary>
  public class DestinationAndroidSpec
  {
    public const char SplitSymbol = '|';

    public string ImageUrl { get; private set; }
    public string Overview { get; private set; }
    public string DisplayName { get; private set; }

    public DestinationAndroidSpec(string imageUrl, string displayName, string overview)
    {
      this.ImageUrl = imageUrl;
      this.DisplayName = displayName;
      this.Overview = overview;
    }

    public DestinationAndroidSpec(string rawValue)
    {
      var list = rawValue.Split(SplitSymbol);

      this.ImageUrl = list[0];
      this.Overview = list[1];
      this.DisplayName = list[2];
    }
  }
}
