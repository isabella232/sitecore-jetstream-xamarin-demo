namespace JetstreamAndroid
{
  using Android.Views;
  using Android.OS;
  using Android.Widget;

  public class AboutFragment : Android.Support.V4.App.Fragment
  {
    public override View OnCreateView(LayoutInflater inflater, ViewGroup viewGroup, Bundle bundle)
    {
      View root = inflater.Inflate(Resource.Layout.fragment_about, viewGroup, false);

      var textview = root.FindViewById<TextView>(Resource.Id.textview_about_text);
      textview.Text = GetString(Resource.String.text_about_screen);

      return root;
    }
  }
}

