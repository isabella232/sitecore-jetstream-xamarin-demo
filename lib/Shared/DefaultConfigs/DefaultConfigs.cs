
namespace DefaultConfigsStorage
{
  using SitecoreInstanceSettings;

  public static class DefaultConfigs
  {
    public static ISitecoreInstanceSettings GetInstanceConfig()
    {
      InstanceConfig instanceConfig = new InstanceConfig
      {
        InstanceUrl = InstanceUrl,
        InstanceSiteName = InstanceSiteName,
        InstanceDatabase = InstanceDatabase,
        InstanceLanguage = InstanceLanguage
      };

      return instanceConfig;
    }

    public static BrochuresQuerryBuilder GetDefaultBrochuresQuerryBuilder()
    {
      
      string template = BrochuresTemplate;
      string path = PathToBrochures;
      BrochuresQuerryBuilder querryBuilder = new BrochuresQuerryBuilder(path, template);

      return querryBuilder;
    }

    public static string InstanceUrl = "http://brdemo.test24dk1.dk.sitecore.net/";

    public static string InstanceSiteName = "/sitecore/shell";

    public static string InstanceDatabase = "web";

    public static string InstanceLanguage = "en";

    public static string PathToBrochures = "/sitecore/media library/Symposium";

    public static string BrochuresTemplate = "Rendition Builder Package";
  }
}

