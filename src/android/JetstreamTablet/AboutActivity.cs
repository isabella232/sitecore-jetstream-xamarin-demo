namespace Jetstream
{
  using System;
  using Android.App;
  using Android.OS;
  using Android.Support.V7.App;
  using Android.Views;
  using Android.Widget;
  using JetStreamCommons;
  using JetStreamCommons.About;
  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.PasswordProvider.Android;
  using Squareup.Picasso;

  [Activity]
  public class AboutActivity : AppCompatActivity, Android.Views.View.IOnClickListener
  {
    private ProgressBar progressBar;
    private LinearLayout textFieldsContainer;

    private TextView aboutTitleTextView;
    private TextView aboutSummaryTextView;
    private TextView aboutBodyTextView;

    private ImageView aboutLogoImageView;

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      this.SetContentView(Resource.Layout.activity_about);

      this.InitToolbar();

      this.InitViews();

      this.LoadAboutItem();
    }

    private void InitToolbar()
    {
      var toolbar = this.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
      this.SetSupportActionBar(toolbar);

      toolbar.SetNavigationIcon(Resource.Drawable.abc_ic_ab_back_mtrl_am_alpha);
      toolbar.SetNavigationOnClickListener(this);
      toolbar.SetLogo(Resource.Drawable.ic_jetstream_logo);

      this.Title = "";
    }

    private void InitViews()
    {
      this.progressBar = this.FindViewById<ProgressBar>(Resource.Id.progress_bar);
      this.textFieldsContainer = this.FindViewById<LinearLayout>(Resource.Id.text_fields_container);

      this.aboutTitleTextView = this.FindViewById<TextView>(Resource.Id.about_title_text);
      this.aboutSummaryTextView = this.FindViewById<TextView>(Resource.Id.about_summary_text);
      this.aboutBodyTextView = this.FindViewById<TextView>(Resource.Id.about_body_text);

      this.aboutLogoImageView = this.FindViewById<ImageView>(Resource.Id.about_image);


      Picasso.With(this).Load("file:///android_asset/about_logo.jpg").Into(this.aboutLogoImageView);
    }

    private async void LoadAboutItem()
    {
      try
      {
        IAboutPageInfo aboutItem = null;
        using (var session = this.CreateSession())
        using (var contentLoader = new ContentLoader(session))
        {
          aboutItem = await contentLoader.LoadAboutInfo();
        }

        if (aboutItem != null)
        {
          this.InitTextFields(aboutItem);
        }
      }
      catch (Exception ex)
      {
        
      }
    }

    private void InitTextFields(IAboutPageInfo aboutItem)
    {
      this.progressBar.Visibility = ViewStates.Gone;
      this.textFieldsContainer.Visibility = ViewStates.Visible;

      this.aboutTitleTextView.Text = aboutItem.TitlePlainText;
      this.aboutSummaryTextView.Text = aboutItem.SummaryPlainText;
      this.aboutBodyTextView.Text = aboutItem.BodyPlainText;
    }

    private ISitecoreWebApiSession CreateSession()
    {
       //TODO:
        ISitecoreWebApiSession session = null;
        //        if (isAuthentiated)
        //        {
        using (var credentials = new SecureStringPasswordProvider("sitecore\\admin", "b"))
        {
          session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(Prefs.From(this).InstanceUrl)
                        .Credentials(credentials)
                          .DefaultDatabase("web")
                        .BuildSession();
        }
        //        }
        //        else
        //        {
        //        session = SitecoreWebApiSessionBuilder.AnonymousSessionWithHost("jetstream800394rev150402.test24dk1.dk.sitecore.net")
        //          .DefaultDatabase("web")
        //            .BuildSession();
        //        }

        return (ScApiSession)session;
    }

    public void OnClick(Android.Views.View v)
    {
      this.Finish();
    }
  }
}