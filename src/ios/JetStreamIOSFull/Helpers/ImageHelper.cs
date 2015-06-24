using System;
using UIKit;
using System.Drawing;
using CoreGraphics;

namespace JetStreamIOSFull.Helpers
{
  public static class ImageHelper
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
      if (sourceImage == null)
      {
        return null;
      }

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

    public static UIImage ImageFromColor(UIColor color, SizeF imageSize)
    {
      var imageSizeRectF = new RectangleF(0, 0, 30, 30);
      UIGraphics.BeginImageContextWithOptions(imageSize, false, 0);
      var context = UIGraphics.GetCurrentContext();
      context.SetFillColor(color.CGColor);
      context.FillRect(imageSizeRectF);
      var image = UIGraphics.GetImageFromCurrentImageContext();
      UIGraphics.EndImageContext();
      return image;
    }
  }
}

