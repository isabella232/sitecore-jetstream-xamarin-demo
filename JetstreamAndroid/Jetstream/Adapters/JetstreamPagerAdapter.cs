namespace JetstreamAndroid.Adapters
{
  using FragmentManager = Android.Support.V4.App.FragmentManager;
  using Fragment = Android.Support.V4.App.Fragment;

  public class JetstreamPagerAdapter : Android.Support.V4.App.FragmentPagerAdapter
  {
    public Fragment[] Fragments { get; set; }

    public JetstreamPagerAdapter(FragmentManager fm)
      : base(fm)
    {
    }

    public override Fragment GetItem(int position)
    {
      return Fragments[position];
    }

    public override int Count
    {
      get
      {
        return Fragments.Length;
      }
    }
  }
}

