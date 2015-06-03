namespace Jetstream.Bitmap
{
  using System;
  using Android.Gms.Maps.Model;
  using Android.Gms.Maps.Utils.Clustering;
  using Android.Graphics;
  using Android.Graphics.Drawables;
  using Jetstream.Map;
  using Squareup.Picasso;

  public class MarkerTarget : Java.Lang.Object, ITarget
  {
    private readonly JetstreamClusterRenderer clusterRenderer;
    private readonly ClusterItem clusterItem;
    private readonly ICluster cluster;
    private readonly Func<Bitmap, Bitmap> bitmapConverter;

    public MarkerTarget(JetstreamClusterRenderer clusterRenderer, ICluster cluster, Func<Bitmap, Bitmap> bitmapConverter)
      : this(clusterRenderer, bitmapConverter)
    {
      this.cluster = cluster;
    }

    protected MarkerTarget(JetstreamClusterRenderer clusterRenderer, Func<Bitmap, Bitmap> bitmapConverter)
    {
      this.bitmapConverter = bitmapConverter;
      this.clusterRenderer = clusterRenderer;
    }

    public MarkerTarget(JetstreamClusterRenderer clusterRenderer, ClusterItem clusterItem, Func<Bitmap, Bitmap> bitmapConverter)
      : this(clusterRenderer, bitmapConverter)
    {
      this.clusterItem = clusterItem;
    }

    public void OnBitmapFailed(Drawable drawable)
    {
      //TODO: Add logger message here
    }

    public void OnBitmapLoaded(Bitmap bitmap, Picasso.LoadedFrom from)
    {
      var marker = this.clusterItem != null ? this.clusterRenderer.GetMarker(this.clusterItem) : this.clusterRenderer.GetMarker(this.cluster);

      if (marker == null)
      {
        return;
      }

      marker.SetIcon(BitmapDescriptorFactory.FromBitmap(this.bitmapConverter.Invoke(bitmap)));
    }

    public void OnPrepareLoad(Drawable drawable)
    {
    }
  }
}