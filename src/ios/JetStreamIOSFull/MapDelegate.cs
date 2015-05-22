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
      MKAnnotationView annotationView = mapView.DequeueReusableAnnotation(annotationIdentifier);   
      if (annotationView == null)
        annotationView = new MKPinAnnotationView(annotation, annotationIdentifier);
      else 
        annotationView.Annotation = annotation;

      annotationView.CanShowCallout = false;
      (annotationView as MKPinAnnotationView).AnimatesDrop = true;


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
            UIImage resizedImage = ImageResize.ResizeImage(image, 100, 100);  
            CALayer imageLayer = annotationView.Layer;
            imageLayer.CornerRadius = resizedImage.Size.Height/2;
            imageLayer.BorderWidth = 1;
            imageLayer.MasksToBounds = true;
            annotationView.Image = resizedImage;
          });
        }

      }
      );
      return annotationView;
    }     
     

    public override void RegionChanged (MKMapView mapView, bool animated) {}
  }
}

