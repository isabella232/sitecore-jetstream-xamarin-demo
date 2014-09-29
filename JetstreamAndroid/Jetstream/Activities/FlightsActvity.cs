using Android.Support.V4.View;

namespace JetstreamAndroid.Activities
{
  using Android.App;
  using Android.Content.PM;
  using Android.OS;
  using Android.Views;
  using JetstreamAndroid.Adapters;
  using JetstreamAndroid.Fragments;
  using JetstreamAndroid.Models;
  using JetstreamAndroid.Tabs;


  [Activity(Label = "FlightsActvity", ScreenOrientation = ScreenOrientation.Portrait)]
  public class FlightsActvity : Activity
  {
    private JetstreamPagerAdapter pagerAdapter;
    private ViewPager mPager;
    private Android.App.Fragment[] _fragments;

    private FlightsContainer flightsContainer;

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      this.RequestWindowFeature(WindowFeatures.IndeterminateProgress);
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
      this.pagerAdapter = new JetstreamPagerAdapter(this.FragmentManager)
      {
        Fragments = new[]
        {
          FlightsListFragment.NewInstance(FragmentType.YesterdayFlights),
          FlightsListFragment.NewInstance(FragmentType.TodayFlights),
          FlightsListFragment.NewInstance(FragmentType.TommorowFlights)
        }
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

