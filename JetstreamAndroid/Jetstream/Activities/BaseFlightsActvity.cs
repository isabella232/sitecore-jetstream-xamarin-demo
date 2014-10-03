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
  public abstract class BaseFlightsActvity : Activity, FlightsListAdapter.IFlightOrderSelectedListener
  {
    #region Stuff associated with Views
    private JetstreamPagerAdapter pagerAdapter;
    private ViewPager viewPager;
    private TabPageIndicator tabsPageIndicator;
    #endregion

    #region Stuff with data
    protected IFlightSearchUserInput UserInput;
    #endregion

    protected JetstreamApp App;

    public abstract DateTime GetDateTime();

    public abstract void OnFlightOrderSelected(IJetStreamFlight flight);

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      ActionBar.SetDisplayHomeAsUpEnabled(true);
      this.SetContentView(Resource.Layout.activity_flights);

      this.App = JetstreamApp.From(this);
      this.UserInput = JetstreamApp.From(this).FlightUserInput;

      this.InitFragmentPagerAdapter();

      this.viewPager = this.FindViewById<ViewPager>(Resource.Id.pager);
      this.viewPager.Adapter = this.pagerAdapter;
      this.viewPager.SetCurrentItem(1, true);

      this.InitTabsIndicator();
    }

    private void InitFragmentPagerAdapter()
    {
      var fragments = new List<FlightsListFragment>
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

      this.tabsPageIndicator.AddTab(this.GetDateTime().AddDays(-1), 0);
      this.tabsPageIndicator.AddTab(this.GetDateTime(), 1);
      this.tabsPageIndicator.AddTab(this.GetDateTime().AddDays(1), 2);

      this.tabsPageIndicator.SetCurrentItem(1);
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

