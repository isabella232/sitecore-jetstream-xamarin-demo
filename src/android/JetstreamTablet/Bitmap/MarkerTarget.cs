namespace Jetstream.Bitmap
{
  using System;
  using Android.Gms.Maps.Model;
  using Android.Graphics;
  using Android.Graphics.Drawables;
  using Squareup.Picasso;

  public class MarkerTarget : Java.Lang.Object, ITarget
  {
    private readonly Marker marker;
    private readonly Func<Bitmap, Bitmap> bitmapConverter;

    public MarkerTarget(Marker marker, Func<Bitmap, Bitmap> bitmapConverter)
    {
      this.marker = marker;
      this.bitmapConverter = bitmapConverter;
    }

    public void OnBitmapFailed(Drawable drawable)
    {
      //TODO: Add logger message here
    }

    public void OnBitmapLoaded(Bitmap bitmap, Picasso.LoadedFrom from)
    {
      try
      {
        this.marker.SetIcon(BitmapDescriptorFactory.FromBitmap(this.bitmapConverter(bitmap)));  
      }
      catch (Java.Lang.RuntimeException exception)
      {
        //TODO: Original cause is that we are trying to update marker's icon after that marker was removed from move.
        // There is no clear way to detect whether marker still exists on map.
      }
    }

    public void OnPrepareLoad(Drawable drawable)
    {
    }
  }
}