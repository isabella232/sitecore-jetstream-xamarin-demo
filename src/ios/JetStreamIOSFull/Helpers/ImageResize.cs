using System;
using UIKit;
using System.Drawing;

namespace JetStreamIOSFull
{
  public static class ImageResize
  {
    public static UIImage MaxResizeImage(UIImage sourceImage, float maxWidth, float maxHeight)
    {
      var sourceSize = sourceImage.Size;
      var maxResizeFactor = Math.Max(maxWidth / sourceSize.Width, maxHeight / sourceSize.Height);
      if (maxResizeFactor > 1) return sourceImage;
      float width = (float)(maxResizeFactor * sourceSize.Width);
      float height = (float)(maxResizeFactor * sourceSize.Height);
      UIGraphics.BeginImageContext(new SizeF(width, height));
      sourceImage.Draw(new RectangleF(0, 0, width, height));
      var resultImage = UIGraphics.GetImageFromCurrentImageContext();
      UIGraphics.EndImageContext();
      return resultImage;
    }

    public static UIImage ResizeImage(UIImage sourceImage, float width, float height)
    {
      UIGraphics.BeginImageContext(new SizeF(width, height));
      sourceImage.Draw(new RectangleF(0, 0, width, height));
      var resultImage = UIGraphics.GetImageFromCurrentImageContext();
      UIGraphics.EndImageContext();
      return resultImage;
    }

    private static UIImage CropImage(UIImage sourceImage, int crop_x, int crop_y, int width, int height)
    {
      var imgSize = sourceImage.Size;
      UIGraphics.BeginImageContext(new SizeF(width, height));
      var context = UIGraphics.GetCurrentContext();
      var clippedRect = new RectangleF(0, 0, width, height);
      context.ClipToRect(clippedRect);
      float fwidth = (float)imgSize.Width;
      float fheight = (float)imgSize.Height;
      var drawRect = new RectangleF(-crop_x, -crop_y, fwidth, fheight);
      sourceImage.Draw(drawRect);
      var modifiedImage = UIGraphics.GetImageFromCurrentImageContext();
      UIGraphics.EndImageContext();
      return modifiedImage;
    }
  }
}

