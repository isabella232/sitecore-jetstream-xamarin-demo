using System;
using MapKit;
using UIKit;
using CoreAnimation;
using CoreGraphics;

namespace JetStreamIOSFull
{
  public class AnnotationViewWithRoundedImage : MKAnnotationView
  {
    private UIImageView imageView;
    private UILabel label;
    public AnnotationViewWithRoundedImage(DestinationAnnotation annotation, string reuseIdentifier)
      : base(annotation, reuseIdentifier)
    {
      this.imageView = new UIImageView (this.Bounds);
      this.AddSubview(this.imageView);

      annotation.onHiddenCount += this.HiddenCountChanged;
    }

    private void HiddenCountChanged(int count)
    {
      if (this.label != null)
      {
        this.label.RemoveFromSuperview();
      }

      if (count > 0)
      {
        this.label = new UILabel ();

        CALayer layer = this.label.Layer;
        layer.CornerRadius = 15;
        layer.MasksToBounds = true;

        label.BackgroundColor = UIColor.Red;
        label.TextColor = UIColor.White;
        label.Text = count.ToString();
        label.Font = UIFont.SystemFontOfSize(20);
        label.TextAlignment = UITextAlignment.Center;
        CGRect frame = new CGRect(0, 0, 30, 30);
        label.Frame = frame;

        this.AddSubview(this.label);
      }
    }

    public override UIImage Image
    {
      get {
        return  this.imageView.Image;
      }
      set {
        nfloat height = value.Size.Height;
        nfloat width = value.Size.Width;
        CGRect frame = new CGRect (this.Frame.Left, this.Frame.Right, width, height);

        this.Frame = frame;
        this.imageView.Frame = this.Bounds;

        CALayer imageLayer = this.imageView.Layer;
        imageLayer.CornerRadius = value.Size.Height/2;
        imageLayer.BorderWidth = 1;
        imageLayer.MasksToBounds = true;
        this.imageView.Image = value;
      }
    }
  }
}

