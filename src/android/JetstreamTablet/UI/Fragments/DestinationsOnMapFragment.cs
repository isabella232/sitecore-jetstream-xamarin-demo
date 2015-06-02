namespace Jetstream.UI.Fragments
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Android.App;
  using Android.Gms.Common;
  using Android.Gms.Maps;
  using Android.Gms.Maps.Model;
  using Android.Gms.Maps.Utils.Clustering;
  using Android.Graphics;
  using Android.OS;
  using Android.Support.V4.Widget;
  using Android.Util;
  using Android.Views;
  using Android.Widget;
  using com.dbeattie;
  using DSoft.Messaging;
  using Jetstream.Map;
  using JetStreamCommons;
  using JetStreamCommons.Destinations;

  public class DestinationsOnMapFragment : Fragment, IOnMapReadyCallback, IActionClickListener, ClusterManager.IOnClusterClickListener, ClusterManager.IOnClusterItemClickListener
  {
    const double Tolerance = 0.01;

    GoogleMap map;
    ClusterManager clusterManager;

    SwipeRefreshLayout refresher;
    MapView mapView;

    MessageBusEventHandler updateInstanceUrlEventHandler;

    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
      var view = inflater.Inflate(Jetstream.Resource.Layout.fragment_map_destinations, container, false);

      this.refresher = view.FindViewById<SwipeRefreshLayout>(Jetstream.Resource.Id.refresher);
      this.mapView = view.FindViewById<MapView>(Jetstream.Resource.Id.mapview);

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
//
      this.clusterManager = new ClusterManager(this.Activity, this.map);
      this.clusterManager.SetRenderer(new JetstreamClusterRenderer(this.Activity, this.map, this.clusterManager));
//      this.clusterManager.SetOnClusterClickListener(this);
//      this.clusterManager.SetOnClusterItemClickListener(this);
      this.map.SetOnCameraChangeListener(this.clusterManager);
      this.map.SetOnMarkerClickListener(this.clusterManager);

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

        this.AddDestinationsItems(destinations);
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

    private void AddDestinationsItems(List<IDestination> destinations)
    {
      var clusterItems = destinations.Where(delegate(IDestination destination)
      {
        var longZero = Math.Abs(destination.Longitude) < Tolerance;
        var latZero = Math.Abs(destination.Latitude) < Tolerance;
        return !(longZero || latZero);
      }).Select(delegate(IDestination destination)
      {
        var url = this.GetFixedUrl(this.Activity.Session.MediaDownloadUrl(destination.ImagePath));
        var title = destination.DisplayName;
        var latLng = new LatLng(destination.Latitude, destination.Longitude);

        return new ClusterItem(latLng, title, url);
      }).ToList();

      this.clusterManager.ClearItems();
      this.clusterManager.AddItems(clusterItems);
      this.clusterManager.Cluster();
    }

    public bool OnClusterClick(ICluster cluster)
    {
      Toast.MakeText(this.Activity, cluster.Items.Count + " items in cluster", ToastLength.Short).Show();
      return false;
    }

    public bool OnClusterItemClick(Java.Lang.Object marker)
    {
      Toast.MakeText(this.Activity, "Marker clicked", ToastLength.Short).Show();
      return false;
    }

    public void OnActionClicked(Snackbar snackbar)
    {
      this.LoadDestinations();
    }

    private string GetFixedUrl(string url)
    {
      if (url.StartsWith("http"))
      {
        return url;
      }

      return "http://" + url;
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

      if (this.updateInstanceUrlEventHandler == null)
      {
        this.updateInstanceUrlEventHandler = new MessageBusEventHandler()
        {
          EventId = EventIdsContainer.SitecoreInstanceUrlUpdateEvent,
          EventAction = (sender, evnt) =>
          this.Activity.RunOnUiThread(delegate
            {
              if (this.refresher.Refreshing)
              {
                return;
              }

              this.LoadDestinations();
              this.map.Clear();
            })
        };
      }

      MessageBus.Default.Register(this.updateInstanceUrlEventHandler);
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

      MessageBus.Default.DeRegister(this.updateInstanceUrlEventHandler);
    }

    public override void OnLowMemory()
    {
      base.OnLowMemory();

      this.mapView.OnLowMemory();
    }
  }
}