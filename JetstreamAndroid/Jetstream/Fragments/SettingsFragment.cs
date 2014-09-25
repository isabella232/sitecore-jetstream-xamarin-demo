namespace JetstreamAndroid.Fragments
{
  using Android.OS;
  using Android.Views;
  using Android.Widget;
  using Android.Support.V4.App;
  using Android.App;

  public class SettingsFragment : Android.Support.V4.App.Fragment
  {
    private EditText instanceUrl;
    private EditText login;
    private EditText password;

    private Prefs prefs;

    public override View OnCreateView(LayoutInflater inflater, ViewGroup viewGroup, Bundle bundle)
    {
      View root = inflater.Inflate(Resource.Layout.fragment_settings, viewGroup, false);

      this.instanceUrl = root.FindViewById<EditText>(Resource.Id.field_instance_url);
      this.login = root.FindViewById<EditText>(Resource.Id.field_instance_login);
      this.password = root.FindViewById<EditText>(Resource.Id.field_instance_password);

      var saveButton = root.FindViewById<Button>(Resource.Id.button_save);

      saveButton.Click += (sender, args) =>
      {
        this.SaveFieldsToPrefs();
        Toast.MakeText(Activity, "Saved", ToastLength.Short).Show();
      };

      this.InitFields();

      return root;
    }

    private void SaveFieldsToPrefs()
    {
      this.prefs.InstanceUrl = this.instanceUrl.Text;
      this.prefs.Login = this.login.Text;
      this.prefs.Password = this.password.Text;
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