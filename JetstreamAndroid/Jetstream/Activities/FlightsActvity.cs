namespace JetstreamAndroid.Activities
{
  using System;
  using System.Collections.Generic;
  using Android.App;
  using Android.Content.PM;
  using Android.OS;
  using Android.Support.V4.View;
  using Android.Views;
  using JetstreamAndroid.Adapters;
  using JetstreamAndroid.Fragments;
  using JetstreamAndroid.Tabs;
  using JetStreamCommons.FlightSearch;


  [Activity(Label = "FlightsActvity", ScreenOrientation = ScreenOrientation.Portrait)]
  public class FlightsActvity : Activity, ViewPager.IOnPageChangeListener
  {
    #region Stuff associated with Views
    private JetstreamPagerAdapter pagerAdapter;
    private ViewPager viewPager;
    private TabPageIndicator tabsPageIndicator;
    #endregion

    #region Stuff with data
    private IFlightSearchUserInput userInput;
    #endregion

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      this.RequestWindowFeature(WindowFeatures.IndeterminateProgress);
      this.SetContentView(Resource.Layout.activity_flights);
      ActionBar.SetDisplayHomeAsUpEnabled(true);

      this.userInput = JetstreamApp.From(this).FlightUserInput;

      this.InitFragmentPagerAdapter();

      this.viewPager = this.FindViewById<ViewPager>(Resource.Id.pager);
      this.viewPager.Adapter = this.pagerAdapter;
      this.viewPager.SetCurrentItem(1, true);

      this.InitTabsIndicator();
    }

    private void InitFragmentPagerAdapter()
    {
      var fragments = new List<Fragment>
      {
        FlightsListFragment.NewInstance(this.userInput.ForwardFlightDepartureDate.AddDays(-1)),
        FlightsListFragment.NewInstance(this.userInput.ForwardFlightDepartureDate),
        FlightsListFragment.NewInstance(this.userInput.ForwardFlightDepartureDate.AddDays(1))
      };

      this.pagerAdapter = new JetstreamPagerAdapter(this.FragmentManager)
      {
        Fragments = fragments
      };
    }

    private void InitTabsIndicator()
    {
      this.tabsPageIndicator = this.FindViewById<TabPageIndicator>(Resource.Id.indicator);
      this.tabsPageIndicator.SetViewPager(this.viewPager);
      this.tabsPageIndicator.SetOnPageChangeListener(this);

      DateTime flightDate = this.userInput.ForwardFlightDepartureDate;

      this.tabsPageIndicator.AddTab(flightDate.AddDays(-1), 0);
      this.tabsPageIndicator.AddTab(flightDate, 1);
      this.tabsPageIndicator.AddTab(flightDate.AddDays(1), 2);

      this.tabsPageIndicator.SetCurrentItem(1);
    }

    public void OnPageScrollStateChanged(int state)
    {
    }

    public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
    {
    }

    public void OnPageSelected(int position)
    {
      if (position == this.tabsPageIndicator.TabsCount - 1)
      {
        var date = this.userInput.ForwardFlightDepartureDate;
        FlightsListFragment newFragment = FlightsListFragment.NewInstance(date.AddDays(this.tabsPageIndicator.TabsCount - 1));
        this.pagerAdapter.Fragments.Add(newFragment);
        this.pagerAdapter.NotifyDataSetChanged();

        this.tabsPageIndicator.AddTab(date.AddDays(this.tabsPageIndicator.TabsCount - 1), this.tabsPageIndicator.TabsCount);
      }
    }

    public override bool OnOptionsItemSelected(IMenuItem item)
    {
      switch (item.ItemId)
      {
        case Android.Resource.Id.Home:
          this.Finish();
          return true;
      }
      return base.OnOptionsItemSelected(item);
    }
  }
}

