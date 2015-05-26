namespace Jetstream.UI.Fragments
{
  using Android.App;
  using Android.OS;
  using Android.Views;
  using Com.Rengwuxian.Materialedittext;

  public class SettingsFragment : Fragment
  {
    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
      var edittextField = new MaterialEditText(this.Activity)
      {
        Text = "test"
      };

      return edittextField;
    }
  }
}