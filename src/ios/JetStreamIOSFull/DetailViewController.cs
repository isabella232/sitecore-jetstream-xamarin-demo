using System;
using UIKit; 
using InstanceSettings;
using JetStreamCommons;
using System.Threading.Tasks;
using System.Collections;
using JetStreamCommons.Destinations;
using MapKit;
using CoreLocation;
using Foundation;

namespace JetStreamIOSFull
{
  public partial class DetailViewController : UIViewController, IMKMapViewDelegate
  {
    private IEnumerable destinations;
    private InstanceSettings.InstanceSettings endpoint;

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
      this.RefreshMap();

      MapDelegate mapDelegate = new MapDelegate();
      mapDelegate.endpoint = this.endpoint;
      this.map.Delegate = mapDelegate;
    }

    private void RefreshMap()
    {
      this.ClearMap();

      foreach(IDestination elem in this.destinations)
      {
        bool coordinatesAvailable = (elem.Latitude != 0.0) && (elem.Longitude != 0.0); 
        if (coordinatesAvailable)
        {
          this.map.AddAnnotation(new MKPointAnnotation () {
            Title = elem.ImagePath,
            Coordinate = new CLLocationCoordinate2D (elem.Latitude, elem.Longitude),
          });
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


