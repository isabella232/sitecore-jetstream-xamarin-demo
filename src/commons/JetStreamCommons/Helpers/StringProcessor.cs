using System.Text.RegularExpressions;

namespace JetStreamCommons
{
  public static class StringProcessor
  {
    const string HTML_TAG_PATTERN = "<.*?>";

    public static string PrimitiveHTMLTagsRemove(string sourceText)
    {
      return Regex.Replace 
        (sourceText, HTML_TAG_PATTERN, string.Empty);
    }
  }
}

