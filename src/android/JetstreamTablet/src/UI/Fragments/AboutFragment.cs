namespace Jetstream.UI.Fragments
{
  using System;
  using Android.OS;
  using Android.Views;
  using Android.Widget;
  using com.dbeattie;
  using Com.Lsjwzh.Widget.Materialloadingprogressbar;
  using JetStreamCommons;
  using JetStreamCommons.About;
  using Sitecore.MobileSDK;
  using Square.Picasso;

  public class AboutFragment : BaseFragment, IActionClickListener
  {
    private const string ImageUrl = "file:///android_asset/about_logo.jpg";

    private CircleProgressBar progressBar;
    private LinearLayout textFieldsContainer;

    private TextView aboutTitleTextView;
    private TextView aboutSummaryTextView;
    private TextView aboutBodyTextView;

    private ImageView aboutLogoImageView;

    private IAboutPageInfo aboutItem;

    public override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);
      this.RetainInstance = true;
    }

    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
      var rootView = inflater.Inflate(Jetstream.Resource.Layout.fragment_about, container, false);

      this.InitViews(rootView);

      return rootView;
    }

    public override void OnRefreshed()
    {
      this.LoadAboutItem();
    }

    protected override bool IsRefreshing
    {
      get
      {
        return this.progressBar.Visibility == ViewStates.Visible;
      }
    }

    public override void OnStart()
    {
      base.OnStart();
      if(this.aboutItem == null)
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
      this.progressBar = root.FindViewById<CircleProgressBar>(Jetstream.Resource.Id.refresher);
      this.textFieldsContainer = root.FindViewById<LinearLayout>(Jetstream.Resource.Id.text_fields_container);

      this.aboutTitleTextView = root.FindViewById<TextView>(Jetstream.Resource.Id.about_title_text);
      this.aboutSummaryTextView = root.FindViewById<TextView>(Jetstream.Resource.Id.about_summary_text);
      this.aboutBodyTextView = root.FindViewById<TextView>(Jetstream.Resource.Id.about_body_text);

      this.aboutLogoImageView = root.FindViewById<ImageView>(Jetstream.Resource.Id.about_image);

      Picasso.With(this.Activity).Load(ImageUrl).Into(this.aboutLogoImageView);
    }

    private async void LoadAboutItem()
    {
      try
      {
        this.progressBar.Visibility = ViewStates.Visible;

        using (var session = this.GetSession())
        using (var contentLoader = new ContentLoader(session))
        {
          this.aboutItem = await contentLoader.LoadAboutInfo();
        }

        if(this.aboutItem != null)
        {
          this.InitTextFields(this.aboutItem);
        }

        this.progressBar.Visibility = ViewStates.Invisible;
      }
      catch (Exception ex)
      {
        this.progressBar.Visibility = ViewStates.Invisible;

        AppLog.Logger.Error(this.Resources.GetString(Jetstream.Resource.String.error_text_fail_to_load_about), ex);

        if(this.IsAdded)
        {
          SnackbarManager.Show(
            Snackbar.With(this.Activity)
          .ActionLabel(this.Resources.GetString(Jetstream.Resource.String.error_text_retry))
          .ActionColor(this.Resources.GetColor(Jetstream.Resource.Color.color_accent))
          .ActionListener(this)
          .Text(this.Resources.GetString(Jetstream.Resource.String.error_text_fail_to_load_about)));
        }
      }
    }

    private ScApiSession GetSession()
    {
      return (this.Activity.Application as JetstreamApplication).Session;
    }

    public void OnActionClicked(Snackbar snackbar)
    {
      this.LoadAboutItem();
    }

    private void InitTextFields(IAboutPageInfo aboutItem)
    {
      this.textFieldsContainer.Visibility = ViewStates.Visible;

      this.aboutTitleTextView.Text = aboutItem.TitlePlainText;
      this.aboutSummaryTextView.Text = aboutItem.SummaryPlainText;
      this.aboutBodyTextView.Text = aboutItem.BodyPlainText;
    }
  }
}