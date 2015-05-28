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
    private UIImage image;
    private int hiddenCount;

    private IAppearanceHelper appearanceHelper;

    public delegate void HiddenCountChanged(int count);
    public event HiddenCountChanged onHiddenCount;

    public DestinationAnnotation(string title, UIImage image, CLLocationCoordinate2D coord, IAppearanceHelper appearance)
    {
      this.appearanceHelper = appearance;

      this.title = title;
      this.coord = coord;

      UIImage resizedImage = ImageResize.ResizeImage(image, appearanceHelper.DestinationIconSize, appearanceHelper.DestinationIconSize);  
      this.image = resizedImage;
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