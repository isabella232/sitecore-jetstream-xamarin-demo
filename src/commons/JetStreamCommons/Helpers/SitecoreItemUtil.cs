namespace JetStreamCommons.Helpers
{
  public class SitecoreItemUtil
  {
    public static string ParseParentItemName(string itemPath)
    {
      var pathElements = itemPath.Split('/');
      return pathElements[pathElements.Length - 2];
    }
  }
}