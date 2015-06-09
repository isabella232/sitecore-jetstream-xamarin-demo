namespace Jetstream.Map
{
  using Android.Gms.Maps.Model;
  using Android.Gms.Maps.Utils.Clustering;
  using JetStreamCommons.Destinations;

  public class ClusterItem : Java.Lang.Object, IClusterItem
  {
    public IDestination Wrapped { get; private set; }
  
    public string ImageUrl { get; private set; }

    public LatLng Position
    {
      get
      {
        return new LatLng(this.Wrapped.Latitude, this.Wrapped.Longitude);
      }
    }

    public string Title
    {
      get
      {
        return this.Wrapped.DisplayName;
      }
    }

    public ClusterItem(IDestination wrapped, string imageUrl)
    {
      this.Wrapped = wrapped;
      this.ImageUrl = imageUrl;
    }
  }
}