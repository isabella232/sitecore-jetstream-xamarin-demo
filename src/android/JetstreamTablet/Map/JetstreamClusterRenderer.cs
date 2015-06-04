namespace Jetstream.Map
{
  using System.Linq;
  using Android.Content;
  using Android.Gms.Maps;
  using Android.Gms.Maps.Model;
  using Android.Gms.Maps.Utils.Clustering;
  using Android.Gms.Maps.Utils.Clustering.View;
  using Android.Graphics;
  using Java.Lang;
  using Jetstream.Bitmap;
  using Squareup.Picasso;

  public class JetstreamClusterRenderer : DefaultClusterRenderer
  {
    private const int MinimalClusterSize = 1;

    private readonly Context context;

    private readonly int cityIconSize;
    private readonly int clusterIconSize;

    public JetstreamClusterRenderer(Context context, GoogleMap map, ClusterManager clusterManager)
      : base(context, map, clusterManager)
    {
      this.context = context;
      this.cityIconSize = (int) context.Resources.GetDimension(Resource.Dimension.city_icon_size);
      this.clusterIconSize = (int) context.Resources.GetDimension(Resource.Dimension.cluster_icon_size);
    }

    protected override void OnBeforeClusterItemRendered(Object item, MarkerOptions markerOptions)
    {
      var color = this.context.Resources.GetColor(Resource.Color.colorAccent);
      markerOptions.InvokeIcon(BitmapDescriptorFactory.DefaultMarker(color.GetHue()));
    }

    protected override void OnClusterItemRendered(Object cluster, Marker marker)
    {
      var clusterItem = cluster as ClusterItem;

      if (clusterItem != null)
      {
        var target = new MarkerTarget(this, clusterItem, bitmap => BitmapUtils.GetCircledBitmapWithBorder(bitmap, Color.Black, 3));
        Picasso.With(this.context).Load(clusterItem.ImageUrl).Resize(this.cityIconSize, this.cityIconSize).Into(target);
      }
    }

    protected override void OnBeforeClusterRendered(ICluster cluster, MarkerOptions markerOptions)
    {
      var color = this.context.Resources.GetColor(Resource.Color.colorAccent);
      markerOptions.InvokeIcon(BitmapDescriptorFactory.DefaultMarker(color.GetHue()));
    }

    protected override void OnClusterRendered(ICluster cluster, Marker marker)
    {
      var item = cluster.Items.OfType<ClusterItem>().ElementAt(0);

      var target = new MarkerTarget(this, cluster, bitmap => BitmapUtils.GetCircledBitmapWithTextIcon(bitmap, Color.White, 4, cluster.Size.ToString(), Color.White, Color.Red));
      Picasso.With(this.context).Load(item.ImageUrl).Resize(this.clusterIconSize, this.clusterIconSize).Into(target);
    }

    protected override bool ShouldRenderAsCluster(ICluster cluster)
    {
      return cluster.Size > MinimalClusterSize;
    }
  }
}