using Foundation;
using System;

using Sitecore.MobileSDK.API;
using Sitecore.MobileSDK.API.Session;
using Newtonsoft.Json;
using Sitecore.MobileSDK.PasswordProvider;

namespace InstanceSettings
{

  public class InstanceSettings
  {
    [JsonProperty]
    private string instanceUrl;
    [JsonProperty]
    private string instanceLogin;
    [JsonProperty]
    private string instancePassword;
    [JsonProperty]
    private string instanceSite;
    [JsonProperty]
    private string instanceDataBase;
    [JsonProperty]
    private string instanceLanguage;

    public InstanceSettings()
    {
    }

    [JsonConstructor]
    public InstanceSettings(string instanceUrl, string instanceLogin, string instancePassword, string instanceSite, string instanceDataBase, string instanceLanguage)
    {
      this.instanceUrl = instanceUrl;
      this.instanceLogin = instanceLogin;
      this.instancePassword = instancePassword;
      this.instanceSite = instanceSite;
      this.instanceDataBase = instanceDataBase;
      this.instanceLanguage = instanceLanguage;
    }

    private ISitecoreSSCSession GetAuthenticatedSession()
    {
      using
        (
          var credentials =
           new ScUnsecuredCredentialsProvider(
            this.instanceLogin,
            this.instancePassword,
            "sitecore"
          )
        )
      {
        var result = SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost(this.instanceUrl)
          .Credentials(credentials)
          .DefaultDatabase(this.InstanceDataBase)
          .DefaultLanguage(this.InstanceLanguage)
          .BuildSession();
        
        return result;
      }
    }

    public override bool Equals(System.Object obj)
    {
      if (obj is InstanceSettings)
      {
        InstanceSettings castedObj = obj as InstanceSettings;
        bool result = this.InstanceUrl == castedObj.InstanceUrl;
        result = result && this.InstanceLogin    == castedObj.InstanceLogin;
        result = result && this.InstancePassword == castedObj.InstancePassword;
        result = result && this.InstanceSite     == castedObj.InstanceSite;
        result = result && this.InstanceDataBase == castedObj.InstanceDataBase;
        result = result && this.InstanceLanguage == castedObj.InstanceLanguage;

        return result;
      }

      return false;
    }

    public override int GetHashCode()
    {
      int hash = this.InstanceUrl.GetHashCode();
      hash += this.InstanceLogin.GetHashCode();
      hash += this.InstancePassword.GetHashCode();
      hash += this.InstanceSite.GetHashCode();
      hash += this.InstanceDataBase.GetHashCode();
      hash += this.InstanceLanguage.GetHashCode();

      return hash;
    }

    private ISitecoreSSCSession GetAnonymousSession()
    {
        var result = SitecoreSSCSessionBuilder.AnonymousSessionWithHost(this.InstanceUrl)
          .DefaultDatabase(this.InstanceDataBase)
          .DefaultLanguage(this.InstanceLanguage)
          .BuildSession();

        return result;
    }

    public ISitecoreSSCSession GetSession()
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
      
    public string InstanceUrl   
    { 
      get
      { 
        if (this.instanceUrl == null) 
        {
          this.instanceUrl = "";
        }

        return this.instanceUrl;
      }
      set
      { 
        this.instanceUrl = value;
      }
    }

    public string InstanceLogin   
    { 
      get
      { 
        if (this.instanceLogin == null) 
        {
          this.instanceLogin = "";
        }

        return this.instanceLogin;
      }
      set
      { 
        this.instanceLogin = value;
      } 
    }

    public string InstancePassword  
    { 
      get
      { 
        if (this.instancePassword == null) 
        {
          this.instancePassword = "";
        }

        return this.instancePassword;
      } 
      set
      { 
        this.instancePassword = value;
      } 
    }

    public string InstanceSite    
    { 
      get
      { 
        if (this.instanceSite == null || this.instanceSite == "") 
        {
          this.instanceSite = "sitecore";
        }

        return this.instanceSite;
      }
      set
      { 
        this.instanceSite = value;
      } 
    }

    public string InstanceDataBase  
    { 
      get
      { 
        if (this.instanceDataBase == null || this.instanceDataBase == "") 
        {
          this.instanceDataBase = "master";
        }

        return this.instanceDataBase;
      }
      set
      { 
        this.instanceDataBase = value;
      } 
    }

    public string InstanceLanguage  
    { 
      get
      { 
        if (this.instanceLanguage == null || this.instanceLanguage == "") 
        {
          this.instanceLanguage = "en";
        }

        return this.instanceLanguage;
      }
      set
      { 
        this.instanceLanguage = value;
      } 
    }
  }
}
