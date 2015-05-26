namespace Jetstream
{
  using Android.App;
  using Android.Graphics;
  using Android.OS;
  using Android.Support.V7.App;
  using Android.Views;
  using Android.Widget;
  using Com.Lilarcor.Cheeseknife;
  using Com.Mikepenz.Google_material_typeface_library;
  using Com.Mikepenz.Iconics;
  using Com.Mikepenz.Materialdrawer;
  using Com.Mikepenz.Materialdrawer.Accountswitcher;
  using Com.Mikepenz.Materialdrawer.Model;
  using Com.Mikepenz.Materialdrawer.Model.Interfaces;
  using Jetstream.UI.Fragments;
  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.PasswordProvider.Android;

  [Activity(MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : AppCompatActivity, Drawer.IOnDrawerItemClickListener
  {
    [InjectView(Resource.Id.toolbar)]
    private Android.Support.V7.Widget.Toolbar toolbar;

    private AccountHeader headerResult = null;
    private Drawer result = null;

    private DestinationsOnMapFragment mapFragment;
    private SettingsFragment settingsFragment;

    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);
      this.SetContentView(Resource.Layout.Main);

      Cheeseknife.Inject(this);

      this.InitDrawer(savedInstanceState);
    }

    private void InitDrawer(Bundle savedInstanceState)
    {
      this.SetSupportActionBar(this.toolbar);

      this.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
      this.SupportActionBar.SetHomeButtonEnabled(false);

      var profile = new ProfileDrawerItem()
        .WithName(this.GetString(Resource.String.text_default_user))
        .WithIcon(
          new IconicsDrawable(this, GoogleMaterial.Icon.GmdVerifiedUser)
            .ActionBarSize()
            .PaddingDp(5)
            .Color(Color.Black));

      this.headerResult = new AccountHeaderBuilder()
        .WithActivity(this)
        .WithCompactStyle(true)
        .WithSelectionListEnabled(false)
        .WithHeaderBackground(Resource.Drawable.header)
        .AddProfiles(profile)
        .WithSavedInstance(savedInstanceState)
        .Build();

      this.headerResult.View.Clickable = false;

      var email = this.headerResult.View.FindViewById<TextView>(Resource.Id.account_header_drawer_email);
      email.Visibility = ViewStates.Gone;

      var destinations = new PrimaryDrawerItem();
      destinations.WithName(Resource.String.text_destinations_item);
      destinations.WithIcon(GoogleMaterial.Icon.GmdFlight);
      destinations.WithIdentifier(1);
      destinations.WithCheckable(false);

      var settings = new PrimaryDrawerItem();
      settings.WithName(Resource.String.text_settings_item);
      settings.WithIcon(GoogleMaterial.Icon.GmdSettings);
      settings.WithIdentifier(2);
      settings.WithCheckable(false);

      this.result = new DrawerBuilder()
                .WithActivity(this)
                .WithRootView(Resource.Id.drawer_container)
                .WithToolbar(this.toolbar)
                .WithAccountHeader(this.headerResult)
                .AddDrawerItems(destinations, settings)
                .WithOnDrawerItemClickListener(this)
                .WithSavedInstance(savedInstanceState)
                .WithActionBarDrawerToggleAnimated(true)
                .Build();

      this.mapFragment = new DestinationsOnMapFragment();
      this.FragmentManager.BeginTransaction().Replace(Resource.Id.map_fragment_container, mapFragment).Commit();
    }

    public override void OnBackPressed()
    {
      //handle the back press :D close the drawer first and if the drawer is closed close the activity
      if (this.result != null && this.result.IsDrawerOpen)
      {
        this.result.CloseDrawer();
      }
      else
      {
        base.OnBackPressed();
      }
    }

    protected override void OnSaveInstanceState(Bundle outState)
    {
      //add the values which need to be saved from the drawer to the bundle
      outState = this.result.SaveInstanceState(outState);
      outState = this.headerResult.SaveInstanceState(outState);
      base.OnSaveInstanceState(outState);
    }

    public bool OnItemClick(AdapterView parent, Android.Views.View view, int position, long id, IDrawerItem drawerItem)
    {
      if (drawerItem == null)
      {
        return true;
      }

      this.result.SetSelectionByIdentifier(drawerItem.Identifier, false);

      switch (drawerItem.Identifier)
      {
        case 1:
          if (this.mapFragment == null)
          {
            this.mapFragment = new DestinationsOnMapFragment();
            this.FragmentManager.BeginTransaction().Replace(Resource.Id.map_fragment_container, this.mapFragment).Commit();
          }

          this.FragmentManager.BeginTransaction().Show(this.mapFragment).Commit();
          this.FragmentManager.BeginTransaction().Hide(this.settingsFragment).Commit();

          break;

        case 2:
          if (this.settingsFragment == null)
          {
            this.settingsFragment = new SettingsFragment();
            this.FragmentManager.BeginTransaction().Replace(Resource.Id.settings_fragment_container, this.settingsFragment).Commit();
          }

          this.FragmentManager.BeginTransaction().Hide(this.mapFragment).Commit();
          this.FragmentManager.BeginTransaction().Show(this.settingsFragment).Commit();
          break;
      }

      return false;
    }

    public ScApiSession Session
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

        return (ScApiSession)session;
      }
    }
  }
}