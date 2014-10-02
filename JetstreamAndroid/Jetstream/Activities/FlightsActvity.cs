namespace JetstreamAndroid.Activities
{
  using System;
  using System.Collections.Generic;
  using Android.App;
  using Android.Content;
  using Android.Content.PM;
  using Android.OS;
  using Android.Support.V4.View;
  using Android.Views;
  using JetstreamAndroid.Adapters;
  using JetstreamAndroid.Fragments;
  using JetstreamAndroid.Tabs;
  using JetStreamCommons.Flight;
  using JetStreamCommons.FlightSearch;


  [Activity(Label = "FlightsActvity", ScreenOrientation = ScreenOrientation.Portrait)]
  public class FlightsActvity : Activity, ViewPager.IOnPageChangeListener, FlightsListAdapter.IFlightOrderSelectedListener
  {
    #region Stuff associated with Views
    private JetstreamPagerAdapter pagerAdapter;
    private ViewPager viewPager;
    private TabPageIndicator tabsPageIndicator;
    #endregion

    #region Stuff with data
    private IFlightSearchUserInput userInput;
    private DateTime dateTime;
    #endregion

    public const string ActivityReturnMode = "mode";
    public bool IsReturnMode { get; private set; }
    private JetstreamApp app;

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      this.RequestWindowFeature(WindowFeatures.IndeterminateProgress);
      this.SetContentView(Resource.Layout.activity_flights);
      ActionBar.SetDisplayHomeAsUpEnabled(true);

      this.app = JetstreamApp.From(this);
      this.IsReturnMode = Intent.Extras.GetBoolean(ActivityReturnMode);
      this.userInput = JetstreamApp.From(this).FlightUserInput;

      if (this.IsReturnMode && userInput.IsRoundTrip)
      {
        this.dateTime = (DateTime)this.userInput.ReturnFlightDepartureDate;
      }
      else
      {
        this.dateTime = this.userInput.ForwardFlightDepartureDate;
      }

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
        FlightsListFragment.NewInstance(this.dateTime.AddDays(-1)),
        FlightsListFragment.NewInstance(this.dateTime),
        FlightsListFragment.NewInstance(this.dateTime.AddDays(1))
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



      this.tabsPageIndicator.AddTab(this.dateTime.AddDays(-1), 0);
      this.tabsPageIndicator.AddTab(this.dateTime, 1);
      this.tabsPageIndicator.AddTab(this.dateTime.AddDays(1), 2);

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

        FlightsListFragment newFragment = FlightsListFragment.NewInstance(this.dateTime.AddDays(this.tabsPageIndicator.TabsCount - 1));
        this.pagerAdapter.Fragments.Add(newFragment);
        this.pagerAdapter.NotifyDataSetChanged();

        this.tabsPageIndicator.AddTab(this.dateTime.AddDays(this.tabsPageIndicator.TabsCount - 1), this.tabsPageIndicator.TabsCount);
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

    public override void OnBackPressed()
    {
      if (IsReturnMode)
      {
        this.app.ReturnFlight = null;
      }
      else
      {
        this.app.DepartureFlight = null;
      }
    }

    public void OnFlightOrderSelected(IJetStreamFlight flight)
    {
      if (this.userInput.IsRoundTrip)
      {
        if (this.IsReturnMode)
        {
          this.app.ReturnFlight = flight;
          this.StartActivity(typeof(SummaryActivity));
        }
        else
        {
          this.app.DepartureFlight = flight;
          var intent = new Intent(this, typeof(FlightsActvity));
          intent.PutExtra(ActivityReturnMode, true);

          StartActivity(intent);
        }
      }
      else
      {
        this.app.DepartureFlight = flight;
        this.StartActivity(typeof(SummaryActivity));
      }
    }
  }
}

