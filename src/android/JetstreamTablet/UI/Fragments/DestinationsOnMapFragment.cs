namespace Jetstream.UI.Fragments
{
  using System.Collections.Generic;
  using Android.App;
  using Android.Gms.Maps;
  using Android.Gms.Maps.Model;
  using Android.OS;
  using Android.Support.V4.Widget;
  using Android.Util;
  using Android.Views;
  using Com.Lilarcor.Cheeseknife;
  using Jetstream.Bitmap;
  using JetStreamCommons;
  using JetStreamCommons.Destinations;
  using Squareup.Picasso;

  public class DestinationsOnMapFragment : Fragment, IOnMapReadyCallback
  {
    const double Tolerance = 0.01;

    GoogleMap map;

    [InjectView(Resource.Id.refresher)]
    SwipeRefreshLayout refresher;

    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
      var view = inflater.Inflate(Resource.Layout.fragment_map_destinations, container, false);

      Cheeseknife.Inject(this, view);

      this.refresher.SetColorScheme(Android.Resource.Color.HoloBlueDark,
       Android.Resource.Color.HoloPurple,
       Android.Resource.Color.DarkerGray,
       Android.Resource.Color.HoloGreenDark);

      this.refresher.SetProgressViewOffset(false, 0, (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 24, this.Resources.DisplayMetrics));
      this.refresher.Refreshing = true;

      return view;
    }

    public override void OnResume()
    {
      base.OnResume();

      var fragment = this.ChildFragmentManager.FindFragmentById<MapFragment>(Resource.Id.map);
      if (fragment != null)
      {
        fragment.GetMapAsync(this);
      }
    }

    public void OnMapReady(GoogleMap googleMap)
    {
      this.map = googleMap;
      this.LoadDestinations();
    }

    async void LoadDestinations()
    {
      var loader = new DestinationsLoader(this.Activity.Session);
      var destinations = await loader.LoadOnlyDestinations();
      this.refresher.Refreshing = false;

      this.ShowDestinationsOnMap(destinations);
    }

    private new MainActivity Activity
    {
      get
      {
        return base.Activity as MainActivity;
      }
    }

    void ShowDestinationsOnMap(List<IDestination> destinations)
    {
      try
      {
        foreach (var dest in destinations)
        {
          var longitude = dest.Longitude;
          var latitude = dest.Latitude;

          var longZero = System.Math.Abs(longitude) < Tolerance;
          var latZero = System.Math.Abs(latitude) < Tolerance;

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

          Picasso.With(this.Activity).LoggingEnabled = true;

          Picasso.With(this.Activity).Load(url).Resize(100, 100).Into(new MarkerTarget(dest, marker));
        }
      }
      catch (System.Exception exception)
      {
        //TODO: Add logger message here
      }
    }

  }
}