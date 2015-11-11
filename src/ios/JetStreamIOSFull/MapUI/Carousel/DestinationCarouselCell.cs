using System;

using Foundation;
using UIKit;
using JetStreamIOSFull.MapUI;
using SDWebImage;
using JetStreamIOSFull.Helpers;
using CoreGraphics;

namespace JetStreamIOSFull
{
	public partial class DestinationCarouselCell : UICollectionViewCell
	{
    private bool UIIsNeedToBeSet = true;

    private float animationDuration = 0.3f;

    private nfloat cornerRadius = 3.0f;
    private nfloat shadowRadius = 5.0f;

		public DestinationCarouselCell (IntPtr handle) : base (handle)
		{

    }

    public void Highlight(bool highlight)
    {
      if (highlight)
      {
        this.ContainerView.Layer.BorderWidth = 2;
      }
      else
      {
        this.ContainerView.Layer.BorderWidth = 0;
      }
    }

    private void ApplyUISettings()
    {
      if (this.UIIsNeedToBeSet)
      {
        this.UIIsNeedToBeSet = false;
        this.ContainerView.Layer.CornerRadius = cornerRadius;
        this.ContainerView.Layer.ShadowColor = UIColor.Black.CGColor;
        this.ContainerView.Layer.ShadowOffset = new CGSize (0, cornerRadius);
        this.ContainerView.Layer.ShadowRadius = shadowRadius;
        this.ContainerView.Layer.ShadowOpacity = 0.8f;
        this.ContainerView.Layer.BorderColor = UIColor.Orange.CGColor;
        this.ContainerView.Layer.BorderWidth = 0;
        this.ContainerView.Layer.MasksToBounds = false;

        this.ImageView.Layer.CornerRadius = cornerRadius;
        this.ImageView.Layer.MasksToBounds = true;
      }
    }

    public void FillWithDestination(DestinationAnnotation destination)
    {
      this.ApplyUISettings();
      this.TitleLabel.Text = destination.Title;

      this.ShowEmptyCellWithAnimation();

      this.ImageView.SetImage (
        url: new NSUrl (destination.ImageUrl),
        placeholder: null,
        options: SDWebImageOptions.RetryFailed | SDWebImageOptions.LowPriority,
        completedBlock: (image, error, cacheType, imageUrl) =>{

        Console.WriteLine("image downloaded");
      } );
    }

    private void ShowEmptyCellWithAnimation()
    {
      this.ImageView.Image = null;

      this.ContainerView.Alpha = 0.0f;

      System.Threading.ThreadPool.QueueUserWorkItem(state =>
      {
        InvokeOnMainThread(() =>
        {
          UIView.AnimateNotify(animationDuration, () =>
          {
            this.ContainerView.Alpha = 1.0f;
          }, 
            new UICompletionHandler((bool fn) =>{})
          );
        });
      });
    }
	}
}
