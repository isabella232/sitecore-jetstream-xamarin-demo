using System;
using UIKit;
using MapKit;
using Foundation;
using SDWebImage;
using CoreAnimation;

namespace JetStreamIOSFull
{
  public class MapDelegate : MKMapViewDelegate 
  {
    protected string annotationIdentifier = "BasicAnnotation";
    UIButton detailButton; // avoid GC
    public InstanceSettings.InstanceSettings endpoint;


    public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
    {
      // try and dequeue the annotation view
      MKAnnotationView annotationView = mapView.DequeueReusableAnnotation(annotationIdentifier);   
      // if we couldn't dequeue one, create a new one
      if (annotationView == null)
        annotationView = new MKPinAnnotationView(annotation, annotationIdentifier);
      else // if we did dequeue one for reuse, assign the annotation to it
        annotationView.Annotation = annotation;

      // configure our annotation view properties
      annotationView.CanShowCallout = true;
      (annotationView as MKPinAnnotationView).AnimatesDrop = true;
      (annotationView as MKPinAnnotationView).PinColor = MKPinAnnotationColor.Green;
      annotationView.Selected = true;

      // you can add an accessory view; in this case, a button on the right and an image on the left
      detailButton = UIButton.FromType(UIButtonType.DetailDisclosure);
      detailButton.TouchUpInside += (s, e) => {
        Console.WriteLine ("Clicked");
        new UIAlertView("Annotation Clicked", "You clicked on " +
          (annotation as MKAnnotation).Coordinate.Latitude.ToString() + ", " +
          (annotation as MKAnnotation).Coordinate.Longitude.ToString() , null, "OK", null).Show();
      };

      annotationView.RightCalloutAccessoryView = detailButton;

      string instanceUrl = this.endpoint.InstanceUrl;
      string imagePath = annotation.GetTitle();
      NSUrl imageUrl = new NSUrl(String.Concat(instanceUrl, imagePath));

      SDWebImageDownloader.SharedDownloader.DownloadImage (
        url: imageUrl,
        options: SDWebImageDownloaderOptions.LowPriority,
        progressHandler: (receivedSize, expectedSize) => {
        // Track progress...
      },
        completedHandler: (image, data, error, finished) => {

        if (image != null && finished) {
          InvokeOnMainThread ( () => {
            CALayer imageLayer = annotationView.Layer;
            imageLayer.CornerRadius = image.Size.Height/2;
            imageLayer.BorderWidth = 1;
            imageLayer.MasksToBounds = true;
            annotationView.Image = image;// = new UIImageView(image);          
          });
        }

      }
      );
      return annotationView;
    }     

    // as an optimization, you should override this method to add or remove annotations as the
    // map zooms in or out.
    public override void RegionChanged (MKMapView mapView, bool animated) {}
  }
}

