namespace JetstreamAndroid.Fragments
{
  using Android.App;
  using Android.OS;
  using Android.Views;

  public class FilterFragment : DialogFragment
  {
    public static FilterFragment NewInstance()
    {
      return new FilterFragment();
    }

    public override Dialog OnCreateDialog(Bundle savedInstanceState)
    {
      var builder = new AlertDialog.Builder(Activity);

      builder.SetTitle("Filter");
      builder.SetView(this.CreateView());
      builder.SetPositiveButton("Apply", (sender, args) =>
      {

      });

      builder.SetNegativeButton("Clear", (sender, args) => this.Dialog.Cancel());

      return builder.Create();
    }

    private View CreateView()
    {
      var rootView = Activity.LayoutInflater.Inflate(Resource.Layout.fragment_filter, null);


      return rootView;
    }
  }
}