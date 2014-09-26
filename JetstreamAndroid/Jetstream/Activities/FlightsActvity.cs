namespace JetstreamAndroid.Activities
{
  using Android.App;
  using Android.Content.PM;
  using Android.OS;
  using Android.Support.V4.App;
  using Android.Support.V4.View;
  using JetstreamAndroid.Adapters;
  using JetstreamAndroid.Fragments;
  using JetstreamAndroid.Models;

  [Activity(Label = "FlightsActvity", ScreenOrientation = ScreenOrientation.Portrait)]
  public class FlightsActvity : FragmentActivity
  {
    private JetstreamPagerAdapter pagerAdapter;
    public ViewPager mPager;
    Android.Support.V4.App.Fragment[] _fragments;

    private FlightsContainer flightsContainer;

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      this.SetContentView(Resource.Layout.activity_flights);

      this.flightsContainer = JetstreamApp.From(this).FlightsContainer;

      this.InitFragmentPagerAdapter();

      this.mPager = this.FindViewById<ViewPager>(Resource.Id.pager);
      this.mPager.Adapter = this.pagerAdapter;
      this.mPager.SetCurrentItem(1, true);

      this.InitTabsIndicator();
    }

    private void InitFragmentPagerAdapter()
    {
      this.pagerAdapter = new JetstreamPagerAdapter(this.SupportFragmentManager);
      this.pagerAdapter.Fragments = new[]
      {
        new SettingsFragment(), new SettingsFragment(), new SettingsFragment()
      };
    }

    private void InitTabsIndicator()
    {
      var indicator = this.FindViewById<TabPageIndicator>(Resource.Id.indicator);
      indicator.SetViewPager(this.mPager);

      indicator.AddDayBeforeTab(this.flightsContainer.YesterdaySummary);
      indicator.AddTodayTab(this.flightsContainer.FlightSearchUserInput);
      indicator.AddDayAfterTab(this.flightsContainer.TomorrowSummary);

      indicator.SetCurrentItem(1);
    }
  }
}

