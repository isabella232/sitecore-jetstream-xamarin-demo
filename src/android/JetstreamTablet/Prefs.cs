namespace Jetstream
{
  using System.Collections.Generic;
  using Android.Content;
  using Android.Preferences;

  public class Prefs
  {
    private readonly ISharedPreferences prefs;

    private const string InstanceUrlKey = "instance_url_key";
    private const string SavedInstanceUrlsKey = "saved_instance_urls_key";

    public const string DefaultInstanceUrl = "http://jetstream800394rev150402.test24dk1.dk.sitecore.net/";

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
        return this.GetString(InstanceUrlKey, DefaultInstanceUrl);
      }

      set
      {
        this.PutString(InstanceUrlKey, value);
        this.AddUrlToHistory(value);
      }
    }

    #endregion Instance URL

    #region Instance URLs history

    public ICollection<string> SavedInstanceUrls
    {
      get
      {
        return this.GetStringSet(SavedInstanceUrlsKey);
      }

      private set
      {
        this.PutStringSet(SavedInstanceUrlsKey, value);
      }
    }

    private void AddUrlToHistory(string url)
    {
      var urls = this.SavedInstanceUrls;
      urls.Add(url);

      this.SavedInstanceUrls = urls;
    }

    public void ClearUrlHistory()
    {
      this.SavedInstanceUrls = new List<string>();
    }

    #endregion Instance URLs history

    private ICollection<string> GetStringSet(string key)
    {
      return this.prefs.GetStringSet(key, new List<string>(0));
    }

    private void PutStringSet(string key, ICollection<string> values)
    {
      ISharedPreferencesEditor editor = this.prefs.Edit();
      editor.PutStringSet(key, values);
      editor.Apply();
    }

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