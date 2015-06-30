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
  using Android.Util;
  using Android.Views;
  using Android.Widget;
  using com.dbeattie;
  using Com.Lsjwzh.Widget.Materialloadingprogressbar;
  using DSoft.Messaging;
  using JetStreamCommons;
  using Jetstream.Map;
  using Jetstream.Models;
  using Jetstream.UI.Activities;
  using Jetstream.UI.Anim;
  using Jetstream.Utils;
  using JetStreamCommons.Destinations;
  using Square.Picasso;

  public class DestinationsOnMapFragment : Fragment, IOnMapReadyCallback, IActionClickListener, ClusterManager.IOnClusterItemClickListener
  {
    GoogleMap map;
    ClusterManager clusterManager;

    LinearLayout cardsContainer;
    CircleProgressBar refresher;
    MapView mapView;

    MessageBusEventHandler updateInstanceUrlEventHandler;
    MessageBusEventHandler refreshEventHandler;

    bool refreshOnHiddenChanged = false;

    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
      var view = inflater.Inflate(Jetstream.Resource.Layout.fragment_map_destinations, container, false);

      this.refresher = view.FindViewById<CircleProgressBar>(Jetstream.Resource.Id.refresher);
      this.mapView = view.FindViewById<MapView>(Jetstream.Resource.Id.mapview);
      this.cardsContainer = view.FindViewById<LinearLayout>(Jetstream.Resource.Id.cards_container);

      this.refresher.SetColorSchemeResources(Android.Resource.Color.White);
      this.refresher.SetCircleBackgroundEnabled(true);
      this.refresher.SetBackgroundColor(Resources.GetColor(Jetstream.Resource.Color.accent));

      this.mapView.OnCreate(savedInstanceState);

      try
      {
        MapsInitializer.Initialize(this.Activity);
      }
      catch (GooglePlayServicesNotAvailableException e)
      {
        AppLog.Logger.Error("Google Play Services not avalible on this device.", e);
      }

      return view;
    }

    public override void OnHiddenChanged(bool hidden)
    {
      base.OnHiddenChanged(hidden);

      if(!hidden && this.refreshOnHiddenChanged)
      {
        this.refreshOnHiddenChanged = false;
        this.LoadDestinations();
      }
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
        this.refresher.Visibility = ViewStates.Visible;

        var loader = new DestinationsLoader(this.Activity.GetSession());
        var destinations = await loader.LoadOnlyDestinations();
        this.refresher.Visibility = ViewStates.Gone;

        var destWithLocation = this.FilterDestinationByLocation(destinations);

        PicassoUtils.ClearCache(destWithLocation, this.Activity);

        this.AddDestinationsItems(destWithLocation);
        this.InitDestinationsCards(destWithLocation);
      }
      catch (Exception exception)
      {
        AppLog.Logger.Error(this.Resources.GetString(Jetstream.Resource.String.error_log_text_failed_to_load_destinations), exception.Message);

        this.refresher.Visibility = ViewStates.Gone;

        SnackbarManager.Show(
          Snackbar.With(this.Activity)
            .ActionLabel(this.Resources.GetString(Jetstream.Resource.String.error_text_retry))
            .ActionColor(this.Resources.GetColor(Jetstream.Resource.Color.color_accent))
            .ActionListener(this)
            .Text(this.Resources.GetString(Jetstream.Resource.String.error_text_fail_to_load_destinations)));
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

        Picasso.With(this.Activity).Load(destination.ImageUrl(this.Activity.GetSession())).Into(destImageView);

        cardView.Click += (sender, args) => this.StartDestinationActivity(dest);

        this.cardsContainer.AddView(cardView);
      }
      this.cardsContainer.ViewTreeObserver.GlobalLayout += this.CardsContainerViewTreeObserverGlobalLayout;
    }

    void CardsContainerViewTreeObserverGlobalLayout(object sender, EventArgs e)
    {
      DestinationsCardsAnimHelper.AnimateAppearance(this.cardsContainer);
      this.cardsContainer.ViewTreeObserver.GlobalLayout -= this.CardsContainerViewTreeObserverGlobalLayout;
    }

    private void AddDestinationsItems(List<IDestination> destinations)
    {
      var clusterItems = destinations.Select(destination => new ClusterItem(destination, destination.ImageUrl(this.Activity.GetSession())))
        .ToList();

      this.map.Clear();

      this.clusterManager.ClearItems();
      this.clusterManager.AddItems(clusterItems);
      this.clusterManager.Cluster();
    }

    private void StartDestinationActivity(IDestination dest)
    {

      var stringDest = string.Join(DestinationAndroidSpec.SplitSymbol.ToString(), dest.ImageUrl(this.Activity.GetSession()), dest.Overview, dest.DisplayName);

      var intent = new Intent(this.Activity, typeof(DestinationActivity));
      intent.PutExtra(DestinationActivity.DestinationParamIntentKey, stringDest);

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
      if(this.map == null)
      {
        this.mapView.GetMapAsync(this);
      }

      if(this.refreshEventHandler == null)
      {
        this.refreshEventHandler = new MessageBusEventHandler()
        {
          EventId = EventIdsContainer.RefreshMenuActionClickedEvent,
          EventAction = (sender, evnt) =>
              this.Activity.RunOnUiThread(delegate
            {
              if(this.refresher.Visibility == ViewStates.Visible || this.IsHidden)
              {
                return;
              }

              this.LoadDestinations();
            })
        };
      }

      if(this.updateInstanceUrlEventHandler == null)
      {
        this.updateInstanceUrlEventHandler = new MessageBusEventHandler()
        {
          EventId = EventIdsContainer.SitecoreInstanceUrlUpdateEvent,
          EventAction = (sender, evnt) =>
          this.Activity.RunOnUiThread(delegate
            {
              this.refreshOnHiddenChanged = this.IsHidden;

              if(this.refresher.Visibility == ViewStates.Visible || this.IsHidden)
              {
                return;
              }

              this.LoadDestinations();
            })
        };
      }

      MessageBus.Default.Register(this.updateInstanceUrlEventHandler);
      MessageBus.Default.Register(this.refreshEventHandler);
    }

    public override void OnDestroy()
    {
      base.OnDestroy();

      this.mapView.OnDestroy();
      MessageBus.Default.DeRegister(this.updateInstanceUrlEventHandler);
      MessageBus.Default.DeRegister(this.refreshEventHandler);
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