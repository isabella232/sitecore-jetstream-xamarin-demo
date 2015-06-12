namespace JetStreamCommons
{
  using Sitecore.MobileSDK;

  public static class SitecoreWebApiSessionExt
  {
    public static string MediaDownloadUrl(this ScApiSession session, string mediaItemPath)
    {
      var instanceUrl = session.Config.InstanceUrl;
      return SitecoreWebApiSessionExt.MediaDownloadUrl(instanceUrl, mediaItemPath);
    }

    private static string MediaDownloadUrl(string instanceUrl, string mediaItemPath)
    {
      if (!instanceUrl.StartsWith("http"))
      {
        instanceUrl = "http://" + instanceUrl;
      }

      string formatter = "";
      if (!instanceUrl.EndsWith("/"))
      {
        formatter = "/";
      }
      return string.Format("{0}{1}{2}", instanceUrl, formatter, mediaItemPath);
    }
  }
}
