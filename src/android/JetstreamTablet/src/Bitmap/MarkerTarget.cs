namespace Jetstream.Bitmap
{
  using System;
  using Android.Gms.Maps.Model;
  using Android.Graphics;
  using Android.Graphics.Drawables;
  using Square.Picasso;

  public class MarkerTarget : Java.Lang.Object, ITarget
  {
    private readonly Marker marker;
    private readonly Func<Bitmap, Bitmap> bitmapConverter;
    private readonly Func<Bitmap> failedHandler;

    public MarkerTarget(Marker marker, Func<Bitmap, Bitmap> bitmapConverter, Func<Bitmap> failedHandler)
    {
      this.marker = marker;
      this.bitmapConverter = bitmapConverter;
      this.failedHandler = failedHandler;
    }

    public void OnBitmapFailed(Drawable drawable)
    {
      if (this.failedHandler != null)
      {
        this.marker.SetIcon(BitmapDescriptorFactory.FromBitmap(this.failedHandler()));
      }
    }

    public void OnBitmapLoaded(Bitmap bitmap, Picasso.LoadedFrom from)
    {
      try
      {
        this.marker.SetIcon(BitmapDescriptorFactory.FromBitmap(this.bitmapConverter(bitmap)));  
      }
      catch (Java.Lang.RuntimeException exception)
      {
        //Original cause is that we are trying to update marker's icon after that marker was removed from map.
        //There is no clear way to detect whether marker still exists on map.
      }
    }

    public void OnPrepareLoad(Drawable drawable)
    {
    }
  }
}