namespace Jetstream.UI.Fragments
{
  using Android.OS;
  using Android.Support.V4.App;
  using Android.Views;
  using Android.Widget;
  using Square.Picasso;

  public class FlightStatusFragment : Fragment
  {
    private const string ImageUrl = "file:///android_asset/flight_status_logo.png";

    private TextView flightStatusTitleTextView;
    private TextView flightStatusBodyTextView;
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
      this.flightStatusTitleTextView = root.FindViewById<TextView>(Resource.Id.flight_status_title_text);
      this.flightStatusBodyTextView = root.FindViewById<TextView>(Resource.Id.flight_status_body_text);
      this.flightStatusLogoImageView = root.FindViewById<ImageView>(Resource.Id.flight_status_image);

      Picasso.With(this.Activity).Load(ImageUrl).Into(this.flightStatusLogoImageView);

      this.InitTextFields();
    }

    private void InitTextFields()
    {
      this.flightStatusTitleTextView.Text = this.Resources.GetString(Resource.String.title_text_flight_status);
      this.flightStatusBodyTextView.Text = this.Resources.GetString(Resource.String.text_screen_placeholder);
    }
  }
}