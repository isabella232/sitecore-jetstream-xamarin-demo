namespace JetStreamCommons
{
  using Sitecore.MobileSDK;

  public static class SitecoreWebApiSessionExt
  {
    public static string MediaDownloadUrl(this ScApiSession session, string mediaItemPath)
    {
      var instanceUrl = session.Config.InstanceUrl;
      if (!instanceUrl.EndsWith("/"))
      {
        instanceUrl += "/";
      }
      return instanceUrl + mediaItemPath;
    }
  }
}
