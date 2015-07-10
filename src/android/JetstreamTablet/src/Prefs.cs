namespace Jetstream
{
  using System.Collections.Generic;
  using System.Linq;
  using Android.Content;
  using Android.Preferences;

  public class Prefs
  {
    private readonly ISharedPreferences prefs;


    private const int MaxItemsInUrlHistory = 10;
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
        var url = this.GetString(InstanceUrlKey, DefaultInstanceUrl);
        return url;
      }

      set
      {
        this.PutString(InstanceUrlKey, value);
        this.AddUrlToHistory(value);
      }
    }

    #endregion Instance URL

    #region Instance URLs history

    public IList<string> SavedInstanceUrls
    {
      get
      {
        var raw = this.GetString(SavedInstanceUrlsKey, DefaultInstanceUrl);

        var list = raw.Split('|').ToList();

        return list;
      }

      private set
      {
        this.PutString(SavedInstanceUrlsKey, string.Join("|", value));
      }
    }

    private void AddUrlToHistory(string url)
    {
      var urls = this.SavedInstanceUrls;
      if (urls.Contains(url))
      {
        return;
      }

      urls.Add(url);

      urls = RemoveItemIfMoreThen(urls, MaxItemsInUrlHistory);

      this.SavedInstanceUrls = urls;
    }

    private static IList<string> RemoveItemIfMoreThen(IList<string> source, int maxCount)
    {
      if (source.Count > maxCount)
      {
        source.RemoveAt(0);
      }

      return source;
    }

    public void ClearUrlHistory()
    {
      this.SavedInstanceUrls = new List<string>();
    }

    #endregion Instance URLs history

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