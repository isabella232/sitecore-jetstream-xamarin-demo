namespace Jetstream.Bitmap
{
  using Android.Graphics;

  public static class BitmapUtils
  {
    public static Bitmap GetCircledBitmapWithBorder(Bitmap source, Color borderColor, float strokeWidth)
    {
      return CreateCircledBitmap(source, borderColor, strokeWidth);
    }

    public static Bitmap GetCircledBitmapWithTextIcon(Bitmap source, Color borderColor, float strokeWidth, string text, Color textColor, Color textCircleFillColor)
    {
      Bitmap output = CreateCircledBitmap(source, borderColor, strokeWidth);

      int w = source.Width;
      int h = source.Height;
      
      var canvas = new Canvas(output);

      var textPaint = new Paint();
      var circlePaint = new Paint();

      textPaint.Color = textColor;
      textPaint.TextSize = 24f;
      textPaint.AntiAlias = true;
      textPaint.TextAlign = Paint.Align.Center;

      Rect bounds = new Rect();
      textPaint.GetTextBounds(text, 0, text.Length, bounds);

      circlePaint.Color = textCircleFillColor;
      circlePaint.AntiAlias = true;

      canvas.DrawCircle(w - 5, h - 5 - (bounds.Height() / 2), bounds.Width() + 10, circlePaint);

      canvas.DrawText(text, w - 5, h - 5, textPaint);

      return output;
    }

    private static Bitmap CreateCircledBitmap(Bitmap bitmap, Color borderColor, float strokeWidth)
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

    public static Bitmap GetCirdcledBitmapWithBorder(Bitmap source, Color borderColor, float strokeWidth)
    {
      int w = source.Width;
      int h = source.Height;

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

      c.DrawBitmap(source, 4, 4, p);
      p.SetXfermode(null);

      p.SetStyle(Paint.Style.Stroke);
      p.Color = borderColor;
      p.StrokeWidth = strokeWidth;
      c.DrawCircle((w / 2) + 4, (h / 2) + 4, radius, p);

    
      return output;
    }
  }
}