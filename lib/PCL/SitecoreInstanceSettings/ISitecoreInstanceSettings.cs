namespace SitecoreInstanceSettings
{
  public interface ISitecoreInstanceSettings
  {
    ISitecoreInstanceSettings ShallowCopy();

    string InstanceUrl
    {
      get;
    }

    string InstanceSiteName
    {
      get;
    }

    string InstanceDatabase
    {
      get;
    }

    string InstanceLanguage
    {
      get;
    }
  }
}

