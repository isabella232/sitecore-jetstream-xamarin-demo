namespace JetstreamAndroid.Activities
{
  using Android.App;
  using Android.Content.PM;
  using Android.OS;
  using Android.Support.V4.App;
  using Android.Support.V4.View;
  using JetstreamAndroid.Adapters;

  [Activity(Label = "FlightsActvity", ScreenOrientation = ScreenOrientation.Portrait)]			
  public class FlightsActvity : FragmentActivity
  {
    public Adapters.FragmentPagerAdapter MPagerAdapter;
    public ViewPager mPager;

    static readonly string Tag = "ActionBarTabsSupport";

    Android.Support.V4.App.Fragment[] _fragments;

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      this.SetContentView(Resource.Layout.activity_flights);

      this.MPagerAdapter = new Adapters.FragmentPagerAdapter (this.SupportFragmentManager);

      this.mPager = this.FindViewById<ViewPager> (Resource.Id.pager);
      this.mPager.Adapter = this.MPagerAdapter;

      var mIndicator = this.FindViewById<TabPageIndicator> (Resource.Id.indicator);
      mIndicator.SetViewPager (this.mPager);
      mIndicator.AddTab("1", 0);
      mIndicator.AddTab("2", 1);
      mIndicator.AddTab("3", 2);
    }
  }
}

