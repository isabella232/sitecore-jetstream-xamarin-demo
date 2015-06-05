namespace Jetstream.UI.Dialogs
{
  using System.Collections.Generic;
  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Views;
  using Android.Widget;
  using Com.Rengwuxian.Materialedittext;
  using DSoft.Messaging;
  using Jetstream.Utils;

  public class SettingsDialog : DialogFragment, View.IOnClickListener
  {
    private readonly Prefs prefs;

    public SettingsDialog(Prefs prefs)
    {
      this.prefs = prefs;
    }

    public override Dialog OnCreateDialog(Bundle savedInstanceState)
    {
      var rootView = this.InitContentView();
      var sitecoreUrlField = this.InitSitecoreUrlField(rootView);

      var builder = new Android.Support.V7.App.AlertDialog.Builder(this.Activity, Resource.Style.AppCompatAlertDialogStyle);
      builder.SetPositiveButton(this.GetString(Resource.String.text_button_apply), handler: null);
      builder.SetNegativeButton(this.GetString(Resource.String.text_button_cancel), handler: null);
      builder.SetView(rootView);
      
      
      var dialog = builder.Show();
      dialog.GetButton((int)DialogButtonType.Positive).SetOnClickListener(new ApplyButtonClickListener(sitecoreUrlField, this.prefs, dialog));
      dialog.SetCanceledOnTouchOutside(false);
      return dialog;
    }

    private View InitContentView()
    {
      View v = LayoutInflater.From(this.Activity).Inflate(Resource.Layout.dialog_settings, null, false);

      var toolbar = v.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
      
      toolbar.SetNavigationIcon(Resource.Drawable.abc_ic_ab_back_mtrl_am_alpha);
      toolbar.SetNavigationOnClickListener(this);
      toolbar.Title = this.GetString(Resource.String.text_settings_item);

      return v;
    }

    public void OnClick(View v)
    {
      this.Dialog.Dismiss();
    }

    private MaterialAutoCompleteTextView InitSitecoreUrlField(View rootView)
    {
      var sitecoreUrlField = rootView.FindViewById<MaterialAutoCompleteTextView>(Resource.Id.field_sitecore_url);
      sitecoreUrlField.Text = this.prefs.InstanceUrl;

      sitecoreUrlField.AddValidator(new SitecoreUrlValidator(this.GetString(Resource.String.error_wrong_url)));

      var autoCompleteAdapter = new ArrayAdapter(this.Activity, Android.Resource.Layout.SimpleDropDownItem1Line,
        new List<string>(this.prefs.SavedInstanceUrls));

      sitecoreUrlField.Adapter = autoCompleteAdapter;
      return sitecoreUrlField;
    }

    private class ApplyButtonClickListener : Java.Lang.Object, View.IOnClickListener
    {
      private MaterialAutoCompleteTextView urlField;
      private Prefs prefs;
      private readonly Dialog dialog;

      protected internal ApplyButtonClickListener(MaterialAutoCompleteTextView urlField, Prefs prefs, Dialog dialog)
      {
        this.urlField = urlField;
        this.prefs = prefs;
        this.dialog = dialog;
      }

      public void OnClick(View v)
      {
        if (this.urlField.Validate())
        {
          this.prefs.InstanceUrl = this.urlField.Text;
          this.dialog.Dismiss();
          MessageBus.PostEvent(EventIdsContainer.SitecoreInstanceUrlUpdateEvent);
        }
      }

      protected override void Dispose(bool disposing)
      {
        base.Dispose(disposing);

        this.urlField = null;
        this.prefs = null;
      }
    }
  }
}