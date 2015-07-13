namespace Jetstream.UI.Activities
{
  using Android.App;
  using Android.Content.PM;
  using Android.Graphics;
  using Android.OS;
  using Android.Support.V7.App;
  using Android.Views;
  using Android.Widget;
  using Mikepenz.Google_material_typeface_library;
  using Mikepenz.Iconics;
  using Mikepenz.MaterialDrawer;
  using Mikepenz.MaterialDrawer.Accountswitcher;
  using Mikepenz.MaterialDrawer.Models;
  using Mikepenz.MaterialDrawer.Models.Interfaces;
  using DSoft.Messaging;
  using Jetstream.Font;
  using Jetstream.UI.Fragments;
  using Jetstream.Utils;
  using Sitecore.MobileSDK;

  [Activity(MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Landscape, Name = "net.sitecore.jetstream.MainActivity")]
  public class MainActivity : AppCompatActivity, Drawer.IOnDrawerItemClickListener
  {
    #region Drawer menu items identifiers

    private const int AboutMenuItemIdentifier = 1;
    private const int DestinationsMenuItemIdentifier = 2;
    private const int FlightStatusMenuItemIdentifier = 3;
    private const int CheckInMenuItemIdentifier = 4;
    private const int SettingsMenuItemIdentifier = 5;

    #endregion

    private bool showRefreshMenuItem = true;

    #region Views

    private Android.Support.V7.Widget.Toolbar toolbar;

    private AccountHeader header = null;
    private Drawer drawer = null;
    private Prefs prefs;

    #endregion

    #region Fragments

    private DestinationsOnMapFragment mapFragment;
    private AboutFragment aboutFragment;
    private CheckInFragment checkInFragment;
    private FlightStatusFragment flightStatusFragment;
    private Android.Support.V4.App.Fragment currentActiveFragment;

    #endregion

    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);
      this.SetContentView(Resource.Layout.activity_main);

      this.prefs = Prefs.From(this);

      this.toolbar = this.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

      this.InitDrawer(savedInstanceState);

      this.currentActiveFragment = this.mapFragment = new DestinationsOnMapFragment();

      if (savedInstanceState == null)
      {
        this.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.map_fragment_container, this.mapFragment).Commit();
      }
    }

    public override bool OnCreateOptionsMenu(IMenu menu)
    {
      this.MenuInflater.Inflate(Resource.Menu.menu_main, menu);

      var menuItem = menu.FindItem(Resource.Id.action_refresh);
      menuItem.SetIcon(new IconicsDrawable(this, GoogleMaterial.Icon.GmdRefresh).ActionBar().Color(Color.White));

      return base.OnCreateOptionsMenu(menu);
    }

    public override bool OnPrepareOptionsMenu(IMenu menu)
    {
      var actionRefresh = menu.FindItem(Resource.Id.action_refresh);
      actionRefresh.SetVisible(this.showRefreshMenuItem);

      return base.OnPrepareOptionsMenu(menu);
    }

    public override void OnBackPressed()
    {
      if (this.drawer != null && this.drawer.IsDrawerOpen)
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

    private void InitDrawer(Bundle savedInstanceState)
    {
      this.SetSupportActionBar(this.toolbar);
      this.toolbar.SetLogo(Resource.Drawable.ic_jetstream_logo);

      this.Title = "";
      this.toolbar.InflateMenu(Resource.Menu.menu_main);
      this.toolbar.MenuItemClick += (sender, e) =>
      {
        AnalyticsHelper.TrackRefreshButtonTouch();
        MessageBus.PostEvent(EventIdsContainer.RefreshMenuActionClickedEvent);
      };

      this.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
      this.SupportActionBar.SetHomeButtonEnabled(false);

      this.PrepareHeader(savedInstanceState);

      var about = new PrimaryDrawerItem();
      about.WithName(this.Resources.GetString(Resource.String.menu_text_about));
      about.WithIcon(new IconicsDrawable(this, JetstreamIcons.Icon.About).ColorRes(Resource.Color.color_primary));
      about.WithIdentifier(AboutMenuItemIdentifier);
      about.WithCheckable(false);

      var destinations = new PrimaryDrawerItem();
      destinations.WithName(Resource.String.menu_text_destinations);
      destinations.WithIcon(new IconicsDrawable(this, JetstreamIcons.Icon.Destinations).ColorRes(Resource.Color.color_primary));
      destinations.WithIdentifier(DestinationsMenuItemIdentifier);
      destinations.WithCheckable(false);

      var flightStatus = new PrimaryDrawerItem();
      flightStatus.WithName(this.Resources.GetString(Resource.String.menu_text_flight_status));
      flightStatus.WithIcon(new IconicsDrawable(this, JetstreamIcons.Icon.FlightStatus).ColorRes(Resource.Color.color_primary));
      flightStatus.WithIdentifier(FlightStatusMenuItemIdentifier);
      flightStatus.WithCheckable(false);

      var checkIn = new PrimaryDrawerItem();
      checkIn.WithName(this.Resources.GetString(Resource.String.menu_text_check_in));
      checkIn.WithIcon(new IconicsDrawable(this, JetstreamIcons.Icon.OnlineCheckin).ColorRes(Resource.Color.color_primary));
      checkIn.WithIdentifier(CheckInMenuItemIdentifier);
      checkIn.WithCheckable(false);

      var settings = new PrimaryDrawerItem();
      settings.WithName(Resource.String.menu_text_settings);
      settings.WithIcon(new IconicsDrawable(this, JetstreamIcons.Icon.Settings).ColorRes(Resource.Color.color_primary));
      settings.WithIdentifier(SettingsMenuItemIdentifier);
      settings.WithCheckable(false);

      var divider = new DividerDrawerItem();

      this.drawer = new DrawerBuilder()
        .WithActivity(this)
        .WithToolbar(this.toolbar)
        .WithAccountHeader(this.header)
        .AddDrawerItems(about, destinations, divider, flightStatus, checkIn, divider, settings)
        .WithOnDrawerItemClickListener(this)
        .WithSavedInstance(savedInstanceState)
        .WithActionBarDrawerToggleAnimated(true)
        .WithSelectedItem(1)
        .Build();
    }

    private void PrepareHeader(Bundle savedInstanceState)
    {
      var profile = new ProfileDrawerItem()
        .WithName(this.GetString(Resource.String.text_default_user))
        .WithIcon(new IconicsDrawable(this, JetstreamIcons.Icon.Profile).Color(Color.White).SizeDp(48));

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

    public bool OnItemClick(AdapterView parent, View view, int position, long id, IDrawerItem drawerItem)
    {
      if (drawerItem == null)
      {
        return true;
      }

      var previousSelectedItem = this.drawer.CurrentSelection;

      this.drawer.SetSelectionByIdentifier(drawerItem.Identifier, false);

      switch (drawerItem.Identifier)
      {
        case AboutMenuItemIdentifier:
          this.showRefreshMenuItem = true;
          this.InvalidateOptionsMenu();

          if (this.currentActiveFragment is AboutFragment)
          {
            return false;
          }

          if (this.aboutFragment == null)
          {
            this.aboutFragment = new AboutFragment();
            this.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.about_fragment_container, this.aboutFragment).Commit();
          }

          this.HideAndShowFragments(this.currentActiveFragment, this.aboutFragment);

          this.currentActiveFragment = this.aboutFragment;
          break;
        case DestinationsMenuItemIdentifier:
          this.showRefreshMenuItem = true;
          this.InvalidateOptionsMenu();

          if (this.currentActiveFragment is DestinationsOnMapFragment)
          {
            return false;
          }

          if (this.mapFragment == null)
          {
            this.mapFragment = new DestinationsOnMapFragment();
            this.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.map_fragment_container, this.mapFragment).Commit();
          }

          this.HideAndShowFragments(this.currentActiveFragment, this.mapFragment);

          this.currentActiveFragment = this.mapFragment;
          break;
        case FlightStatusMenuItemIdentifier:
          this.showRefreshMenuItem = false;
          this.InvalidateOptionsMenu();

          if (this.currentActiveFragment is FlightStatusFragment)
          {
            return false;
          }

          if (this.flightStatusFragment == null)
          {
            this.flightStatusFragment = new FlightStatusFragment();
            this.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.flight_status_fragment_container, this.flightStatusFragment).Commit();
          }

          this.HideAndShowFragments(this.currentActiveFragment, this.flightStatusFragment);

          this.currentActiveFragment = this.flightStatusFragment;
          break;
        case CheckInMenuItemIdentifier:
          this.showRefreshMenuItem = false;
          this.InvalidateOptionsMenu();

          if (this.currentActiveFragment is CheckInFragment)
          {
            return false;
          }

          if (this.checkInFragment == null)
          {
            this.checkInFragment = new CheckInFragment();
            this.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.check_in_fragment_container, this.checkInFragment).Commit();
          }

          this.HideAndShowFragments(this.currentActiveFragment, this.checkInFragment);

          this.currentActiveFragment = this.checkInFragment;
          break;
        case SettingsMenuItemIdentifier:
          this.StartActivity(typeof(SettingsActivity));

          new Handler().PostDelayed(() => this.drawer.SetSelection(previousSelectedItem, false), 500);
          break;
      }

      return false;
    }

    private void HideAndShowFragments(Android.Support.V4.App.Fragment toHide, Android.Support.V4.App.Fragment toShow)
    {
      this.SupportFragmentManager.BeginTransaction().Hide(toHide).Commit();
      this.SupportFragmentManager.BeginTransaction().Show(toShow).Commit();
    }

    public ScApiSession GetSession()
    {
      return (this.Application as JetstreamApplication).Session;
    }
  }
}