using System;
using CoreLocation;
using MapKit;
using UIKit;
using JetStreamIOSFull.Helpers;
using JetStreamCommons.Destinations;

namespace JetStreamIOSFull.MapUI
{
  public class DestinationAnnotation : MKAnnotation
  {
    private string title;
    private CLLocationCoordinate2D coord;
    public readonly CLLocationCoordinate2D initialCoord;
    private int hiddenCount;
    private string imageUrl;

    public delegate void HiddenCountChanged(int count);
    public event HiddenCountChanged onHiddenCount;

    public readonly IDestination Destination;

    public DestinationAnnotation(IDestination destination, string instanceUrl)
    {
      this.Destination = destination;

      CLLocationCoordinate2D coordinates = new CLLocationCoordinate2D(destination.Latitude, destination.Longitude);

     
      string imagePath = destination.ImagePath;
      this.imageUrl = String.Concat(instanceUrl, imagePath);

      this.title = destination.DisplayName;
      this.coord = coordinates;
      this.initialCoord = coordinates;
    }
      

    public int HiddenCount
    {
      get
      { 
        return this.hiddenCount;
      }
      set
      { 
        this.hiddenCount = value;
        if (onHiddenCount != null)
        {
          onHiddenCount(value);
        }
      }
    }

    public override string Title 
    {
      get 
      {
        return this.title;
      }
    }

    public string ImageUrl
    {
      get 
      {
        return this.imageUrl;
      }
    }

    public override CLLocationCoordinate2D Coordinate 
    {
      get 
      {
        return this.coord;
      }    

    }
     
    public override void SetCoordinate(CLLocationCoordinate2D coordinate)
    {
      WillChangeValue("coordinate");

      this.coord = coordinate;

      DidChangeValue("coordinate");
    }

    public void MoveToCoordinatesWithAnimation(CLLocationCoordinate2D coordinate, UICompletionHandler completion)
    {
      if (this.coord.Latitude != coordinate.Latitude || this.coord.Longitude != coordinate.Longitude)
      {
        float springDampingRatio = 0.7f;
        float initialSpringVelocity = 2.0f;
        float duration = 0.7f;

        System.Threading.ThreadPool.QueueUserWorkItem(state =>
        {
          InvokeOnMainThread(() =>
          {

            UIView.AnimateNotify(duration, 0.0, springDampingRatio, initialSpringVelocity, UIViewAnimationOptions.AllowUserInteraction, () =>
            {
              this.SetCoordinate(coordinate);
            }, completion);

          });
        });
      }
    }
  
  }
}