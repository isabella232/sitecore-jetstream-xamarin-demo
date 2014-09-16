namespace JetstreamAndroid.Fragments
{
  using Android.App;
  using Android.OS;
  using Android.Views;

  public class BookFlightFragment : Fragment
  {
    public override View OnCreateView(LayoutInflater inflater, ViewGroup viewGroup, Bundle bundle)
    {
      return inflater.Inflate(Resource.Layout.fragment_search, viewGroup, false);
    }
  }
}