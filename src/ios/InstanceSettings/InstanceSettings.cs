using Foundation;

namespace InstanceSettings
{
  using System;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Session;

  using Sitecore.MobileSDK.PasswordProvider.iOS;


  //TODO: NSUserDefaults is not secure enough for credentials.
  //  Please use either of :
  // * native iOS keychain
  // * C# SecureString class
  public class InstanceSettings
  {
    private string instanceUrl;
    private string instanceLogin;
    private string instancePassword;
    private string instanceSite;
    private string instanceDataBase;
    private string instanceLanguage;

    public InstanceSettings()
    {
      this.ReadValuesFromStorage();
    }

    private void ReadValuesFromStorage()
    {
      NSUserDefaults userDefaults = NSUserDefaults.StandardUserDefaults;
      this.instanceUrl            = userDefaults.StringForKey("instanceUrl"     );
      this.instanceLogin          = userDefaults.StringForKey("instanceLogin"   );
      this.instancePassword       = userDefaults.StringForKey("instancePassword");
      this.instanceSite           = userDefaults.StringForKey("instanceSite"    );
      this.instanceDataBase       = userDefaults.StringForKey("instanceDataBase");
      this.instanceLanguage       = userDefaults.StringForKey("instanceLanguage");
    }

    private ISitecoreWebApiSession GetAuthenticatedSession()
    {
      using 
        (
          var credentials = 
          new SecureStringPasswordProvider(
            this.InstanceLogin, 
            this.InstancePassword)
        )
      {
        var result = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(this.InstanceUrl)
          .Credentials(credentials)
          .Site(this.InstanceSite)
          .DefaultDatabase(this.InstanceDataBase)
          .DefaultLanguage(this.InstanceLanguage)
          .BuildSession();

        return result;
      }
    }

    private ISitecoreWebApiSession GetAnonymousSession()
    {
        var result = SitecoreWebApiSessionBuilder.AnonymousSessionWithHost(this.InstanceUrl)
          .Site(this.InstanceSite)
          .DefaultDatabase(this.InstanceDataBase)
          .DefaultLanguage(this.InstanceLanguage)
          .BuildSession();

        return result;
    }

    public ISitecoreWebApiSession GetSession()
    {
      bool isAnonymousSession = string.IsNullOrWhiteSpace(this.InstanceLogin);
      if (isAnonymousSession)
      {
        return this.GetAnonymousSession();
      }
      else
      {
        return this.GetAuthenticatedSession();
      }
    }

    private void SaveValueToStorage(string value, string key)
    {
      NSUserDefaults userDefaults = NSUserDefaults.StandardUserDefaults;
      userDefaults.SetString(value, key);
      userDefaults.Synchronize();
    }

    public string InstanceUrl   
    { 
      get
      { 
        if (this.instanceUrl == null) 
        {
          this.instanceUrl = "http://jetstream800394rev150402.test24dk1.dk.sitecore.net/";
        }

        return this.instanceUrl;
      }
      set
      { 
        this.instanceUrl = value;
       
        this.SaveValueToStorage(value, "instanceUrl");
      }
    }

    public string InstanceLogin   
    { 
      get
      { 
        if (this.instanceLogin == null) 
        {
          this.instanceLogin = "admin";
        }

        return this.instanceLogin;
      }
      set
      { 
        this.instanceLogin = value;
        this.SaveValueToStorage (value, "instanceLogin");
      } 
    }

    public string InstancePassword  
    { 
      get
      { 
        if (this.instancePassword == null) 
        {
          this.instancePassword = "b";
        }

        return this.instancePassword;
      } 
      set
      { 
        this.instancePassword = value;
        this.SaveValueToStorage(value, "instancePassword");
      } 
    }

    public string InstanceSite    
    { 
      get
      { 
        if (this.instanceSite == null) 
        {
          this.instanceSite = "/sitecore/shell";
        }

        return this.instanceSite;
      }
      set
      { 
        this.instanceSite = value;
        this.SaveValueToStorage(value, "instanceSite");
      } 
    }

    public string InstanceDataBase  
    { 
      get
      { 
        if (this.instanceDataBase == null) 
        {
          this.instanceDataBase = "master";
        }

        return this.instanceDataBase;
      }
      set
      { 
        this.instanceDataBase = value;
        this.SaveValueToStorage(value, "instanceDataBase");
      } 
    }

    public string InstanceLanguage  
    { 
      get
      { 
//        if (this.instanceLanguage == null) 
//        {
//          this.instanceLanguage = "en";
//        }
//
        return this.instanceLanguage;
      }
      set
      { 
        this.instanceLanguage = value;
        this.SaveValueToStorage(value, "instanceLanguage");
      } 
    }
  }
}
