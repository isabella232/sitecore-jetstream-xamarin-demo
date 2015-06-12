// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using JetStreamIOSFull.MapUI;
using SDWebImage;
using JetStreamIOSFull.Helpers;

namespace JetStreamIOSFull
{
	public partial class DestinationCarouselCell : UICollectionViewCell
	{
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

    public void FillWithDestination(DestinationAnnotation destination)
    {
      this.ContainerView.Layer.CornerRadius = 15;

      this.ContainerView.Layer.BorderColor = UIColor.Orange.CGColor;
      this.ContainerView.Layer.BorderWidth = 0;

      this.ContainerView.Layer.MasksToBounds = true;

      this.TitleLabel.Text = destination.Title;

      NSUrl imageUrl = new NSUrl(destination.ImageUrl);

      this.ImageView.Image = null;

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
          InvokeOnMainThread(() =>
          {
            if (image != null)
            {
              this.ImageView.Image = image;
            }
          });
        }
      }
      );
    }
	}
}
