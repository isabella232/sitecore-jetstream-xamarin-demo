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
    public object DetailItem { get; set; }
    private IEnumerable destinations;
    private InstanceSettings.InstanceSettings endpoint;

    public DetailViewController(IntPtr handle) : base(handle)
    {
    }

    public void SetDetailItem(object newDetailItem)
    {
      if (DetailItem != newDetailItem)
      {
        DetailItem = newDetailItem;
        
        // Update the view
        ConfigureView();
      }
    }

    void ConfigureView()
    {
      // Update the user interface for the detail item
      if (IsViewLoaded && DetailItem != null)
        detailDescriptionLabel.Text = DetailItem.ToString();
    }

    public async override void ViewDidLoad()
    {
      base.ViewDidLoad();
      // Perform any additional setup after loading the view, typically from a nib.
      ConfigureView();

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
        // It will automatically get values from the NSUserDefaults singleton
        this.endpoint = new InstanceSettings.InstanceSettings();

        var session = this.endpoint.GetSession();
        using (var loader = new DestinationsLoader(session))
        {
          return await loader.LoadOnlyDestionations();
        }
      }
      catch
      {
        AlertHelper.ShowLocalizedAlertWithOkOption("AIRPORTS_DOWNLOAD_FAILURE_ALERT_TITLE", "AIRPORTS_DOWNLOAD_FAILURE_ALERT_MESSAGE");
        return null;
      }
      finally
      {
        //this.HideLoader();
      }
    }

    public override void DidReceiveMemoryWarning()
    {
      base.DidReceiveMemoryWarning();
      // Release any cached data, images, etc that aren't in use.
    }
  }
}


