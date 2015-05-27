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


namespace JetStreamIOSFull
{
  public partial class DetailViewController : UIViewController, IMKMapViewDelegate
  {
    private IEnumerable destinations;
    private InstanceSettings.InstanceSettings endpoint;
    private MapDelegate mapDelegate;

    public DetailViewController(IntPtr handle) : base(handle)
    {
    }

    public void SetDetailItem(object newDetailItem)
    {
    }

    public async override void ViewDidLoad()
    {
      base.ViewDidLoad();

      this.destinations = await this.DownloadAllDestinations();

      this.mapDelegate = new MapDelegate();
      this.map.Delegate = mapDelegate;

      this.RefreshMap();
    }

    private void RefreshMap()
    {
      this.ClearMap();

      List<IMKAnnotation> annotations = new List<IMKAnnotation>();

      foreach(IDestination elem in this.destinations)
      {
        bool coordinatesAvailable = (elem.Latitude != 0.0) && (elem.Longitude != 0.0); 
        if (coordinatesAvailable)
        {
          CLLocationCoordinate2D coordinates = new CLLocationCoordinate2D(elem.Latitude, elem.Longitude);

          string instanceUrl = this.endpoint.InstanceUrl;
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
              DestinationAnnotation annotation = new DestinationAnnotation(elem.DisplayName, image, coordinates);
              this.mapDelegate.AddAnnotationForMap(this.map, annotation);
            }
          }
          );

        }
      }
    }

    private void ClearMap()
    {
      map.RemoveAnnotations(map.Annotations);
    }

    private async Task<IEnumerable> DownloadAllDestinations()
    {
      try
      {
        //FIXME: error here, some objects must be disposed!!!
        this.endpoint = new InstanceSettings.InstanceSettings();

        var session = this.endpoint.GetSession();
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


