using Sitecore.MobileSDK.PasswordProvider.Android;

namespace JetstreamAndroid
{
  using Android.Content;
  using Android.Preferences;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Session;

  public class Prefs
  {
    private readonly ISharedPreferences prefs;
    private readonly Context context;

    private Prefs(Context context, ISharedPreferences sharedPreferences)
    {
      this.context = context;
      this.prefs = sharedPreferences;
    }

    #region Instance URL

    public string InstanceUrl
    {
      get
      {
        string instanceUrlKey = this.context.GetString(Resource.String.key_instance_url);
        string defaultInstanceUrl = this.context.GetString(Resource.String.text_default_instance_url);

        return this.GetString(instanceUrlKey, defaultInstanceUrl);
      }

      set
      {
        this.PutString(this.context.GetString(Resource.String.key_instance_url), value);
      }
    }

    #endregion Instance URL

    #region Login
    public string Login
    {
      get
      {
        string loginKey = this.context.GetString(Resource.String.key_login);
        string defaultLogin = this.context.GetString(Resource.String.text_default_login);

        return this.GetString(loginKey, defaultLogin);
      }

      set
      {
        this.PutString(this.context.GetString(Resource.String.key_login), value);
      }
    }

    #endregion Login

    #region Database
    public string Database
    {
      get
      {
        return context.GetString(Resource.String.text_default_database);
      }

      set
      {
      }
    }

    #endregion Database

    #region Language
    public string Language
    {
      get
      {
        return context.GetString(Resource.String.text_default_language);
      }

      set
      {
      }
    }

    #endregion Language

    #region Password
    public string Password
    {
      get
      {
        string passwordKey = this.context.GetString(Resource.String.key_password);
        string defaultPassword = this.context.GetString(Resource.String.text_default_password);

        return this.GetString(passwordKey, defaultPassword);
      }

      set
      {
        this.PutString(this.context.GetString(Resource.String.key_password), value);
      }
    }

    #endregion Password

    public ISitecoreWebApiSession Session
    {
      get
      {
        bool isAuthentiated = !string.IsNullOrEmpty(this.Login) && !string.IsNullOrEmpty(this.Password);

        ISitecoreWebApiSession session = null;
        if (isAuthentiated)
        {
          using (var credentials = new SecureStringPasswordProvider(this.Login, this.Password))
          {
            session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(this.InstanceUrl)
                      .Credentials(credentials)
                      .DefaultDatabase(this.Database)
                      .DefaultLanguage(this.Language)
                      .BuildSession();
          }
        }
        else
        {
          session = SitecoreWebApiSessionBuilder.AnonymousSessionWithHost(this.InstanceUrl)
            .DefaultDatabase(this.Database)
            .DefaultLanguage(this.Language)
            .BuildSession();
        }

        return session;
      }
    }

    public static Prefs From(Context context)
    {
      return new Prefs(context, PreferenceManager.GetDefaultSharedPreferences(context));
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