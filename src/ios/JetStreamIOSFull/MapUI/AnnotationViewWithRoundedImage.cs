using System;
using MapKit;
using UIKit;
using CoreAnimation;
using CoreGraphics;
using JetStreamIOSFull.Helpers;
using Foundation;
using SDWebImage;

namespace JetStreamIOSFull.MapUI
{
  public class AnnotationViewWithRoundedImage : MKAnnotationView
  {
    private UIImageView imageView;
    private UILabel label;
    private IAppearanceHelper appearanceHelper;

    public AnnotationViewWithRoundedImage(DestinationAnnotation annotation, string reuseIdentifier, IAppearanceHelper appearance)
      : base(annotation, reuseIdentifier)
    {
      this.appearanceHelper = appearance;

      this.InitUI();

      annotation.onHiddenCount += this.HiddenCountChanged;
      this.HiddenCountChanged(annotation.HiddenCount);

      this.DownloadImage(annotation.ImageUrl);
    }

    private void InitUI()
    {
      //IGK, order matters!
      this.InitRoundedImage();
      this.InitHiddenLabel();
    }

    private void InitHiddenLabel()
    {
      this.label = new UILabel ();
      CALayer layer = this.label.Layer;
      layer.CornerRadius = appearanceHelper.HiddenLabelSize/2;
      layer.MasksToBounds = true;

      this.label.BackgroundColor = appearanceHelper.SelectionColor;
      this.label.TextColor = appearanceHelper.MenuTextColor;
      this.label.Font = UIFont.SystemFontOfSize(appearanceHelper.HiddenLabelFontSize);
      this.label.TextAlignment = UITextAlignment.Center;
      CGRect frame = new CGRect (0, 0, appearanceHelper.HiddenLabelSize, appearanceHelper.HiddenLabelSize);
      this.label.Frame = frame;
      this.AddSubview(this.label);
    }

    private void DownloadImage(string imagePath)
    {
      if (this.appearanceHelper != null)
      {
        this.Image = this.appearanceHelper.DestinationPlaceholder;
      }

      NSUrl imageUrl = new NSUrl(imagePath);

      SDWebImageDownloader.SharedDownloader.DownloadImage(
        url: imageUrl,
        options: SDWebImageDownloaderOptions.LowPriority,
        progressHandler: (receivedSize, expectedSize) =>
      {
        // Track progress...
      },
        completedHandler: (image, data, error, finished) =>
      {
        if (image != null)
        {
          UIImage resizedImage = ImageResize.ResizeImage(image, appearanceHelper.DestinationIconSize, appearanceHelper.DestinationIconSize);  
          this.Image = resizedImage;
        }
      }
      );
    }

    private void InitRoundedImage()
    {
      this.imageView = new UIImageView (this.Bounds);
      this.AddSubview(this.imageView);

      CALayer imageLayer = this.imageView.Layer;
      imageLayer.BorderWidth = appearanceHelper.DestinationIconBorderSize;
      imageLayer.MasksToBounds = true;
    }

    public void HiddenCountChanged(int count)
    {
      InvokeOnMainThread(() =>
      {
        if (count > 1)
        {
          this.label.Text = count.ToString();
          this.label.Hidden = false;
        }
        else
        {
          this.label.Hidden = true;
        }
      });
    }

    public override IMKAnnotation Annotation
    {
      set
      {
        if (value != null)
        {
          DestinationAnnotation annotation = value as DestinationAnnotation;
          base.Annotation = annotation;
          this.DownloadImage(annotation.ImageUrl);
        }
      }
    }

    public override void SetSelected(bool selected, bool animated)
    {
      CALayer imageLayer = this.imageView.Layer;

      if (selected)
      {
        imageLayer.BorderColor = appearanceHelper.SelectionColor.CGColor;
      }
      else
      {
        imageLayer.BorderColor = appearanceHelper.MenuBackgroundColor.CGColor;
      }
    }

    public override UIImage Image
    {
      get 
      {
        return  this.imageView.Image;
      }
      set
      {
        InvokeOnMainThread(() =>
        {
          nfloat height = value.Size.Height;
          nfloat width = value.Size.Width;
          CGRect frame = new CGRect (this.Frame.X, this.Frame.Y, width, height);

          this.Frame = frame;
          this.imageView.Frame = this.Bounds;

          CALayer imageLayer = this.imageView.Layer;
          imageLayer.CornerRadius = value.Size.Height / 2;

          this.imageView.Image = value;
        });
      }
    }
  }
}

