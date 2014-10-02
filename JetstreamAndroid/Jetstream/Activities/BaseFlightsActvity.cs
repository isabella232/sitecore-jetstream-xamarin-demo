namespace JetstreamAndroid.Activities
{
  using System;
  using System.Collections.Generic;
  using Android.App;
  using Android.OS;
  using Android.Support.V4.View;
  using Android.Views;
  using JetstreamAndroid.Adapters;
  using JetstreamAndroid.Fragments;
  using JetstreamAndroid.Tabs;
  using JetStreamCommons.Flight;
  using JetStreamCommons.FlightSearch;


  [Activity]
  public abstract class BaseFlightsActvity : Activity, ViewPager.IOnPageChangeListener, FlightsListAdapter.IFlightOrderSelectedListener
  {
    #region Stuff associated with Views
    private JetstreamPagerAdapter pagerAdapter;
    private ViewPager viewPager;
    private TabPageIndicator tabsPageIndicator;
    #endregion

    #region Stuff with data
    protected IFlightSearchUserInput userInput;
    #endregion

    protected JetstreamApp app;

    public abstract DateTime GetDateTime();

    public abstract void OnFlightOrderSelected(IJetStreamFlight flight);

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      
      ActionBar.SetDisplayHomeAsUpEnabled(true);
      this.SetContentView(Resource.Layout.activity_flights);

      this.app = JetstreamApp.From(this);
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
        FlightsListFragment.NewInstance(this.GetDateTime().AddDays(-1)),
        FlightsListFragment.NewInstance(this.GetDateTime()),
        FlightsListFragment.NewInstance(this.GetDateTime().AddDays(1))
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



      this.tabsPageIndicator.AddTab(this.GetDateTime().AddDays(-1), 0);
      this.tabsPageIndicator.AddTab(this.GetDateTime(), 1);
      this.tabsPageIndicator.AddTab(this.GetDateTime().AddDays(1), 2);

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

        FlightsListFragment newFragment = FlightsListFragment.NewInstance(this.GetDateTime().AddDays(this.tabsPageIndicator.TabsCount - 1));
        this.pagerAdapter.Fragments.Add(newFragment);
        this.pagerAdapter.NotifyDataSetChanged();

        this.tabsPageIndicator.AddTab(this.GetDateTime().AddDays(this.tabsPageIndicator.TabsCount - 1), this.tabsPageIndicator.TabsCount);
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

