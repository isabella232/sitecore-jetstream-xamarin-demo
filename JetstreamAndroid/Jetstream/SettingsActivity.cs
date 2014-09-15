namespace JetstreamAndroid
{
  using Android.App;
  using Android.Content.PM;
  using Android.OS;
  using Android.Widget;

  [Activity(ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = true)]
  public class SettingsActivity : Activity
  {
    private Prefs prefs;

    private EditText instanceUrl;
    private EditText login;
    private EditText password;

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      this.SetContentView(Resource.Layout.activity_settings);
      this.prefs = Prefs.From(this);

      this.InitFields();

      Button saveButton = this.FindViewById<Button>(Resource.Id.button_save);

      saveButton.Click += (sender, args) =>
      {
        this.SaveFieldsToPrefs();
        Toast.MakeText(this, "Saved", ToastLength.Short).Show();
      };
    }

    private void SaveFieldsToPrefs()
    {
      this.prefs.InstanceUrl = this.instanceUrl.Text;
      this.prefs.Login = this.login.Text;
      this.prefs.Password = this.password.Text;
    }

    private void InitFields()
    {
      this.instanceUrl = this.FindViewById<EditText>(Resource.Id.field_instance_url);
      this.login = this.FindViewById<EditText>(Resource.Id.field_instance_login);
      this.password = this.FindViewById<EditText>(Resource.Id.field_instance_password);

      this.instanceUrl.Text = this.prefs.InstanceUrl;
      this.login.Text = this.prefs.Login;
      this.password.Text = this.prefs.Password;
    }
  }
}