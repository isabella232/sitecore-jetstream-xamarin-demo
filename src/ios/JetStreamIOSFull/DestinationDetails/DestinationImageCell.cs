using System;

using Foundation;
using UIKit;

namespace JetStreamIOSFull.DestinationDetails
{
	public partial class DestinationImageCell : UITableViewCell
	{
		public DestinationImageCell (IntPtr handle) : base (handle)
		{
		}

    public UIImageView DestinationImageView
    {
      get
      { 
        return this.DestinationImage;
      }
    }

    public void SetImage(UIImage image)
    {
      this.DestinationImage.Image = image;
    }

    public UIActivityIndicatorView ActivityIndicator
    {
      get
      { 
        return this.LoadingIndicator;
      }
    }
	}
}
