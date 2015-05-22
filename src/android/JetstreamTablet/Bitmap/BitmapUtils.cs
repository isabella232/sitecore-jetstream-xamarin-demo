namespace Jetstream.Bitmap
{
  using Android.Graphics;

  public static class BitmapUtils
  {
    public static Bitmap GetCircledBitmapWithBorder(Bitmap bitmap, Color borderColor, float strokeWidth)
    {
      int w = bitmap.Width;
      int h = bitmap.Height;

      int radius = System.Math.Min(h / 2, w / 2);
      Bitmap output = Bitmap.CreateBitmap(w + 20, h + 20, Bitmap.Config.Argb8888);

      Paint p = new Paint
      {
        AntiAlias = true
      };

      Canvas c = new Canvas(output);
      c.DrawARGB(0, 0, 0, 0);
      p.SetStyle(Paint.Style.Fill);

      c.DrawCircle((w / 2) + 4, (h / 2) + 4, radius, p);

      p.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.SrcIn));

      c.DrawBitmap(bitmap, 4, 4, p);
      p.SetXfermode(null);

      p.SetStyle(Paint.Style.Stroke);
      p.Color = borderColor;
      p.StrokeWidth = strokeWidth;
      c.DrawCircle((w / 2) + 4, (h / 2) + 4, radius, p);

      return output;   
    }
  }
}