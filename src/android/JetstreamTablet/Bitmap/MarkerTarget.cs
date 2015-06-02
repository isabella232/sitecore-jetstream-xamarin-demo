namespace Jetstream.Bitmap
{
  using System;
  using Android.Gms.Maps.Model;
  using Android.Graphics;
  using Android.Graphics.Drawables;
  using Squareup.Picasso;

  public class MarkerTarget : Java.Lang.Object, ITarget
  {
    private readonly Marker targetMarker;
    private readonly Func<Bitmap, Bitmap> func; 

    public MarkerTarget(Marker targetMarker, Func<Bitmap, Bitmap> func)
    {
      this.targetMarker = targetMarker;
      this.func = func;
    }

    public void OnBitmapFailed(Drawable drawable)
    {
      //TODO: Add logger message here
    }

    public void OnBitmapLoaded(Bitmap bitmap, Picasso.LoadedFrom from)
    {
      this.targetMarker.SetIcon(BitmapDescriptorFactory.FromBitmap(this.func.Invoke(bitmap)));
    }

    public void OnPrepareLoad(Drawable drawable)
    {
    }
  }
}