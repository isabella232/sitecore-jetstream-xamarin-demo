using DSoft.Messaging;
using com.dbeattie;

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

  public class AboutFragment : Fragment, IActionClickListener
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

    MessageBusEventHandler refreshEventHandler;

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
      var rootView = inflater.Inflate(Resource.Layout.fragment_about, container, false);

      this.InitViews(rootView);

      return rootView;
    }

    public override void OnHiddenChanged(bool hidden)
    {
      base.OnHiddenChanged(hidden);
      if(hidden)
      {
        MessageBus.Default.DeRegister(this.refreshEventHandler);
      }
      else
      {
        MessageBus.Default.Register(this.refreshEventHandler);
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
    
      if(this.refreshEventHandler == null)
      {
        this.refreshEventHandler = new MessageBusEventHandler()
        {
          EventId = EventIdsContainer.RefreshMenuActionClickedEvent,
          EventAction = (sender, evnt) =>
              this.Activity.RunOnUiThread(delegate
            {
              this.LoadAboutItem();
            })
        };
      }

      MessageBus.Default.Register(this.refreshEventHandler);
    }

    public override void OnPause()
    {
      base.OnPause();

      MessageBus.Default.DeRegister(this.refreshEventHandler);
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
        this.progressBar.Visibility = ViewStates.Visible;

        using (var contentLoader = new ContentLoader(this.session))
        {
          this.aboutItem = await contentLoader.LoadAboutInfo();
        }

        if(this.aboutItem != null)
        {
          this.InitTextFields(aboutItem);
        }
      }
      catch (Exception ex)
      {
        this.progressBar.Visibility = ViewStates.Gone;

        SnackbarManager.Show(
          Snackbar.With(this.Activity)
          .ActionLabel(Resources.GetString(Resource.String.error_text_retry))
          .ActionColor(this.Resources.GetColor(Resource.Color.color_accent))
          .ActionListener(this)
          .Text(Resources.GetString(Resource.String.error_text_fail_to_load_about)));
      }
    }

    public void OnActionClicked(Snackbar snackbar)
    {
      this.LoadAboutItem();
    }

    private void InitTextFields(IAboutPageInfo aboutItem)
    {
      this.progressBar.Visibility = ViewStates.Gone;
      this.textFieldsContainer.Visibility = ViewStates.Visible;

      this.aboutTitleTextView.Text = aboutItem.TitlePlainText;
      this.aboutSummaryTextView.Text = aboutItem.SummaryPlainText;
      this.aboutBodyTextView.Text = aboutItem.BodyPlainText;
    }
  }
}