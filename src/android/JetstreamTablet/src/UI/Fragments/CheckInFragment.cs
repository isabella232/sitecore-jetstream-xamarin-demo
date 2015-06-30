namespace Jetstream.UI.Fragments
{
  using Android.OS;
  using Android.Support.V4.App;
  using Android.Views;
  using Android.Widget;
  using Square.Picasso;

  public class CheckInFragment : Fragment
  {
    private const string ImageUrl = "file:///android_asset/check_in_logo.jpg";

    private TextView checkInTitleTextView;
    private ImageView checkInLogoImageView;

    public override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);
      this.RetainInstance = true;
    }

    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
      var rootView = inflater.Inflate(Resource.Layout.fragment_check_in, container, false);

      this.InitViews(rootView);

      return rootView;
    }

    private void InitViews(View root)
    {
      this.checkInTitleTextView = root.FindViewById<TextView>(Resource.Id.check_in_title_text);
      this.checkInLogoImageView = root.FindViewById<ImageView>(Resource.Id.check_in_image);

      Picasso.With(this.Activity).Load(ImageUrl).Into(this.checkInLogoImageView);

      this.InitTextFields();
    }

    private void InitTextFields()
    {
      this.checkInTitleTextView.Text = this.Resources.GetString(Resource.String.title_text_check_in);
    }
  }
}