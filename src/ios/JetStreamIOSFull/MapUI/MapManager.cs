﻿using MapKit;
using System.Collections.Generic;
using UIKit;
using System;
using JetStreamIOSFull.Helpers;
using CoreAnimation;
using CoreGraphics;
using JetStreamCommons.Destinations;

namespace JetStreamIOSFull.MapUI
{
  public class MapManager : MKMapViewDelegate 
  {
    private readonly double iphoneScaleFactorLatitude;
    private readonly double iphoneScaleFactorLongitude;

    private double prevZoomLevel = 0;

    protected string annotationIdentifier = "BasicAnnotation";

    private List<DestinationAnnotation> annotations = new List<DestinationAnnotation>();

    private AppearanceHelper appearanceHelper;

    public delegate void DestinationSelected(IDestination destination);
    public event DestinationSelected onDestinationSelected;

    public MapManager(AppearanceHelper appearance)
    {
      this.appearanceHelper = appearance;

      double correctionKeof = 1.2;
      this.iphoneScaleFactorLatitude = UIScreen.MainScreen.Bounds.Width/(appearance.Map.DestinationIconSize/correctionKeof);
      this.iphoneScaleFactorLongitude = UIScreen.MainScreen.Bounds.Height/(appearance.Map.DestinationIconSize/correctionKeof);
    }

    public void SetAnnotationsForMap(MKMapView mapView, List<DestinationAnnotation> annotations)
    {
      this.annotations = annotations;
      this.FilterAnnotations(mapView);
    }

    public void  AddAnnotationForMap(MKMapView mapView,  DestinationAnnotation annotation)
    {
      annotations.Add(annotation);
      this.FilterAnnotations(mapView);
    }

    public void ResetMapState(MKMapView mapView)
    {
      mapView.RemoveAnnotations(mapView.Annotations);
      this.annotations = new List<DestinationAnnotation>();
    }

    public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation ann)
    {
      DestinationAnnotation annotation = ann as DestinationAnnotation;
      AnnotationViewWithRoundedImage annotationView = mapView.DequeueReusableAnnotation(annotationIdentifier) as AnnotationViewWithRoundedImage;   

      if (annotationView == null)
      {
        annotationView = new AnnotationViewWithRoundedImage(annotation, annotationIdentifier, this.appearanceHelper);
      }
      else
      {
        annotationView.Annotation = annotation;
      }
        
      annotationView.HiddenCountChanged(annotation.HiddenCount);

      return annotationView;
    }     

    private void ClearAnnotations(MKMapView mapView)
    {
        for (int i = 0; i < this.annotations.Count; ++i)
        {
          DestinationAnnotation checkingLocation = this.annotations[i];
          checkingLocation.HiddenCount = 1;
        }
    }

    private void FilterAnnotations(MKMapView mapView)
    {
      InvokeOnMainThread(() =>
      {
        this.ClearAnnotations(mapView);

        double latDelta = (mapView.Region.Span.LatitudeDelta/iphoneScaleFactorLatitude);
        double longDelta = (mapView.Region.Span.LongitudeDelta/iphoneScaleFactorLongitude);

        List<DestinationAnnotation> shopsToShow = new List<DestinationAnnotation>();

        for (int i = this.annotations.Count-1; i >=0 ; --i) 
        {
          DestinationAnnotation checkingLocation = this.annotations[i];
          double latitude = checkingLocation.initialCoord.Latitude;
          double longitude = checkingLocation.initialCoord.Longitude;

          bool shouldBeHiden = false;

          foreach (DestinationAnnotation tempPlacemark in shopsToShow) 
          {
            shouldBeHiden = Math.Abs(tempPlacemark.initialCoord.Latitude - latitude) < latDelta
              && Math.Abs(tempPlacemark.initialCoord.Longitude - longitude) < longDelta;

            if (shouldBeHiden)
            {
              tempPlacemark.HiddenCount = tempPlacemark.HiddenCount + 1;
              checkingLocation.HiddenCount = -1;

              checkingLocation.MoveToCoordinatesWithAnimation(tempPlacemark.Coordinate, finished => 
              {
                mapView.RemoveAnnotation(checkingLocation); 
              });

              break;
            }
          }

          if (!shouldBeHiden)
          {
            shopsToShow.Add(checkingLocation);

            bool shouldAdd = true;
            foreach (IMKAnnotation ann in mapView.Annotations)
            {
              if (ann == checkingLocation)
              {
                shouldAdd = false;
              }
            }

            if (shouldAdd)
            {
              mapView.AddAnnotation(checkingLocation);
            }

            checkingLocation.MoveToCoordinatesWithAnimation(checkingLocation.initialCoord, finished => {});
          }
        }
      });

    }

    public override void RegionChanged (MKMapView mapView, bool animated)
    {
      double newZoomLevel = MapHelper.GetZoomLevel(mapView);
      bool shouldRefilter = Math.Abs(newZoomLevel - prevZoomLevel) > 0.3;

      if (shouldRefilter)
      {
        this.FilterAnnotations(mapView); 
        prevZoomLevel = newZoomLevel;
      }

    }

    public override void DidSelectAnnotationView(MKMapView mapView, MKAnnotationView view)
    {
      AnnotationViewWithRoundedImage castedAnnotation = view as AnnotationViewWithRoundedImage;
      if (castedAnnotation != null && !castedAnnotation.IsGroup)
      {
        onDestinationSelected(castedAnnotation.Destination);
        mapView.DeselectAnnotation(castedAnnotation.Annotation, true);
      }
    }
  }
}

