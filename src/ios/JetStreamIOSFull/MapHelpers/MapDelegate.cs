using System;
using UIKit;
using MapKit;
using Foundation;
using SDWebImage;
using CoreAnimation;
using System.Collections.Generic;

namespace JetStreamIOSFull
{
  public class MapDelegate : MKMapViewDelegate 
  {
    private static double iphoneScaleFactorLatitude = 1024.0 / 100;
    private static double iphoneScaleFactorLongitude = 768.0 / 100;

    protected string annotationIdentifier = "BasicAnnotation";
    public InstanceSettings.InstanceSettings endpoint;

    private List<DestinationAnnotation> annotations = new List<DestinationAnnotation>();

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

    public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation ann)
    {
      DestinationAnnotation annotation = ann as DestinationAnnotation;
      AnnotationViewWithRoundedImage annotationView = mapView.DequeueReusableAnnotation(annotationIdentifier) as AnnotationViewWithRoundedImage;   

      if (annotationView == null)
        annotationView = new AnnotationViewWithRoundedImage(annotation, annotationIdentifier);
      else 
        annotationView.Annotation = annotation;

      annotationView.CanShowCallout = true;

      if (annotation is DestinationAnnotation)
      {
        DestinationAnnotation castedAnnotation = annotation as DestinationAnnotation;
        UIImage image = castedAnnotation.Image;

        if ( image != null) 
        {
            annotationView.Image = image;
        }
      }

      return annotationView;
    }     

    public void FilterAnnotations(MKMapView mapView)
    {
      InvokeOnMainThread(() =>
      {
        
        double latDelta = Math.Truncate(mapView.Region.Span.LatitudeDelta/iphoneScaleFactorLatitude);
        double longDelta = Math.Truncate(mapView.Region.Span.LongitudeDelta/iphoneScaleFactorLongitude);

        List<DestinationAnnotation> shopsToShow = new List<DestinationAnnotation>();

        for (int i = 0; i < this.annotations.Count; ++i) 
        {
          DestinationAnnotation checkingLocation = this.annotations[i];
          double latitude = checkingLocation.Coordinate.Latitude;
          double longitude = checkingLocation.Coordinate.Longitude;

          bool shouldBeHiden = false;

          foreach (DestinationAnnotation tempPlacemark in shopsToShow) 
          {
            shouldBeHiden = Math.Abs(tempPlacemark.Coordinate.Latitude - latitude) < latDelta
                              && Math.Abs(tempPlacemark.Coordinate.Longitude - longitude) < longDelta;

            if (shouldBeHiden)
            {
              InvokeOnMainThread(() =>
              {
                mapView.RemoveAnnotation(checkingLocation);
                tempPlacemark.HiddenCount += checkingLocation.HiddenCount;
                checkingLocation.HiddenCount = 0;
                ++tempPlacemark.HiddenCount;
              });
              break;
            }
          }

          if (!shouldBeHiden)
          {
            checkingLocation.HiddenCount = 0;
            shopsToShow.Add(checkingLocation);
          }

        }

        foreach (IMKAnnotation annotation in shopsToShow) 
        {
          
          mapView.AddAnnotation(annotation);
        }
      });

    }

      
    public override void RegionChanged (MKMapView mapView, bool animated)
    {
      this.FilterAnnotations(mapView); 
    }
  }
}

