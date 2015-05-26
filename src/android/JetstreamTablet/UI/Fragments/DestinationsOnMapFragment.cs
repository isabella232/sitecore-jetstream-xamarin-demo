namespace Jetstream.UI.Fragments
{
  using System;
  using System.Collections.Generic;
  using Android.App;
  using Android.Gms.Common;
  using Android.Gms.Maps;
  using Android.Gms.Maps.Model;
  using Android.Graphics;
  using Android.OS;
  using Android.Support.V4.Widget;
  using Android.Util;
  using Android.Views;
  using com.dbeattie;
  using Com.Lilarcor.Cheeseknife;
  using Jetstream.Bitmap;
  using JetStreamCommons;
  using JetStreamCommons.Destinations;
  using Squareup.Picasso;

  public class DestinationsOnMapFragment : Fragment, IOnMapReadyCallback, IActionClickListener
  {
    const double Tolerance = 0.01;

    GoogleMap map;

    [InjectView(Jetstream.Resource.Id.refresher)]
    SwipeRefreshLayout refresher;

    [InjectView(Jetstream.Resource.Id.mapview)]
    MapView mapView;

    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
      var view = inflater.Inflate(Jetstream.Resource.Layout.fragment_map_destinations, container, false);

      Cheeseknife.Inject(this, view);

      this.refresher.SetColorScheme(Android.Resource.Color.HoloBlueDark,
       Android.Resource.Color.HoloPurple,
       Android.Resource.Color.DarkerGray,
       Android.Resource.Color.HoloGreenDark);

      this.refresher.SetProgressViewOffset(false, 0, (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 24, this.Resources.DisplayMetrics));

      this.mapView.OnCreate(savedInstanceState);

      try {
        MapsInitializer.Initialize(this.Activity);
      } catch (GooglePlayServicesNotAvailableException e) {
        //TODO: Log error here
      }

      return view;
    }

    public void OnMapReady(GoogleMap googleMap)
    {
      this.map = googleMap;
      this.map.MapType = GoogleMap.MapTypeSatellite;
      this.LoadDestinations();
    }

    async void LoadDestinations()
    {
      try
      {
        this.refresher.Refreshing = true;

        var loader = new DestinationsLoader(this.Activity.Session);
        var destinations = await loader.LoadOnlyDestinations();
        this.refresher.Refreshing = false;

        this.ShowDestinationsOnMap(destinations);
      }
      catch (Exception exception)
      {
        Log.Error("Jetstream", "Failed to load destinations. Reason: " + exception.Message);
        
        this.refresher.Refreshing = false;

        SnackbarManager.Show(
          Snackbar.With(this.Activity)
            .ActionLabel("Retry")
            .ActionColor(Color.Yellow) 
            .ActionListener(this)
            .Text("Failed to load destinations, please check your internet connection."));
      }
    }

    public void OnActionClicked(Snackbar snackbar)
    {
      this.LoadDestinations();
    }

    void ShowDestinationsOnMap(List<IDestination> destinations)
    {
      try
      {
        foreach (var dest in destinations)
        {
          var longitude = dest.Longitude;
          var latitude = dest.Latitude;

          var longZero = Math.Abs(longitude) < Tolerance;
          var latZero = Math.Abs(latitude) < Tolerance;

          if (longZero || latZero)
          {
            //TODO: Add logger message here
            continue;
          }

          var markerOptions = new MarkerOptions();
          markerOptions.SetPosition(new LatLng(latitude, longitude));

          markerOptions.SetTitle(dest.DisplayName);

          var marker = this.map.AddMarker(markerOptions);

          //TODO: Fix this hardcoded url prefix. 
          var url = "http://" + this.Activity.Session.MediaDownloadUrl(dest.ImagePath);

          Picasso.With(this.Activity).Load(url).Resize(100, 100).Into(new MarkerTarget(dest, marker));
        }
      }
      catch (Exception exception)
      {
        //TODO: Add logger message here
      }
    }

    private new MainActivity Activity
    {
      get
      {
        return base.Activity as MainActivity;
      }
    }

    public override void OnResume()
    {
      base.OnResume();

      this.mapView.OnResume();
      this.mapView.GetMapAsync(this);
    }

    public override void OnDestroy()
    {
      base.OnDestroy();

      this.mapView.OnDestroy();
    }

    public override void OnPause()
    {
      base.OnPause();

      this.mapView.OnPause();
    }

    public override void OnLowMemory()
    {
      base.OnLowMemory();

      this.mapView.OnLowMemory();
    }
  }
}