﻿using System;
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
using CoreGraphics;
using JetStreamIOSFull.BaseVC;
using JetStreamIOSFull.DestinationDetails;
using ObjCRuntime;
using InstanceSettings;


namespace JetStreamIOSFull.MapUI
{
  public partial class DetailViewController : BaseViewController, IMKMapViewDelegate
  {
    private static nfloat carouselLinkHeight = 40;
    private bool caruselAnimationIsRunning = false;

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
          detailsVc.InstancesManager = this.InstancesManager;
          detailsVc.Appearance = this.Appearance;
          detailsVc.ShowDestinationDetails(this.currentSelectedDestination);
        }
      }
    }

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();

      this.RefreshButton = new UIBarButtonItem (UIBarButtonSystemItem.Refresh
        , (sender, args) =>
      {
        this.RefreshButtonTouched(this.RefreshButton);
      });

      this.InitializeMap();

      this.DetailsCarousel.BackgroundColor = UIColor.Clear;
      this.DetailsCarousel.BackgroundView = new UIView(new CGRect (0, 0, 0, 0));

      this.RegisterCarouselSwipes();
    }

    public override void ViewDidAppear(bool animated)
    {
      base.ViewDidAppear(animated);

      UIDevice thisDevice = UIDevice.CurrentDevice;

      if (thisDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone)
      {
        this.RealNavigationItem.SetRightBarButtonItem(this.RefreshButton, false);
      }
      else
      {
        this.NavigationItem.SetRightBarButtonItem(this.RefreshButton, false);
      }

      this.RefreshMap();
    }

    [Export("animationDidStop:finished:context:")]
    void SlideStopped (NSString animationID, NSNumber finished, NSObject context)
    {
      this.caruselAnimationIsRunning = false;
    }

    partial void RefreshButtonTouched(Foundation.NSObject sender)
    {
      AnalyticsHelper.TrackRefreshButtonTouch();

      SDWebImage.SDWebImageManager.SharedManager.ImageCache.ClearDisk();
      SDWebImage.SDWebImageManager.SharedManager.ImageCache.CleanDisk();
      SDWebImage.SDWebImageManager.SharedManager.ImageCache.ClearMemory();

      this.RefreshMap();
    }

    private void ClearData()
    {
      this.map.RemoveAnnotations(this.map.Annotations);
      this.DetailsCarousel.DataSource = null;
      this.DetailsCarousel.ReloadData();
    }
     
    #region Map

    private void InitializeMap()
    {
      this.mapManager = new MapManager(this.Appearance);
      this.mapManager.onDestinationSelected += this.DidSelectDestination;
      this.map.Delegate = mapManager;

      MKCoordinateRegion region = this.Appearance.Map.InitialRegion;
      this.map.SetRegion(region, false);
    }

    private void DidSelectDestination(IDestination destination)
    {
      this.currentSelectedDestination = destination;
      this.PerformSegue(DESTINATION_DETAIL_SEGUE_ID, this);
    }

    private async void RefreshMap()
    {
      this.ShowLoader();

      if (RefreshButton != null)
      {
        this.RefreshButton.Enabled = false;
      }

      try
      {
        this.destinations = await this.DownloadAllDestinations();
        this.ShowCurrentDestinationsOnMap();
      }
      catch
      {
        AlertHelper.ShowLocalizedAlertWithOkOption("NETWORK_ERROR_TITLE", "CANNOT_DOWNLOAD_DESTINATIONS_ERROR");
      }
      finally
      {
        if (RefreshButton != null)
        {
          this.RefreshButton.Enabled = true;
        }
        this.HideLoader();
      }
    }

    private void ShowCurrentDestinationsOnMap()
    {
      this.mapManager.ResetMapState(this.map);

      List<DestinationAnnotation> annotationsList = new List<DestinationAnnotation>();

      foreach(IDestination elem in this.destinations)
      {
        DestinationAnnotation annotation = new DestinationAnnotation(elem, this.InstancesManager.ActiveInstance.InstanceUrl);
          annotationsList.Add(annotation);
      }

      if (annotationsList.Count == 0)
      {
        AlertHelper.ShowLocalizedAlertWithOkOption("MESSAGE", "DESTINATIONS NOT FOUND");
        return;
      }

      this.mapManager.SetAnnotationsForMap(this.map, annotationsList);

      CarouselDataSource carouselSource = new CarouselDataSource (annotationsList);
      carouselSource.onItemSelected += this.DidSelectDestination;

      this.DetailsCarousel.Source = carouselSource;
      this.DetailsCarousel.ReloadData();
    }

    #endregion Map

    #region Carousel

    private void RegisterCarouselSwipes()
    {
      UISwipeGestureRecognizer swipeUpGesture = new UISwipeGestureRecognizer(sw => 
      {
        this.showCarousel(true);
      });
      swipeUpGesture.Direction = UISwipeGestureRecognizerDirection.Up;
      this.DetailsCarousel.AddGestureRecognizer(swipeUpGesture);

      UISwipeGestureRecognizer swipeDownGesture = new UISwipeGestureRecognizer(sw => 
      {
        this.hideCarousel(true);
      });
      swipeDownGesture.Direction = UISwipeGestureRecognizerDirection.Down;
      this.DetailsCarousel.AddGestureRecognizer(swipeDownGesture);
    }

    private void showCarousel(bool animated)
    {
      nfloat height = this.View.Bounds.Height - this.DetailsCarousel.Bounds.Height;
      this.MoveCarouselToCoordiantes(height, animated);
    }

    private void hideCarousel(bool animated)
    {
      nfloat height = this.View.Bounds.Height - carouselLinkHeight;
      this.MoveCarouselToCoordiantes(height, animated);
    }

    private void MoveCarouselToCoordiantes(nfloat height, bool animated)
    {
      if (this.caruselAnimationIsRunning == false)
      {
        if (animated)
        {
          this.caruselAnimationIsRunning = true;
          UIView.BeginAnimations("slideAnimation");

          UIView.SetAnimationDuration(0.7);
          UIView.SetAnimationCurve(UIViewAnimationCurve.EaseInOut);

          UIView.SetAnimationDelegate(this);
          UIView.SetAnimationDidStopSelector(new Selector ("animationDidStop:finished:context:"));
        }

        CGRect frame = this.DetailsCarousel.Frame;
        frame.Y = height;
        this.DetailsCarousel.Frame = frame;

        if (animated)
        {
          UIView.CommitAnimations();
        }
      }
    }

    #endregion Carousel

    #region Network

    private async Task<IEnumerable> DownloadAllDestinations()
    {
      using (var session = this.InstancesManager.ActiveInstance.GetSession())
      {
        using (var loader = new DestinationsLoader (session))
        {
          try
          {
            return await loader.LoadOnlyDestinations(true);
          }
          catch(Exception ex)
          {
            Console.WriteLine("DestinationsLoader exception");
            throw ex;
          }
        }
      }
    }

    #endregion Network

  }
}


