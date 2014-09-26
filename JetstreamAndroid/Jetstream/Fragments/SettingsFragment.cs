namespace JetstreamAndroid.Fragments
{
  using System;
  using Android.OS;
  using Android.Views;
  using Android.Widget;
  using Android.Support.V4.App;
  using Android.App;
  using JetstreamAndroid.Utils;
  using JetStreamCommons;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Session;

  public class SettingsFragment : Android.Support.V4.App.Fragment
  {
    #region Views
    private EditText instanceUrl;
    private EditText login;
    private EditText password;
    private Button saveButton;
    #endregion

    private Prefs prefs;

    public override View OnCreateView(LayoutInflater inflater, ViewGroup viewGroup, Bundle bundle)
    {
      View root = inflater.Inflate(Resource.Layout.fragment_settings, viewGroup, false);

      this.instanceUrl = root.FindViewById<EditText>(Resource.Id.field_instance_url);
      this.login = root.FindViewById<EditText>(Resource.Id.field_instance_login);
      this.password = root.FindViewById<EditText>(Resource.Id.field_instance_password);

      this.saveButton = root.FindViewById<Button>(Resource.Id.button_save);

      this.saveButton.Click += (sender, args) => this.CheckAndSaveToPrefs();

      this.InitFields();

      return root;
    }

    private async void CheckAndSaveToPrefs()
    {
      var instanceUrl = this.instanceUrl.Text;
      var login = this.login.Text;
      var pass = this.password.Text;

      bool isAuthenticated = !string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(pass);

      ISitecoreWebApiSession session;
      if (isAuthenticated)
      {
        var credentials = new WebApiCredentialsPODInsequredDemo(login, pass);

        session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(instanceUrl)
          .Credentials(credentials)
          .DefaultDatabase("master")
          .DefaultLanguage("en")
          .BuildSession();
      }
      else
      {
        session = SitecoreWebApiSessionBuilder.AnonymousSessionWithHost(instanceUrl)
          .DefaultDatabase("master")
          .DefaultLanguage("en")
          .BuildSession();
      }

      Activity.SetProgressBarIndeterminateVisibility(true);
      this.saveButton.Enabled = false;

      bool isValid = false;
      try
      {
        isValid = await session.AuthenticateAsync();
      }
      catch (System.Exception exception)
      {
        LogUtils.Info(typeof(SettingsFragment), "Failed to connect to " + instanceUrl);
      }

      this.saveButton.Enabled = true;
      Activity.SetProgressBarIndeterminateVisibility(false);

      if (isValid)
      {
        this.prefs.InstanceUrl = instanceUrl;
        this.prefs.Login = login;
        this.prefs.Password = pass;

        Toast.MakeText(Activity, "Connected. The settings is saved", ToastLength.Long).Show();
      }
      else
      {
        Toast.MakeText(Activity, "Please check your data and network availibility", ToastLength.Long).Show();
      }
    }

    public override void OnAttach(Activity activity)
    {
      base.OnAttach(activity);
      this.prefs = Prefs.From(activity);
    }

    private void InitFields()
    {
      this.instanceUrl.Text = this.prefs.InstanceUrl;
      this.login.Text = this.prefs.Login;
      this.password.Text = this.prefs.Password;
    }
  }
}