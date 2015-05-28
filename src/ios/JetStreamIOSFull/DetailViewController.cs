using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UIKit;
using MapKit;
using Foundation;
using CoreLocation;

using SDWebImage;

using JetStreamCommons;
using JetStreamIOSFull.MapUI;
using JetStreamCommons.Destinations;
using JetStreamIOSFull.Helpers;


namespace JetStreamIOSFull
{
  public partial class DetailViewController : BaseViewController, IMKMapViewDelegate
  {
    private IEnumerable destinations;

    private MapManager mapManager;

    public DetailViewController(IntPtr handle) : base(handle)
    {
      
    }

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();

      this.mapManager = new MapManager(this.Appearance);
      this.map.Delegate = mapManager;

    }

    public override void ViewDidAppear(bool animated)
    {
      base.ViewDidAppear(animated);

      this.RefreshMap();
    }

    partial void RefreshButtonTouched(Foundation.NSObject sender)
    {
      this.RefreshMap();
    }

    private async void RefreshMap()
    {
      this.destinations = await this.DownloadAllDestinations();

      this.mapManager.ResetMapState(this.map);

      foreach(IDestination elem in this.destinations)
      {
        bool coordinatesAvailable = (elem.Latitude != 0.0) && (elem.Longitude != 0.0); 
        if (coordinatesAvailable)
        {
          CLLocationCoordinate2D coordinates = new CLLocationCoordinate2D(elem.Latitude, elem.Longitude);

          string instanceUrl = this.Endpoint.InstanceUrl;
          string imagePath = elem.ImagePath;
            
          NSUrl imageUrl = new NSUrl(String.Concat(instanceUrl, imagePath));

          SDWebImageDownloader.SharedDownloader.DownloadImage(
            url: imageUrl,
            options: SDWebImageDownloaderOptions.HighPriority,
            progressHandler: (receivedSize, expectedSize) =>
          {
            // Track progress...
          },
            completedHandler: (image, data, error, finished) =>
          {
            if (image != null)
            {
              DestinationAnnotation annotation = new DestinationAnnotation(elem.DisplayName, image, coordinates, this.Appearance);
              this.mapManager.AddAnnotationForMap(this.map, annotation);
            }
          }
          );

        }
      }
    }

    private async Task<IEnumerable> DownloadAllDestinations()
    {
      try
      {
        //FIXME: error here, some objects must be disposed!!!

        var session = this.Endpoint.GetSession();
        using (var loader = new DestinationsLoader(session))
        {
          return await loader.LoadOnlyDestinations();
        }
      }
      catch
      {
        AlertHelper.ShowLocalizedAlertWithOkOption("NETWORK_ERROR_TITLE", "CANNOT_DOWNLOAD_DESTINATIONS_ERROR");
        return null;
      }

    }

    public override void DidReceiveMemoryWarning()
    {
      base.DidReceiveMemoryWarning();
      // Release any cached data, images, etc that aren't in use.
    }
  }
}


