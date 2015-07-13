using System;
using MapKit;

namespace JetStreamIOSFull.MapUI
{
  public static class MapHelper
  {
    private static double MERCATOR_RADIUS = 85445659.44705395;

    public static double GetZoomLevel(MKMapView mapView)
    {
      double zoom = 21.00 - Math.Log(mapView.Region.Span.LongitudeDelta * MapHelper.MERCATOR_RADIUS * Math.PI / (180.0 * mapView.Bounds.Size.Width));
      return Math.Truncate(zoom * 10) / 10;
    }
  }
}

