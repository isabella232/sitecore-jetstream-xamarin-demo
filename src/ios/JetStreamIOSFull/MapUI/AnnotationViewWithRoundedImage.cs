using System;
using MapKit;
using UIKit;
using CoreAnimation;
using CoreGraphics;
using JetStreamIOSFull.Helpers;
using Foundation;
using SDWebImage;
using JetStreamCommons.Destinations;

namespace JetStreamIOSFull.MapUI
{
  public class AnnotationViewWithRoundedImage : MKAnnotationView
  {
    private UIImageView imageView;
    private UILabel label;
    private AppearanceHelper appearanceHelper;
    private IDestination destination;
    private bool isGroup = true;

    public AnnotationViewWithRoundedImage(DestinationAnnotation annotation, string reuseIdentifier, AppearanceHelper appearance)
      : base(annotation, reuseIdentifier)
    {
      this.appearanceHelper = appearance;

      this.InitUI();

      this.InitWithAnnotation(annotation);
    }

    private void InitWithAnnotation(DestinationAnnotation annotation)
    {
      this.destination = annotation.Destination;
      base.Annotation = annotation;
      annotation.onHiddenCount += this.HiddenCountChanged;
      this.HiddenCountChanged(annotation.HiddenCount);
      this.DownloadImage(annotation.ImageUrl);
    }

    public IDestination Destination
    {
      get
      { 
        return this.destination;
      }
    }

    private void InitUI()
    {
      //IGK, order matters!
      this.InitRoundedImage();
      this.InitHiddenLabel();
    }

    private void InitHiddenLabel()
    {
      nfloat size = appearanceHelper.Map.GroupLabelSize;

      this.label = new UILabel ();
      CALayer layer = this.label.Layer;
      layer.CornerRadius = size/2;
      layer.MasksToBounds = true;

      this.label.BackgroundColor = appearanceHelper.Map.GroupLabelColor;
      this.label.TextColor = appearanceHelper.Map.GroupTextColor;
      this.label.Font = UIFont.SystemFontOfSize(appearanceHelper.Map.GroupLabelFontSize);
      this.label.TextAlignment = UITextAlignment.Center;

      CGRect frame = new CGRect (0, 0, size, size);
      this.label.Frame = frame;
      this.AddSubview(this.label);
    }

    private void DownloadImage(string imagePath)
    {
      if (this.imageView != null)
      {
        this.Image = this.appearanceHelper.Map.DestinationPlaceholder;

        NSUrl imageUrl = new NSUrl (imagePath);

        SDWebImageManager manager = SDWebImageManager.SharedManager;
        manager.Download(
          url: imageUrl,
          options: SDWebImageOptions.RetryFailed | SDWebImageOptions.LowPriority,
          progressHandler: (receivedSize, expectedSize) =>
          {
          },
          completedHandler: (image, error, cacheType, finished, url) =>
          {
            float size = appearanceHelper.Map.DestinationIconSize;
            UIImage resizedImage = ImageHelper.ResizeImage(image, size, size);  
            if (resizedImage != null)
            {
              this.Image = resizedImage;
            }
          }
        );
      }
    }

    private void InitRoundedImage()
    {
      this.imageView = new UIImageView (this.Bounds);
      this.AddSubview(this.imageView);

      CALayer imageLayer = this.imageView.Layer;
      imageLayer.BorderWidth = this.appearanceHelper.Map.DestinationIconBorderSize;
      imageLayer.BorderColor = this.appearanceHelper.Map.DestinationBorderColor.CGColor;
      imageLayer.MasksToBounds = true;
    }

    public bool IsGroup
    {
      get
      { 
        return this.isGroup;
      }
    }

    public void HiddenCountChanged(int count)
    {
      isGroup = false;
      InvokeOnMainThread(() =>
      {
        if (this.label != null)
        {
          if (count > 1)
          {
            isGroup = true;
            this.label.Text = count.ToString();
            this.label.Hidden = false;
          }
          else
          {
            this.label.Hidden = true;
          }
        }
      });
    }

    public override IMKAnnotation Annotation
    {
      set
      {
        DestinationAnnotation oldAnnotation = base.Annotation as DestinationAnnotation;

        if (oldAnnotation != null)
        {
          oldAnnotation.onHiddenCount -= this.HiddenCountChanged;
        }

        if (value != null)
        {
          DestinationAnnotation annotation = value as DestinationAnnotation;
          this.InitWithAnnotation(annotation);
        }
        else
        {
          this.HiddenCountChanged(1);
        }
      }
    }

    public override void SetSelected(bool selected, bool animated)
    {
      CALayer imageLayer = this.imageView.Layer;

      if (selected)
      {
        imageLayer.BorderColor = appearanceHelper.Map.DestinationSelectedBorderColor.CGColor;
      }
      else
      {
        imageLayer.BorderColor = appearanceHelper.Map.DestinationBorderColor.CGColor;
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

