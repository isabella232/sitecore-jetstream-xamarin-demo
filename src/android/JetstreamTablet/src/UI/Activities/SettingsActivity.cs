using Xamarin;

namespace Jetstream.UI.Activities
{
  using System.Collections.Generic;
  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Support.V7.App;
  using Android.Views;
  using Android.Views.InputMethods;
  using Android.Widget;
  using Com.Rengwuxian.Materialedittext;
  using DSoft.Messaging;
  using Jetstream.Utils;

  [Activity(Theme = "@style/Settings.Activity.Theme")]
  public class SettingsActivity : AppCompatActivity, View.IOnClickListener, TextView.IOnEditorActionListener
  {
    private Prefs prefs;

    private Android.Support.V7.Widget.Toolbar toolbar;
    private MaterialAutoCompleteTextView sitecoreUrlField;
    private Button applyButton;
    private Button cancelButton;

    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);
      this.SetContentView(Resource.Layout.activity_settings);

      this.prefs = Prefs.From(this);

      this.InitContentView();
    }

    private void InitContentView()
    {
      this.toolbar = this.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
      this.toolbar.Title = this.GetString(Resource.String.menu_text_settings);

      this.SetSupportActionBar(this.toolbar);

      this.toolbar.SetNavigationIcon(Resource.Drawable.abc_ic_ab_back_mtrl_am_alpha);
      this.toolbar.SetNavigationOnClickListener(this);
      this.toolbar.SetBackgroundResource(Resource.Color.color_primary);

      this.Title = "";
      this.InitSitecoreUrlField();

      this.applyButton = this.FindViewById<Button>(Resource.Id.button_apply);
      this.applyButton.Click += (sender, args) => this.Apply();

      this.cancelButton = this.FindViewById<Button>(Resource.Id.button_cancel);
      this.cancelButton.Click += (sender, args) => this.Finish();
    }

    private void InitSitecoreUrlField()
    {
      this.sitecoreUrlField = this.FindViewById<MaterialAutoCompleteTextView>(Resource.Id.field_sitecore_url);
      this.sitecoreUrlField.Text = this.prefs.InstanceUrl;

      this.sitecoreUrlField.AddValidator(new SitecoreUrlValidator(this.GetString(Resource.String.error_wrong_url)));

      var autoCompleteAdapter = new HidingKeyboardAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line,
        new List<string>(this.prefs.SavedInstanceUrls), this.sitecoreUrlField);

      this.sitecoreUrlField.Adapter = autoCompleteAdapter;

      this.sitecoreUrlField.SetOnEditorActionListener(this);
    }

    public bool OnActionItemClicked(ActionMode mode, IMenuItem item)
    {
      return false;
    }

    public bool OnCreateActionMode(ActionMode mode, IMenu menu)
    {
      this.toolbar.Visibility = ViewStates.Gone;
      return false;
    }

    public void OnDestroyActionMode(ActionMode mode)
    {
      this.toolbar.Visibility = ViewStates.Visible;
    }

    public bool OnPrepareActionMode(ActionMode mode, IMenu menu)
    {
      return false;
    }

    private void Apply()
    {
      if (this.sitecoreUrlField.Validate())
      {
        if (this.sitecoreUrlField.Text != this.prefs.InstanceUrl)
        {
          AnalyticsHelper.TrackUrlChanged();  
        }

        this.prefs.InstanceUrl = this.sitecoreUrlField.Text;
        MessageBus.PostEvent(EventIdsContainer.SitecoreInstanceUrlUpdateEvent);
        this.Finish();
      }
    }

    public bool OnEditorAction(TextView v, ImeAction actionId, KeyEvent e)
    {
      if (actionId == ImeAction.Done)
      {
        this.Apply();
        return true;
      }

      return false;
    }

    public void OnClick(View v)
    {
      this.Finish();
    }
  }

  class HidingKeyboardAdapter : ArrayAdapter<string>, View.IOnTouchListener
  {
    private readonly MaterialAutoCompleteTextView field;

    public HidingKeyboardAdapter(Context context, int textViewResourceId, MaterialAutoCompleteTextView field)
      : base(context, textViewResourceId)
    {
      this.field = field;
    }

    public HidingKeyboardAdapter(Context context, int resource, int textViewResourceId, MaterialAutoCompleteTextView field)
      : base(context, resource, textViewResourceId)
    {
      this.field = field;
    }

    public HidingKeyboardAdapter(Context context, int textViewResourceId, string[] objects, MaterialAutoCompleteTextView field)
      : base(context, textViewResourceId, objects)
    {
      this.field = field;
    }

    public HidingKeyboardAdapter(Context context, int resource, int textViewResourceId, string[] objects, MaterialAutoCompleteTextView field)
      : base(context, resource, textViewResourceId, objects)
    {
      this.field = field;
    }

    public HidingKeyboardAdapter(Context context, int textViewResourceId, IList<string> objects, MaterialAutoCompleteTextView field)
      : base(context, textViewResourceId, objects)
    {
      this.field = field;
    }

    public HidingKeyboardAdapter(Context context, int resource, int textViewResourceId, IList<string> objects, MaterialAutoCompleteTextView field)
      : base(context, resource, textViewResourceId, objects)
    {
      this.field = field;
    }

    public override View GetView(int position, View convertView, ViewGroup parent)
    {
      if (parent != null)
      {
        parent.SetOnTouchListener(this);
      }

      return base.GetView(position, convertView, parent);
    }

    public bool OnTouch(View v, MotionEvent e)
    {
      InputMethodManager imm = (InputMethodManager)this.Context.GetSystemService(Context.InputMethodService);
      imm.HideSoftInputFromWindow(this.field.WindowToken, 0);

      v.OnTouchEvent(e);

      return false;
    }
  }
}