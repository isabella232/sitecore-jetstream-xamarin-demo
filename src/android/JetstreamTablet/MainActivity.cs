namespace Jetstream
{
  using Android.App;
  using Android.Gms.Maps;
  using Android.OS;
  using Android.Support.V4.Widget;
  using Android.Util;
  using Android.Widget;
  using Com.Lilarcor.Cheeseknife;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.PasswordProvider.Android;

  [Activity(MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity
  {
    MapFragment mapFragment;

    [InjectView(Resource.Id.refresher)]
    SwipeRefreshLayout refresher;

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      this.SetContentView(Resource.Layout.Main);

      Cheeseknife.Inject(this);

      this.refresher.SetColorScheme(Android.Resource.Color.HoloBlueDark,
        Android.Resource.Color.HoloPurple,
        Android.Resource.Color.DarkerGray,
        Android.Resource.Color.HoloGreenDark);

      this.refresher.SetProgressViewOffset(false, 0, (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 24, this.Resources.DisplayMetrics));

      this.mapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map);
      GoogleMap map = this.mapFragment.Map;
      if (map != null) {
        map.MapType = GoogleMap.MapTypeTerrain;
      } else
      {
        Toast.MakeText(this, "Map is null", ToastLength.Long).Show();
      }
    }

        public ISitecoreWebApiSession Session
        {
          get
          {
            ISitecoreWebApiSession session = null;
            //        if (isAuthentiated)
            //        {
            using (var credentials = new SecureStringPasswordProvider("sitecore\\admin", "b"))
            {
              session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("jetstream800394rev150402.test24dk1.dk.sitecore.net")
                        .Credentials(credentials)
                          .DefaultDatabase("web")
                        .BuildSession();
            }
            //        }
            //        else
            //        {
            //        session = SitecoreWebApiSessionBuilder.AnonymousSessionWithHost("jetstream800394rev150402.test24dk1.dk.sitecore.net")
            //          .DefaultDatabase("web")
            //            .BuildSession();
            //        }
    
            return session;
          }
        }
  }
}


