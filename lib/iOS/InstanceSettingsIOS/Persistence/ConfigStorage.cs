
namespace SitecoreInstanceSettingsIOS.Persistence
{
  using System;
  using Foundation;

  public class ConfigStorage
  {
    private const string INSTANCE_URL_KEY = "InstanceUrlKey";
    private const string INSTANCE_SITE_KEY = "InstanceSiteNameKey";
    private const string INSTANCE_DB_KEY = "InstanceDatabaseKey";
    private const string INSTANCE_LANG_KEY = "InstanceLanguageKey";
    private const string INSTANCE_LOGIN_KEY = "InstanceLoginKey";
    private const string INSTANCE_PASSWORD_KEY = "InstancePasswordKey";
    private const string PATH_TO_ITEMS_KEY = "PathToItemsKey";

    private SecureAccountStorage secureStorage;

    public ConfigStorage()
    {
      this.secureStorage = new SecureAccountStorage();
    }

    private void SaveValueForKey(string value, string key)
    {
      NSUserDefaults.StandardUserDefaults.SetString(value, key); 
      NSUserDefaults.StandardUserDefaults.Synchronize();
    }

    private string GetValueForKey(string key)
    {
      string value = NSUserDefaults.StandardUserDefaults.StringForKey(key);

      if (value == null)
      {
        value = "";
      }

      return value;
    }

    public string InstanceUrl
    {
      get
      { 
        return GetValueForKey(INSTANCE_URL_KEY); 
      }
      set
      { 
        SaveValueForKey(value, INSTANCE_URL_KEY); 
      }
    }

    public string InstanceSiteName
    {
      get
      { 
        return GetValueForKey(INSTANCE_SITE_KEY); 
      }
      set
      { 
        SaveValueForKey(value, INSTANCE_SITE_KEY); 
      }
    }


    public string InstanceDatabase
    {
      get
      { 
        return GetValueForKey(INSTANCE_DB_KEY); 
      }
      set
      { 
        SaveValueForKey(value, INSTANCE_DB_KEY); 
      }
    }

    public string InstanceLanguage
    {
      get
      { 
        return GetValueForKey(INSTANCE_LANG_KEY); 
      }
      set
      { 
        SaveValueForKey(value, INSTANCE_LANG_KEY); 
      }
    }

    public string InstanceLogin
    {
      get
      { 
        return GetValueForKey(INSTANCE_LOGIN_KEY); 
      }
      set
      { 
        SaveValueForKey(value, INSTANCE_LOGIN_KEY); 
      }
    }

    public string PathToItems
    {
      get
      { 
        return GetValueForKey(PATH_TO_ITEMS_KEY); 
      }
      set
      { 
        SaveValueForKey(value, PATH_TO_ITEMS_KEY); 
      }
    }


    public string InstancePassword
    {
      get
      { 
        string login = InstanceLogin;

        if (login == null)
        {
          throw new UnauthorizedAccessException("Set Login to retrieve the password");
        }

        string password = null;
        try
        {
         password = this.secureStorage.GetAccountPassword(InstanceLogin);
        }
        catch
        {
          //no saved password for this login, do nothing
        }

        if (password == null)
        {
          return "";
        }

        return password;
      }
      set
      { 
        string login = InstanceLogin;

        if (login == null)
        {
          throw new UnauthorizedAccessException("Login must be set to store the password");
        }

        this.secureStorage.StoreUserAccount(InstanceLogin, value); 
      }
    }
  }
}

