namespace Jetstream.Map
{
  using Android.Gms.Maps.Model;
  using Android.Gms.Maps.Utils.Clustering;

  public class ClusterItem : Java.Lang.Object, IClusterItem
  {
    public LatLng Position { get; private set; }

    public string Title { get; private set; }

    public string ImageUrl { get; private set; }

    public ClusterItem(LatLng position, string title, string imageUrl)
    {
      this.Position = position;
      this.Title = title;
      this.ImageUrl = imageUrl;
    }
  }
}