namespace Jetstream.UI.Fragments
{
  using Android.OS;
  using Android.Support.V4.App;
  using Android.Views;
  using Android.Widget;
  using Squareup.Picasso;

  public class FlightStatusFragment : Fragment
  {
    private const string ImageUrl = "file:///android_asset/flight_status_logo.png";

    private LinearLayout textFieldsContainer;
    private TextView flightStatusTitleTextView;
    private ImageView flightStatusLogoImageView;

    public override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);
      this.RetainInstance = true;
    }

    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
      var rootView = inflater.Inflate(Resource.Layout.fragment_flight_status, container, false);

      this.InitViews(rootView);

      return rootView;
    }

    private void InitViews(View root)
    {
      this.textFieldsContainer = root.FindViewById<LinearLayout>(Resource.Id.text_fields_container);
      this.flightStatusTitleTextView = root.FindViewById<TextView>(Resource.Id.check_in_title_text);
      this.flightStatusLogoImageView = root.FindViewById<ImageView>(Resource.Id.check_in_image);

      Picasso.With(this.Activity).Load(ImageUrl).Into(this.flightStatusLogoImageView);

      this.InitTextFields();
    }

    private void InitTextFields()
    {
      this.flightStatusTitleTextView.Text = "Flight Status";
    }
  }
}