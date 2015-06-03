using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UIKit;
using MapKit;
using Foundation;
using CoreLocation;

using JetStreamCommons;
using JetStreamIOSFull.MapUI;
using JetStreamCommons.Destinations;
using JetStreamIOSFull.Helpers;
using SDWebImage;


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
      this.NavigationController.NavigationBar.BackgroundColor = this.Appearance.MenuBackgroundColor;
      this.Title = NSBundle.MainBundle.LocalizedString("DESTINATION_SCREEN_TITLE", null);

      this.InitializeMap();
    }

    private void InitializeMap()
    {
      this.mapManager = new MapManager(this.Appearance);
      this.map.Delegate = mapManager;

      MKCoordinateRegion region = this.Appearance.MapInitialRegion;
      this.map.SetRegion(region, false);
    }

    public override void ViewDidAppear(bool animated)
    {
      base.ViewDidAppear(animated);

      this.RefreshMap();
    }

    partial void RefreshButtonTouched(Foundation.NSObject sender)
    {
      SDWebImage.SDWebImageManager.SharedManager.ImageCache.ClearDisk();
      SDWebImage.SDWebImageManager.SharedManager.ImageCache.ClearMemory();

      this.map.RemoveAnnotations(this.map.Annotations);

      this.RefreshMap();
    }

    private async void RefreshMap()
    {
      bool destinationsLoaded = false;
      try
      {
        this.destinations = await this.DownloadAllDestinations();
        destinationsLoaded = true;
      }
      catch
      {
        destinationsLoaded = false;
        AlertHelper.ShowLocalizedAlertWithOkOption("NETWORK_ERROR_TITLE", "CANNOT_DOWNLOAD_DESTINATIONS_ERROR");
      }
  
      if (destinationsLoaded)
      {
        this.ShowCurrentDestinationsOnMap();
      }

    }

    private void ShowCurrentDestinationsOnMap()
    {
      this.mapManager.ResetMapState(this.map);

      List<DestinationAnnotation> annotationsList = new List<DestinationAnnotation>();

      foreach(IDestination elem in this.destinations)
      {
        bool coordinatesAvailable = (elem.Latitude != 0.0) && (elem.Longitude != 0.0); 
        if (coordinatesAvailable)
        {
          CLLocationCoordinate2D coordinates = new CLLocationCoordinate2D(elem.Latitude, elem.Longitude);

          string instanceUrl = this.Endpoint.InstanceUrl;
          string imagePath = elem.ImagePath;
          string imageUrl = String.Concat(instanceUrl, imagePath);

          DestinationAnnotation annotation = new DestinationAnnotation(elem.DisplayName, imageUrl, coordinates);
          annotationsList.Add(annotation);
        }
      }

      this.mapManager.SetAnnotationsForMap(this.map, annotationsList);
    }

    private async Task<IEnumerable> DownloadAllDestinations()
    {
     
        //FIXME: error here, some objects must be disposed!!!
        var session = this.Endpoint.GetSession();
        using (var loader = new DestinationsLoader(session))
        {
          return await loader.LoadOnlyDestinations();
        }
    }
  }
}


