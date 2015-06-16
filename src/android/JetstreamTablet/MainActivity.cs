namespace Jetstream
{
  using Android.App;
  using Android.Content.PM;
  using Android.Graphics;
  using Android.OS;
  using Android.Support.V7.App;
  using Android.Views;
  using Android.Widget;
  using Com.Mikepenz.Google_material_typeface_library;
  using Com.Mikepenz.Iconics;
  using Com.Mikepenz.Materialdrawer;
  using Com.Mikepenz.Materialdrawer.Accountswitcher;
  using Com.Mikepenz.Materialdrawer.Model;
  using Com.Mikepenz.Materialdrawer.Model.Interfaces;
  using DSoft.Messaging;
  using Jetstream.UI.Dialogs;
  using Jetstream.UI.Fragments;
  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.PasswordProvider.Android;

  [Activity(MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Landscape, Name = "net.sitecore.jetstream.MainActivity")]
  public class MainActivity : AppCompatActivity, Drawer.IOnDrawerItemClickListener
  {
    private const int AboutMenuItemIdentifier = 1;
    private const int DestinationsMenuItemIdentifier = 2;
    private const int FlightStatusMenuItemIdentifier = 3;
    private const int CheckInMenuItemIdentifier = 4;
    private const int SettingsMenuItemIdentifier = 5;

    private Android.Support.V7.Widget.Toolbar toolbar;

    private AccountHeader header = null;
    private Drawer drawer = null;
    private Prefs prefs;

    private DestinationsOnMapFragment mapFragment;

    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);
      this.SetContentView(Resource.Layout.activity_main);

      this.prefs = Prefs.From(this);

      this.toolbar = this.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

      this.InitDrawer(savedInstanceState);

      if(savedInstanceState == null)
      {
        this.FragmentManager.BeginTransaction().Replace(Resource.Id.map_fragment_container, this.mapFragment).Commit();  
      }
    }

    private void InitDrawer(Bundle savedInstanceState)
    {
      this.SetSupportActionBar(this.toolbar);
      this.toolbar.SetLogo(Resource.Drawable.ic_jetstream_logo);

      this.Title = "";
      this.toolbar.InflateMenu(Resource.Menu.menu_main);
      this.toolbar.MenuItemClick += (sender, e) => MessageBus.PostEvent(EventIdsContainer.SitecoreInstanceUrlUpdateEvent);

      this.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
      this.SupportActionBar.SetHomeButtonEnabled(false);

      this.PrepareHeader(savedInstanceState);

      var about = new PrimaryDrawerItem();
      about.WithName("About Jetstream");
      about.WithIcon(Resource.Drawable.ic_about);
      about.WithIdentifier(AboutMenuItemIdentifier);
      about.WithCheckable(false);

      var destinations = new PrimaryDrawerItem();
      destinations.WithName(Resource.String.text_destinations_item);
      destinations.WithIcon(Resource.Drawable.ic_destinations);
      destinations.WithIdentifier(DestinationsMenuItemIdentifier);
      destinations.WithCheckable(false);

      var flightStatus = new PrimaryDrawerItem();
      flightStatus.WithName("Flight Status");
      flightStatus.WithIcon(Resource.Drawable.ic_flight_status);
      flightStatus.WithIdentifier(FlightStatusMenuItemIdentifier);
      flightStatus.WithCheckable(false);

      var checkIn = new PrimaryDrawerItem();
      checkIn.WithName("Check-in");
      checkIn.WithIcon(Resource.Drawable.ic_online_checkin);
      checkIn.WithIdentifier(CheckInMenuItemIdentifier);
      checkIn.WithCheckable(false);

      var settings = new PrimaryDrawerItem();
      settings.WithName(Resource.String.text_settings_item);
      settings.WithIcon(Resource.Drawable.ic_settings);
      settings.WithIdentifier(SettingsMenuItemIdentifier);
      settings.WithCheckable(false);

      var divider = new DividerDrawerItem();

      this.drawer = new DrawerBuilder()
                .WithActivity(this)
                .WithRootView(Resource.Id.drawer_container)
                .WithToolbar(this.toolbar)
                .WithAccountHeader(this.header)
                .AddDrawerItems(about, destinations, divider, flightStatus, checkIn, divider, settings)
                .WithOnDrawerItemClickListener(this)
                .WithSavedInstance(savedInstanceState)
                .WithActionBarDrawerToggleAnimated(true)
                .WithSelectedItem(1)
                .Build();

      this.mapFragment = new DestinationsOnMapFragment();
    }

    public override bool OnCreateOptionsMenu(IMenu menu)
    {
      this.MenuInflater.Inflate(Resource.Menu.menu_main, menu);

      var menuItem = menu.FindItem(Resource.Id.action_refresh);
      menuItem.SetIcon(new IconicsDrawable(this, GoogleMaterial.Icon.GmdRefresh).ActionBar().Color(Color.White));

      return base.OnCreateOptionsMenu(menu);
    }

    private void PrepareHeader(Bundle savedInstanceState)
    {
      var profileDrawable = this.Resources.GetDrawable(Resource.Drawable.ic_profile);

      var profile = new ProfileDrawerItem()
        .WithName(this.GetString(Resource.String.text_default_user))
        .WithIcon(profileDrawable);

      this.header = new AccountHeaderBuilder()
        .WithActivity(this)
        .WithCompactStyle(true)
        .WithSelectionListEnabled(false)
        .WithHeaderBackground(Resource.Drawable.header)
        .AddProfiles(profile)
        .WithProfileImagesClickable(false)
        .WithSavedInstance(savedInstanceState)
        .Build();

      this.header.View.Clickable = false;

      var email = this.header.View.FindViewById<TextView>(Resource.Id.account_header_drawer_email);
      email.Visibility = ViewStates.Gone;
    }

    public override void OnBackPressed()
    {
      if(this.drawer != null && this.drawer.IsDrawerOpen)
      {
        this.drawer.CloseDrawer();
      }
      else
      {
        base.OnBackPressed();
      }
    }

    protected override void OnSaveInstanceState(Bundle outState)
    {
      outState = this.drawer.SaveInstanceState(outState);
      outState = this.header.SaveInstanceState(outState);
      base.OnSaveInstanceState(outState);
    }

    public bool OnItemClick(AdapterView parent, Android.Views.View view, int position, long id, IDrawerItem drawerItem)
    {
      if(drawerItem == null)
      {
        return true;
      }

      this.drawer.SetSelectionByIdentifier(drawerItem.Identifier, false);

      switch (drawerItem.Identifier)
      {
        case AboutMenuItemIdentifier:
          break;
        case DestinationsMenuItemIdentifier:
          break;
        case FlightStatusMenuItemIdentifier:
          break;
        case CheckInMenuItemIdentifier:
          break;
        case SettingsMenuItemIdentifier:
          var settings = new SettingsDialog();
          settings.Show(this.FragmentManager, "settings");
          
          new Handler().PostDelayed(() => this.drawer.SetSelectionByIdentifier(1, false), 500);
          break;
      }

      return false;
    }

    //TODO:
    public ScApiSession Session
    {
      get
      {
        ISitecoreWebApiSession session = null;
        //        if (isAuthentiated)
        //        {
        using (var credentials = new SecureStringPasswordProvider("sitecore\\admin", "b"))
        {
          session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(this.prefs.InstanceUrl)
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