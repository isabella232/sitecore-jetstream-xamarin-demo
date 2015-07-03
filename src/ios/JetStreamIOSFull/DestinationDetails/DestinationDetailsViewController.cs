using System;

using Foundation;
using UIKit;
using JetStreamCommons.Destinations;
using SDWebImage;
using CoreGraphics;
using JetStreamCommons;
using System.Collections;
using System.Collections.Generic;
using JetStreamIOSFull.Helpers;
using JetStreamIOSFull.BaseVC;

namespace JetStreamIOSFull.DestinationDetails
{
  public partial class DestinationDetailsViewController : BaseViewController
	{
    private IDestination destination;
    
		public DestinationDetailsViewController (IntPtr handle) : base (handle)
		{
      
		}

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();

      this.TopBarImage.Image = this.Appearance.Common.NavigationBackgroundImage;

      UIImage image = UIImage.FromBundle("Images.xcassets/LeftArrow.png");
      this.BackButton.SetImage(image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate), UIControlState.Normal);
   }

    private async void DownloadAttractions()
    {
      if (this.destination != null)
      {
        using (var session = this.Endpoint.GetSession())
        {
          using (var loader = new DestinationsLoader (session))
          {
            List<IAttraction> attractions;
            try
            {
              attractions = await loader.LoadAttractions(this.destination);
            }
            catch
            {
              AlertHelper.ShowLocalizedAlertWithOkOption("NETWORK_ERROR_TITLE", "CANNOT_DOWNLOAD_ATTRACTIONS_ERROR");
              return;
            }
              
            this.ImagesTableView.Source = new DestinationImagesSource (attractions, this.destination, this.Endpoint);
            this.ImagesTableView.ReloadData();
          }
        }
      }
    }

    partial void BackgroundViewTouched (Foundation.NSObject sender)
    {
      this.DismissModalViewController(true);
    }

    public void ShowDestinationDetails(IDestination destination)
    {
      if (destination == null)
      {
        throw new NullReferenceException("Destination must not be null");
      }
      this.destination = destination;
      this.DownloadAttractions();
    }

    public override void ViewWillAppear(bool animated)
    {
      base.ViewWillAppear(animated);

      this.FillScreesnWithDestinationInfo(this.destination);
    }

    private void FillScreesnWithDestinationInfo(IDestination destination)
    {
      this.TitleLabel.Text = destination.DisplayName;

      this.WebDestinationDescroption.LoadHtmlString(destination.Overview, null);
    }
	}
}
