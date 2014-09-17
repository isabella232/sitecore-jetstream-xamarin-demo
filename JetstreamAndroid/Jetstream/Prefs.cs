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

        ISitecoreWebApiSession session;
        if (isAuthentiated)
        {
          var credentials = new Credentials(this.Login, this.Password);

          session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(this.InstanceUrl)
            .Credentials(credentials)
            .BuildSession();
        }
        else
        {
          session = SitecoreWebApiSessionBuilder.AnonymousSessionWithHost(this.InstanceUrl)
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

    class Credentials : IWebApiCredentials
    {
      private string login;
      private string password;

      public Credentials(string login, string password)
      {
        this.login = login;
        this.password = password;
      }

      public IWebApiCredentials CredentialsShallowCopy()
      {
        return new Credentials(this.login, this.password);
      }

      public void Dispose()
      {
        this.login = null;
        this.password = null;
      }

      public string Username
      {
        get
        {
          return this.login;
        }
      }

      public string Password
      {
        get
        {
          return this.password;
        }
      }
    }
  }
}