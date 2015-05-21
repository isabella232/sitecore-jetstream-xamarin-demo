namespace JetstreamAndroid.Activities
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Android.App;
  using Android.OS;
  using Android.Support.V4.View;
  using Android.Views;
  using Android.Widget;
  using JetstreamAndroid.Adapters;
  using JetstreamAndroid.Fragments;
  using JetstreamAndroid.Tabs;
  using JetstreamAndroid.Utils;
  using JetStreamCommons.Flight;
  using JetStreamCommons.FlightSearch;

  [Activity]
  public abstract class BaseFlightsActvity : Activity, FlightsListAdapter.IFlightOrderSelectedListener,
    FilterFragment.IFilterActionListener, ViewPager.IOnPageChangeListener
  {
    private const int NumberOfDatesFragments = 10;

    #region Stuff associated with Views
    private JetstreamPagerAdapter pagerAdapter;
    private ViewPager viewPager;
    private TabPageIndicator tabsPageIndicator;
    #endregion

    #region Stuff with data
    protected IFlightSearchUserInput UserInput;
    protected ExtendedFlightsFilterSettings FlightsFilterSettings;
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

      var dates = this.InitDatesList();

      this.InitFragmentPagerAdapter(dates);

      this.viewPager = this.FindViewById<ViewPager>(Resource.Id.pager);
      this.viewPager.Adapter = this.pagerAdapter;
      this.viewPager.SetCurrentItem(dates.Count / 2, true);

      this.InitTabsIndicator(dates);
    }

    private List<DateTime> InitDatesList()
    {
      var date = this.GetDateTime();
      var dateList = new List<DateTime>();

      const int NumberOfSideFragments = NumberOfDatesFragments / 2;

      for (int i = (-NumberOfSideFragments); i <= NumberOfSideFragments; i++)
      {
        dateList.Add(date.AddDays(i));
      }

      return dateList;
    }

    private void InitFragmentPagerAdapter(IEnumerable<DateTime> dates)
    {
      var fragments = dates.Select(FlightsListFragment.NewInstance).ToList();

      this.pagerAdapter = new JetstreamPagerAdapter(this.FragmentManager)
      {
        Fragments = fragments
      };
    }

    private void InitTabsIndicator(IList<DateTime> dates)
    {
      this.tabsPageIndicator = this.FindViewById<TabPageIndicator>(Resource.Id.indicator);
      this.tabsPageIndicator.SetViewPager(this.viewPager);
      this.tabsPageIndicator.SetOnPageChangeListener(this);

      foreach (var date in dates)
      {
        this.tabsPageIndicator.AddTab(date, dates.IndexOf(date));
      }

      this.tabsPageIndicator.SetCurrentItem(dates.Count / 2);
    }

    public override bool OnOptionsItemSelected(IMenuItem item)
    {
      switch (item.ItemId)
      {
        case Android.Resource.Id.Home:
          this.Finish();
          return true;
        case Resource.Id.filter:
          FilterFragment.NewInstance().Show(FragmentManager, "dialog");
          return true;
      }
      return base.OnOptionsItemSelected(item);
    }

    public override bool OnCreateOptionsMenu(IMenu menu)
    {
      MenuInflater.Inflate(Resource.Menu.flights, menu);
      return base.OnPrepareOptionsMenu(menu);
    }

    public void ApplyFilter(ExtendedFlightsFilterSettings filter)
    {
      Toast.MakeText(this, GetString(Resource.String.text_filter_applied), ToastLength.Short).Show();

      this.FlightsFilterSettings = filter;
      this.UpdateFragmentFilter();
    }

    private void UpdateFragmentFilter()
    {
      var fragment = pagerAdapter.Fragments[viewPager.CurrentItem];
      fragment.PerformFiltering();
    }

    public void ClearFilter()
    {
      Toast.MakeText(this, GetString(Resource.String.text_filter_cleared), ToastLength.Short).Show();
      this.FlightsFilterSettings = null;
      this.UpdateFragmentFilter();
    }

    public ExtendedFlightsFilterSettings GetFilterInput()
    {
      return this.MergeFilters(this.FlightsFilterSettings, this.GetDefaultFilter());
    }

    private ExtendedFlightsFilterSettings MergeFilters(ExtendedFlightsFilterSettings one, ExtendedFlightsFilterSettings two)
    {
      if (one == null)
      {
        return null;
      }

      return new ExtendedFlightsFilterSettings(one)
      {
        MaxAvaliblePrice = two.MaxAvaliblePrice
      };
    }

    public ExtendedFlightsFilterSettings GetDefaultFilter()
    {
      var fragment = this.pagerAdapter.Fragments[this.viewPager.CurrentItem];
      var fragmentFilter = fragment.CreateDefaultFilter();

      var midnight = this.GetDateTime() + new TimeSpan(0, 0, 0);

      var resultFilter = new ExtendedFlightsFilterSettings(fragmentFilter)
      {
        IsRedEyeFlight = false,
        IsInFlightWifiIncluded = false,
        IsPersonalEntertainmentIncluded = false,
        IsFoodServiceIncluded = false,
        EarliestDepartureTime = midnight,
        LatestDepartureTime = midnight.AddDays(1).AddSeconds(-1)
      };

      return resultFilter;
    }

    public void OnPageScrollStateChanged(int state)
    {
    }

    public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
    {
    }

    public void OnPageSelected(int position)
    {
      var fragment = pagerAdapter.Fragments[position];
      fragment.PerformFiltering();
    }
  }
}

