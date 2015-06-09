namespace Jetstream.Utils
{
  public static class Util
  {
    public static string GetFixedUrl(string url)
    {
      if (url.StartsWith("http"))
      {
        return url;
      }

      return "http://" + url;
    }
  }
}