using System;
using CoreLocation;
using MapKit;
using UIKit;

namespace JetStreamIOSFull
{
  public class DestinationAnnotation : MKAnnotation
  {
    private string title;
    private CLLocationCoordinate2D coord;
    private UIImage image;
    private int hiddenCount;

    public delegate void HiddenCountChanged(int count);
    public event HiddenCountChanged onHiddenCount;

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

    public DestinationAnnotation(string title, UIImage image, CLLocationCoordinate2D coord)
    {
      this.title = title;
      this.coord = coord;

      UIImage resizedImage = ImageResize.ResizeImage(image, 90, 90);  
      this.image = resizedImage;
    }

    public override string Title 
    {
      get 
      {
        return this.title;
      }
    }

    public UIImage Image 
    {
      get 
      {
        return this.image;
      }
    }

    public override CLLocationCoordinate2D Coordinate 
    {
      get 
      {
        return this.coord;
      }
    }
  }
}