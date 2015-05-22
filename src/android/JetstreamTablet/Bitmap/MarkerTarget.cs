namespace Jetstream.Bitmap
{
  using Android.Gms.Maps.Model;
  using Android.Graphics;
  using Android.Graphics.Drawables;
  using JetStreamCommons.Destinations;
  using Squareup.Picasso;

  public class MarkerTarget : Java.Lang.Object, ITarget
  {
    private Marker targetMarker;
    private IDestination destination;

    public void Dispose()
    {
      this.targetMarker = null;
      this.destination = null;
    }

    public MarkerTarget(IDestination destination, Marker targetMarker)
    {
      this.targetMarker = targetMarker;
      this.destination = destination;
    }
    
    public void OnBitmapFailed(Drawable drawable)
    {
      //TODO: Add logger message here
    }

    public void OnBitmapLoaded(Bitmap bitmap, Picasso.LoadedFrom from)
    {
      var newBitmap = BitmapUtils.GetCircledBitmapWithBorder(bitmap, Color.Black, 3);
      this.targetMarker.SetIcon(BitmapDescriptorFactory.FromBitmap(newBitmap));
    }

    public void OnPrepareLoad(Drawable drawable)
    {
    }
  }
}