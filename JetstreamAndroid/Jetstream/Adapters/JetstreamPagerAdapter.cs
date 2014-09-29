namespace JetstreamAndroid.Adapters
{
  using Android.Support.V13.App;
  using Android.App;

  public class JetstreamPagerAdapter : FragmentPagerAdapter
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

