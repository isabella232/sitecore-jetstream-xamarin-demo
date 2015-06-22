namespace Jetstream.UI.Fragments
{
  using System;
  using Android.OS;
  using Android.Support.V4.App;
  using Android.Views;
  using Android.Widget;
  using JetStreamCommons;
  using JetStreamCommons.About;
  using Sitecore.MobileSDK;
  using Squareup.Picasso;

  public class AboutFragment : Fragment
  {
    private const string ImageUrl = "file:///android_asset/about_logo.jpg";

    private ProgressBar progressBar;
    private LinearLayout textFieldsContainer;

    private TextView aboutTitleTextView;
    private TextView aboutSummaryTextView;
    private TextView aboutBodyTextView;

    private ImageView aboutLogoImageView;

    private ScApiSession session;
    private IAboutPageInfo aboutItem;

    public AboutFragment(ScApiSession session)
    {
      this.session = session;
    }

    public override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);
      this.RetainInstance = true;
    }

    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
      var rootView = inflater.Inflate(Resource.Layout.activity_about, container, false);

      this.InitViews(rootView);

      return rootView;
    }

    public override void OnStart()
    {
      base.OnStart();
      if (this.aboutItem == null) 
      {
        this.LoadAboutItem();  
      } 
      else 
      {
        this.InitTextFields(this.aboutItem);
      }
    }

    private void InitViews(View root)
    {
      this.progressBar = root.FindViewById<ProgressBar>(Resource.Id.progress_bar);
      this.textFieldsContainer = root.FindViewById<LinearLayout>(Resource.Id.text_fields_container);

      this.aboutTitleTextView = root.FindViewById<TextView>(Resource.Id.about_title_text);
      this.aboutSummaryTextView = root.FindViewById<TextView>(Resource.Id.about_summary_text);
      this.aboutBodyTextView = root.FindViewById<TextView>(Resource.Id.about_body_text);

      this.aboutLogoImageView = root.FindViewById<ImageView>(Resource.Id.about_image);

      Picasso.With(this.Activity).Load(ImageUrl).Into(this.aboutLogoImageView);
    }

    private async void LoadAboutItem()
    {
      try
      {
        using (var contentLoader = new ContentLoader(this.session))
        {
          this.aboutItem = await contentLoader.LoadAboutInfo();
        }

        if (this.aboutItem != null)
        {
          this.InitTextFields(aboutItem);
        }
      }
      catch (Exception ex)
      {
        //TODO: implement logging.
      }
    }

    private void InitTextFields(IAboutPageInfo aboutItem)
    {
      this.progressBar.Visibility = ViewStates.Gone;
      this.textFieldsContainer.Visibility = ViewStates.Visible;

      this.aboutTitleTextView.Text = aboutItem.TitlePlainText;
      this.aboutSummaryTextView.Text = aboutItem.SummaryPlainText;
      this.aboutBodyTextView.Text = aboutItem.BodyPlainText + aboutItem.BodyPlainText;
    }
  }
}