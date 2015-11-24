namespace SitecoreInstanceSettings
{
  public class InstanceConfig : ISitecoreInstanceSettings
  {
    private string instanceUrl;
    private string instanceSite;
    private string instanceDataBase;
    private string instanceLanguage;

    public InstanceConfig()
    {
    }

    public ISitecoreInstanceSettings ShallowCopy()
    {
      InstanceConfig newConfig = new InstanceConfig ();

      newConfig.InstanceUrl = this.instanceUrl;
      newConfig.InstanceSiteName = this.instanceSite;
      newConfig.InstanceDatabase = this.instanceDataBase;
      newConfig.InstanceLanguage = this.instanceLanguage;

      return newConfig;
    }

    public string InstanceUrl
    {
      set
      {
        this.instanceUrl = value;
      }
      get
      {
        if (this.instanceUrl == null)
        {
          return null; //no default value
        }
        else
        {
          return this.instanceUrl;
        }
      }
    }

    public string InstanceSiteName
    {
      set
      {
        this.instanceSite = value;
      }
      get
      {
        if (this.instanceSite == null)
        {
          return "/sitecore/shell"; //default value
        }
        else
        {
          return this.instanceSite;
        }
      }
    }

    public string InstanceDatabase
    {
      set
      {
        this.instanceDataBase = value;
      }
      get
      {
        if (this.instanceDataBase == null)
        {
          return "master"; //default value
        }
        else
        {
          return this.instanceDataBase;
        }
      }
    }

    public string InstanceLanguage
    {
      set
      {
        this.instanceLanguage = value;
      }
      get
      {
        if (this.instanceLanguage == null)
        {
          return "en"; //default value
        }
        else
        {
          return this.instanceLanguage;
        }
      }
    }
  }
}

