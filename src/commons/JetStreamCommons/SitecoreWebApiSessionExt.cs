namespace JetStreamCommons
{
  using Sitecore.MobileSDK;

  public static class SitecoreWebApiSessionExt
  {
    public static string MediaDownloadUrl(this ScApiSession session, string mediaItemPath)
    {
      string instanceUrl = session.Config.InstanceUrl;
      return SitecoreWebApiSessionExt.MediaDownloadUrl(instanceUrl, mediaItemPath);
    }

    public static string MediaDownloadUrl(string instanceUrl, string mediaItemPath)
    {
      string formatter = "";
      if (!instanceUrl.EndsWith("/"))
      {
        formatter = "/";
      }
      return string.Format("{0}{1}{2}", instanceUrl, formatter, mediaItemPath);
    }

  }
}
