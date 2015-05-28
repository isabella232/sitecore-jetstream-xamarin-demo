using System;
using CoreLocation;
using MapKit;
using UIKit;
using JetStreamIOSFull.Helpers;

namespace JetStreamIOSFull.MapUI
{
  public class DestinationAnnotation : MKAnnotation
  {
    private string title;
    private CLLocationCoordinate2D coord;
    private int hiddenCount;
    private string imageUrl;

    public delegate void HiddenCountChanged(int count);
    public event HiddenCountChanged onHiddenCount;

    public DestinationAnnotation(string title, string imageUrl, CLLocationCoordinate2D coord)
    {
      this.imageUrl = imageUrl;
      this.title = title;
      this.coord = coord;
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
  }
}