using System;
using MapKit;
using UIKit;
using CoreAnimation;
using CoreGraphics;

namespace JetStreamIOSFull.MapUI
{
  public class AnnotationViewWithRoundedImage : MKAnnotationView
  {
    private UIImageView imageView;
    private UILabel label;

    private nfloat HiddenLabelSize = 30;
    private nfloat HiddenLabelFontSize = 20;

    public AnnotationViewWithRoundedImage(DestinationAnnotation annotation, string reuseIdentifier)
      : base(annotation, reuseIdentifier)
    {
      this.InitUI();

      annotation.onHiddenCount += this.HiddenCountChanged;
      this.HiddenCountChanged(annotation.HiddenCount);
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
      layer.CornerRadius = HiddenLabelSize/2;
      layer.MasksToBounds = true;

      this.label.BackgroundColor = UIColor.Red;
      this.label.TextColor = UIColor.White;
      this.label.Font = UIFont.SystemFontOfSize(HiddenLabelFontSize);
      this.label.TextAlignment = UITextAlignment.Center;
      CGRect frame = new CGRect (0, 0, HiddenLabelSize, HiddenLabelSize);
      this.label.Frame = frame;
      this.AddSubview(this.label);
    }

    private void InitRoundedImage()
    {
      this.imageView = new UIImageView (this.Bounds);
      this.AddSubview(this.imageView);
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

    public override UIImage Image
    {
      get {
        return  this.imageView.Image;
      }
      set {
        InvokeOnMainThread(() =>
        {
          nfloat height = value.Size.Height;
          nfloat width = value.Size.Width;
          CGRect frame = new CGRect (this.Frame.Left, this.Frame.Right, width, height);

          this.Frame = frame;
          this.imageView.Frame = this.Bounds;

          CALayer imageLayer = this.imageView.Layer;
          imageLayer.CornerRadius = value.Size.Height / 2;
          imageLayer.BorderWidth = 1;
          imageLayer.MasksToBounds = true;
          this.imageView.Image = value;
        });
      }
    }
  }
}

