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
    private readonly string DESTINATION_DETAIL_SEGUE_ID = "ShowDestinationDetails";
    private IEnumerable destinations;
    private MapManager mapManager;

    private IDestination currentSelectedDestination = null;

    public DetailViewController(IntPtr handle) : base(handle)
    {
      
    }

    public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
    {
      if (segue.Identifier.Equals(DESTINATION_DETAIL_SEGUE_ID))
      {
        DestinationDetailsViewController detailsVc = segue.DestinationViewController as DestinationDetailsViewController;
        if (detailsVc != null)
        {
          detailsVc.Endpoint = this.Endpoint;
          detailsVc.Appearance = this.Appearance;
          detailsVc.ShowDestinationDetails(this.currentSelectedDestination);
        }
      }
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
      this.mapManager.onDestinationSelected += this.DidSelectDestination;
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

    private void DidSelectDestination(IDestination destination)
    {
      this.currentSelectedDestination = destination;
      this.PerformSegue(DESTINATION_DETAIL_SEGUE_ID, this);
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
        if (elem.CoordinatesIsAvailable)
        {
          DestinationAnnotation annotation = new DestinationAnnotation(elem, this.Endpoint.InstanceUrl);
          annotationsList.Add(annotation);
        }
      }

      this.mapManager.SetAnnotationsForMap(this.map, annotationsList);
    }

    private async Task<IEnumerable> DownloadAllDestinations()
    {
      using (var session = this.Endpoint.GetSession())
      {
        using (var loader = new DestinationsLoader (session))
        {
          try
          {
            return await loader.LoadOnlyDestinations();
          }
          catch(Exception ex)
          {
            AlertHelper.ShowLocalizedAlertWithOkOption("NETWORK_ERROR_TITLE", "CANNOT_DOWNLOAD_DESTINATIONS_ERROR");
            throw ex;
          }
        }
      }
    }
  }
}


