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
  public abstract class BaseFlightsActvity : Activity, FlightsListAdapter.IFlightOrderSelectedListener, FilterFragment.IFilterActionListener
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
      Toast.MakeText(this, "Filter Applied", ToastLength.Short).Show();

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
      Toast.MakeText(this, "Filter Cleared", ToastLength.Short).Show();
      this.FlightsFilterSettings = null;
      this.UpdateFragmentFilter();
    }

    public ExtendedFlightsFilterSettings GetFilter()
    {
      return this.FlightsFilterSettings;
    }

    public ExtendedFlightsFilterSettings GetDefaultFilter()
    {
      var fragment = this.pagerAdapter.Fragments[this.viewPager.CurrentItem];
      return fragment.CreateDefaultFilter();
    }
  }
}

