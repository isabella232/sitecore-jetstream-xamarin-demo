namespace Jetstream
{
  using Android.Content;
  using Android.Preferences;

  public class Prefs
  {
    private readonly ISharedPreferences prefs;

    private const string InstanceUrlKey = "instance_url_key";

    private Prefs(ISharedPreferences sharedPreferences)
    {
      this.prefs = sharedPreferences;
    }

    public static Prefs From(Context context)
    {
      return new Prefs(PreferenceManager.GetDefaultSharedPreferences(context));
    }

    #region Instance URL

    public string InstanceUrl
    {
      get
      {
        return this.GetString(InstanceUrlKey, "http://jetstream800394rev150402.test24dk1.dk.sitecore.net/");
      }

      set
      {
        this.PutString(InstanceUrlKey, value);
      }
    }

    #endregion Instance URL

    private bool GetBool(string key, bool defaultValue)
    {
      return this.prefs.GetBoolean(key, defaultValue);
    }

    private void PutBool(string key, bool value)
    {
      ISharedPreferencesEditor editor = this.prefs.Edit();
      editor.PutBoolean(key, value);
      editor.Apply();
    }

    private string GetString(string key, string defaultValue)
    {
      return this.prefs.GetString(key, defaultValue);
    }

    private void PutString(string key, string value)
    {
      ISharedPreferencesEditor editor = this.prefs.Edit();
      editor.PutString(key, value);
      editor.Apply();
    }

  }
}