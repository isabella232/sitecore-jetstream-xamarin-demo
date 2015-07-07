namespace Jetstream.Bitmap
{
  using Android.Graphics;

  public static class BitmapUtils
  {
    private const int OuterCircleCenterShift = 4;
    private const int OutBitmapSize = 20;
    private const int TextCircleShift = 5;
    private const float TextSize = 24;

    public static Bitmap GetCircledBitmapWithTextIcon(Bitmap source, Color borderColor, float strokeWidth, string text, Color textColor, Color textCircleFillColor)
    {
      Bitmap output = CreateCircledBitmap(source, borderColor, strokeWidth);

      int w = source.Width;
      int h = source.Height;
      
      var canvas = new Canvas(output);

      var textPaint = new Paint();
      var circlePaint = new Paint();

      textPaint.Color = textColor;
      textPaint.TextSize = TextSize;
      textPaint.AntiAlias = true;
      textPaint.TextAlign = Paint.Align.Center;

      Rect bounds = new Rect();
      textPaint.GetTextBounds(text, 0, text.Length, bounds);

      circlePaint.Color = textCircleFillColor;
      circlePaint.AntiAlias = true;

      canvas.DrawCircle(w - TextCircleShift, h - TextCircleShift - (bounds.Height() / 2), bounds.Width() + 10, circlePaint);

      canvas.DrawText(text, w - TextCircleShift, h - TextCircleShift, textPaint);

      return output;
    }

    private static Bitmap CreateCircledBitmap(Bitmap bitmap, Color borderColor, float strokeWidth)
    {
      int w = bitmap.Width;
      int h = bitmap.Height;

      int radius = System.Math.Min(h / 2, w / 2);
      Bitmap output = Bitmap.CreateBitmap(w + OutBitmapSize, h + OutBitmapSize, Bitmap.Config.Argb8888);

      Paint p = new Paint
      {
        AntiAlias = true
      };

      Canvas c = new Canvas(output);
      c.DrawARGB(0, 0, 0, 0);
      p.SetStyle(Paint.Style.Fill);

      c.DrawCircle((w / 2) + OuterCircleCenterShift, (h / 2) + OuterCircleCenterShift, radius, p);

      p.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.SrcIn));

      c.DrawBitmap(bitmap, OuterCircleCenterShift, OuterCircleCenterShift, p);
      p.SetXfermode(null);

      p.SetStyle(Paint.Style.Stroke);
      p.Color = borderColor;
      p.StrokeWidth = strokeWidth;
      c.DrawCircle((w / 2) + OuterCircleCenterShift, (h / 2) + OuterCircleCenterShift, radius, p);

      return output;   
    }

    public static Bitmap GetCircledBitmapWithBorder(Bitmap source, Color borderColor, float strokeWidth)
    {
      int w = source.Width;
      int h = source.Height;

      int radius = System.Math.Min(h / 2, w / 2);
      Bitmap output = Bitmap.CreateBitmap(w + OutBitmapSize, h + OutBitmapSize, Bitmap.Config.Argb8888);

      Paint p = new Paint
      {
        AntiAlias = true
      };

      Canvas c = new Canvas(output);
      c.DrawARGB(0, 0, 0, 0);
      p.SetStyle(Paint.Style.Fill);

      c.DrawCircle((w / 2) + OuterCircleCenterShift, (h / 2) + OuterCircleCenterShift, radius, p);

      p.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.SrcIn));

      c.DrawBitmap(source, OuterCircleCenterShift, OuterCircleCenterShift, p);
      p.SetXfermode(null);

      p.SetStyle(Paint.Style.Stroke);
      p.Color = borderColor;
      p.StrokeWidth = strokeWidth;
      c.DrawCircle((w / 2) + OuterCircleCenterShift, (h / 2) + OuterCircleCenterShift, radius, p);
    
      return output;
    }
  }
}