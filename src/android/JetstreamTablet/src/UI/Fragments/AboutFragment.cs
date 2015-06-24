namespace Jetstream.UI.Fragments
{
  using System;
  using Android.OS;
  using Android.Support.V4.App;
  using Android.Views;
  using Android.Widget;
  using com.dbeattie;
  using DSoft.Messaging;
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

    private IAboutPageInfo aboutItem;

    MessageBusEventHandler refreshEventHandler;
    MessageBusEventHandler updateInstanceUrlEventHandler;

    bool refreshOnHiddenChanged = false;

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

    public override void OnHiddenChanged(bool hidden)
    {
      base.OnHiddenChanged(hidden);

      if (!hidden && this.refreshOnHiddenChanged)
      {
        this.refreshOnHiddenChanged = false;
        this.LoadAboutItem();
      }

      if (hidden)
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
      if (this.aboutItem == null)
      {
        this.LoadAboutItem();
      }
      else
      {
        this.InitTextFields(this.aboutItem);
      }

      if (this.refreshEventHandler == null)
      {
        this.refreshEventHandler = new MessageBusEventHandler()
        {
          EventId = EventIdsContainer.RefreshMenuActionClickedEvent,
          EventAction = (sender, evnt) =>
              this.Activity.RunOnUiThread(this.LoadAboutItem)
        };
      }

      if (this.updateInstanceUrlEventHandler == null)
      {
        this.updateInstanceUrlEventHandler = new MessageBusEventHandler()
          {
            EventId = EventIdsContainer.SitecoreInstanceUrlUpdateEvent,
            EventAction = (sender, evnt) =>
            {
              if (this.IsHidden)
              {
                this.refreshOnHiddenChanged = true;
              }

              if (this.IsHidden || this.IsRefreshing())
              {
                return;;
              }

              this.Activity.RunOnUiThread(this.LoadAboutItem);
            }
          };
      }

      MessageBus.Default.Register(this.refreshEventHandler);
      MessageBus.Default.Register(this.updateInstanceUrlEventHandler);
    }

    public override void OnDestroy()
    {
      base.OnDestroy();

      MessageBus.Default.DeRegister(this.refreshEventHandler);
      MessageBus.Default.DeRegister(this.updateInstanceUrlEventHandler);
    }

    public bool IsRefreshing()
    {
      return this.progressBar.Visibility == ViewStates.Visible;
    }

    private void InitViews(View root)
    {
      this.progressBar = root.FindViewById<ProgressBar>(Jetstream.Resource.Id.progress_bar);
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
        this.textFieldsContainer.Visibility = ViewStates.Invisible;

        using (var contentLoader = new ContentLoader(this.GetSession()))
        {
          this.aboutItem = await contentLoader.LoadAboutInfo();
        }

        if (this.aboutItem != null)
        {
          this.InitTextFields(this.aboutItem);
        }
      }
      catch (Exception ex)
      {
        this.progressBar.Visibility = ViewStates.Gone;

        SnackbarManager.Show(
          Snackbar.With(this.Activity)
          .ActionLabel(this.Resources.GetString(Jetstream.Resource.String.error_text_retry))
          .ActionColor(this.Resources.GetColor(Jetstream.Resource.Color.color_accent))
          .ActionListener(this)
          .Text(this.Resources.GetString(Jetstream.Resource.String.error_text_fail_to_load_about)));
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
      this.progressBar.Visibility = ViewStates.Gone;
      this.textFieldsContainer.Visibility = ViewStates.Visible;

      this.aboutTitleTextView.Text = aboutItem.TitlePlainText;
      this.aboutSummaryTextView.Text = aboutItem.SummaryPlainText;
      this.aboutBodyTextView.Text = aboutItem.BodyPlainText;
    }
  }
}