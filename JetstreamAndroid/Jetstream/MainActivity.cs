namespace JetstreamAndroid
{
  using Android.App;
  using Android.Content.PM;
  using Android.Content.Res;
  using Android.OS;
  using Android.Support.V4.App;
  using Android.Support.V4.Widget;
  using Android.Views;
  using Android.Widget;
  using JetstreamAndroid.Fragments;
  using JetstreamAndroid.Utils;

  [Activity(Label = "Jetstream", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, Icon = "@drawable/icon")]
  public class MainActivity : FragmentActivity
  {
    private DrawerLayout drawer;
    private JetstreamActionBarDrawerToggle drawerToggle;
    private ListView drawerList;

    private BookFlightFragment bookFlightFragment;
    private SettingsFragment settingsFragment;

    private string drawerTitle;
    private string title;
    private string[] titles;

    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);

      RequestWindowFeature(WindowFeatures.IndeterminateProgress);

      this.SetContentView(Resource.Layout.activity_main);

      this.drawer = this.FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
      this.drawerList = this.FindViewById<ListView>(Resource.Id.left_drawer);

      this.drawer.SetDrawerShadow(Resource.Drawable.drawer_shadow_dark, (int)GravityFlags.Start);

      this.title = this.drawerTitle = this.Title;
      this.titles = Resources.GetStringArray(Resource.Array.fragment_names_array);

      this.drawerList.Adapter = new ArrayAdapter<string>(this,
          Android.Resource.Layout.SimpleListItem1, titles);
      this.drawerList.ItemClick += (sender, args) => this.SelectItem(args.Position);


      this.ActionBar.SetDisplayHomeAsUpEnabled(true);
      this.ActionBar.SetHomeButtonEnabled(true);

      //DrawerToggle is the animation that happens with the indicator next to the

      //ActionBar icon. You can choose not to use this.
      this.drawerToggle = new JetstreamActionBarDrawerToggle(this, this.drawer,
                                                Resource.Drawable.ic_drawer_light,
                                                Resource.String.app_name,
                                                Resource.String.app_name);

      //You can alternatively use drawer.DrawerClosed here
      this.drawerToggle.DrawerClosed += delegate
      {
        this.ActionBar.Title = this.title;
        this.InvalidateOptionsMenu();
      };

      //You can alternatively use drawer.DrawerOpened here
      this.drawerToggle.DrawerOpened += delegate
      {
        this.ActionBar.Title = this.drawerTitle;
        this.InvalidateOptionsMenu();
      };

      this.drawer.SetDrawerListener(this.drawerToggle);

      if (null == savedInstanceState)
        this.SelectItem(0);
    }

    private void SelectItem(int position)
    {
      Android.Support.V4.App.Fragment fragment;
      switch (position)
      {
        case 0:
          if (this.bookFlightFragment == null)
          {
             this.bookFlightFragment = new BookFlightFragment();  
          }
          fragment = this.bookFlightFragment;

          break;
        case 1:
          if (this.settingsFragment == null)
          {
             this.settingsFragment = new SettingsFragment();  
          }
          fragment = this.settingsFragment;

          break;
        default:
          fragment = new BookFlightFragment();
          break;
      }

      this.SupportFragmentManager.BeginTransaction()
          .Replace(Resource.Id.content_frame, fragment)
          .Commit();

      this.drawerList.SetItemChecked(position, true);

      this.ActionBar.Title = this.title = this.titles[position];
      this.drawer.CloseDrawer(this.drawerList);
    }

    protected override void OnPostCreate(Bundle savedInstanceState)
    {
      base.OnPostCreate(savedInstanceState);
      this.drawerToggle.SyncState();
    }

    public override void OnConfigurationChanged(Configuration newConfig)
    {
      base.OnConfigurationChanged(newConfig);
      this.drawerToggle.OnConfigurationChanged(newConfig);
    }

    public override bool OnOptionsItemSelected(IMenuItem item)
    {
      if (this.drawerToggle.OnOptionsItemSelected(item))
        return true;

      return base.OnOptionsItemSelected(item);
    }
  }
}

