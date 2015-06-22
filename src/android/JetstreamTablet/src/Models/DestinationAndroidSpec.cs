namespace Jetstream.Models
{
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
