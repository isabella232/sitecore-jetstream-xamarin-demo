using Jetstream.Models;

namespace Jetstream.UI.Fragments
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Android.Content;
  using Android.Gms.Common;
  using Android.Gms.Maps;
  using Android.Gms.Maps.Model;
  using Android.Gms.Maps.Utils.Clustering;
  using Android.OS;
  using Android.Support.V4.App;
  using Android.Support.V4.Widget;
  using Android.Util;
  using Android.Views;
  using Android.Widget;
  using com.dbeattie;
  using DSoft.Messaging;
  using Jetstream.Map;
  using Jetstream.UI.Anim;
  using JetStreamCommons;
  using JetStreamCommons.Destinations;
  using Squareup.Picasso;

  public class DestinationsOnMapFragment : Fragment, IOnMapReadyCallback, IActionClickListener, ClusterManager.IOnClusterItemClickListener
  {
    GoogleMap map;
    ClusterManager clusterManager;

    LinearLayout cardsContainer;
    SwipeRefreshLayout refresher;
    MapView mapView;

    MessageBusEventHandler updateInstanceUrlEventHandler;

    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
      var view = inflater.Inflate(Jetstream.Resource.Layout.fragment_map_destinations, container, false);

      this.refresher = view.FindViewById<SwipeRefreshLayout>(Jetstream.Resource.Id.refresher);
      this.mapView = view.FindViewById<MapView>(Jetstream.Resource.Id.mapview);
      this.cardsContainer = view.FindViewById<LinearLayout>(Jetstream.Resource.Id.cards_container);

      this.refresher.SetColorScheme(Android.Resource.Color.White);
      this.refresher.SetProgressBackgroundColorSchemeResource(Jetstream.Resource.Color.color_accent);

      this.refresher.SetProgressViewOffset(false, 0, (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 24, this.Resources.DisplayMetrics));

      this.mapView.OnCreate(savedInstanceState);

      try
      {
        MapsInitializer.Initialize(this.Activity);
      }
      catch (GooglePlayServicesNotAvailableException e)
      {
        //TODO: Log error here
      }

      return view;
    }

    public void OnMapReady(GoogleMap googleMap)
    {
      this.map = googleMap;
      this.map.MapType = GoogleMap.MapTypeNormal;

      this.clusterManager = new ClusterManager(this.Activity, this.map);
      this.clusterManager.SetRenderer(new JetstreamClusterRenderer(this.Activity, this.map, this.clusterManager));
      this.clusterManager.SetOnClusterItemClickListener(this);
      this.map.CameraChange += this.CenterMapOnEurope;
      this.map.SetOnMarkerClickListener(this.clusterManager);

      this.LoadDestinations();
    }

    private void CenterMapOnEurope(object sender, GoogleMap.CameraChangeEventArgs cameraChangeEventArgs)
    {
      // Hardcoded Europe position to center on.
      var bounds = LatLngBounds.InvokeBuilder().Include(new LatLng(61.088913, -13.313578)).Include(new LatLng(30.811438, 44.575028)).Build();

      this.map.MoveCamera(CameraUpdateFactory.NewLatLngBounds(bounds, 0));

      this.map.CameraChange -= this.CenterMapOnEurope;
      this.map.SetOnCameraChangeListener(this.clusterManager);
    }

    async void LoadDestinations()
    {
      try
      {
        this.refresher.Refreshing = true;

        var loader = new DestinationsLoader(this.Activity.Session);
        var destinations = await loader.LoadOnlyDestinations();
        this.refresher.Refreshing = false;

        var destWithLocation = this.FilterDestinationByLocation(destinations);

        this.AddDestinationsItems(destWithLocation);
        this.InitDestinationsCards(destWithLocation);
      }
      catch (Exception exception)
      {
        Log.Error("Jetstream", "Failed to load destinations. Reason: " + exception.Message);

        this.refresher.Refreshing = false;

        SnackbarManager.Show(
          Snackbar.With(this.Activity)
            .ActionLabel("Retry")
            .ActionColor(this.Resources.GetColor(Jetstream.Resource.Color.color_accent))
            .ActionListener(this)
            .Text("Failed to load destinations, please check your internet connection."));
      }
    }

    private List<IDestination> FilterDestinationByLocation(List<IDestination> source)
    {
      return source.Where(destination => destination.IsCoordinatesAvailable).ToList();
    }

    private void InitDestinationsCards(List<IDestination> destinations)
    {
      this.cardsContainer.RemoveAllViews();

      var inflater = LayoutInflater.From(this.Activity);

      foreach (var destination in destinations)
      {
        var dest = destination;

        var cardView = inflater.Inflate(Jetstream.Resource.Layout.view_destination_card, this.cardsContainer, false);

        var destImageView = cardView.FindViewById<ImageView>(Jetstream.Resource.Id.destination_image);
        var destNameTextView = cardView.FindViewById<TextView>(Jetstream.Resource.Id.destination_name);

        destNameTextView.Text = destination.DisplayName;
        destNameTextView.SetBackgroundColor(this.Resources.GetColor(Jetstream.Resource.Color.color_primary_light));

        Picasso.With(this.Activity).Load(destination.ImageUrl(this.Activity.Session)).Into(destImageView);

        cardView.Click += (sender, args) => this.StartDestinationActivity(dest);

        this.cardsContainer.AddView(cardView);
      }
      this.cardsContainer.ViewTreeObserver.GlobalLayout += this.CardsContainerViewTreeObserverGlobalLayout;
    }

    void CardsContainerViewTreeObserverGlobalLayout (object sender, EventArgs e)
    {
      DestinationsCardsAnimHelper.AnimateAppearance(this.cardsContainer);
      this.cardsContainer.ViewTreeObserver.GlobalLayout -= this.CardsContainerViewTreeObserverGlobalLayout;
    }

    private void AddDestinationsItems(List<IDestination> destinations)
    {
      var clusterItems = destinations.Select(destination => new ClusterItem(destination, destination.ImageUrl(this.Activity.Session)))
        .ToList();

      this.map.Clear();

      this.clusterManager.ClearItems();
      this.clusterManager.AddItems(clusterItems);
      this.clusterManager.Cluster();
    }

    private void StartDestinationActivity(IDestination dest)
    {

      var parcebleDest = string.Join(DestinationAndroidSpec.SplitSymbol.ToString(), dest.ImageUrl(this.Activity.Session), dest.Overview, dest.DisplayName);

      var intent = new Intent(this.Activity, typeof(DestinationActivity));
      intent.PutExtra(DestinationActivity.DestinationParamIntentKey, parcebleDest);

      this.StartActivity(intent);
    }

    public bool OnClusterClick(ICluster cluster)
    {
      return false;
    }

    public bool OnClusterItemClick(Java.Lang.Object item)
    {
      this.StartDestinationActivity(((ClusterItem)item).Wrapped);
      return false;
    }

    public void OnActionClicked(Snackbar snackbar)
    {
      this.LoadDestinations();
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
      if (this.map == null)
      {
        this.mapView.GetMapAsync(this);
      }

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
          })
        };
      }

      MessageBus.Default.Register(this.updateInstanceUrlEventHandler);
    }

    public override void OnDestroy()
    {
      base.OnDestroy();

      this.mapView.OnDestroy();
      MessageBus.Default.DeRegister(this.updateInstanceUrlEventHandler);
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