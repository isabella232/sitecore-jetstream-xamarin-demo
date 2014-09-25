
namespace JetstreamAndroid.Adapters
{
  using FragmentManager = Android.Support.V4.App.FragmentManager;
  using JetstreamAndroid.Fragments;
  using Fragment = Android.Support.V4.App.Fragment;

  public class FragmentPagerAdapter : Android.Support.V4.App.FragmentPagerAdapter
  {
    public FragmentPagerAdapter(FragmentManager fm)
      : base(fm)
    {
    }

    public override Fragment GetItem(int position)
    {
			return new SettingsFragment();
    }

    public override int Count
    {
      get
      {
        return 3;	
      }	
    }
  }
}

