using System;

namespace JetStreamCommons
{
  public static class MediaPathExtractor
  {
    public static string GetImagePathFromImageRawValue(string rawValue)
    {
      //<image src="~/media/605015FD900C488AAD0E8F3762F42A43.ashx" mediaid="{605015FD-900C-488A-AD0E-8F3762F42A43}" mediapath="/Images/Imported/8/0/b/6/a_tokyooverviewFEB09" />
      //<image mediaid="{903A0864-B02F-44CD-9DEB-45CAF0FE08A3}" mediapath="/Images/Planes/14061" src="~/media/903A0864B02F44CD9DEB45CAF0FE08A3.ashx" />

      string prefixToSearch = "src=\"";
      string postfixToSearch = ".ashx";

      int srcIndex = rawValue.IndexOf(prefixToSearch) + prefixToSearch.Length;
      int finishIndex = rawValue.IndexOf(postfixToSearch) + postfixToSearch.Length;
      int mediaPathLenght = finishIndex - srcIndex;

      //TODO: igk magik numbers, must be ok, but possible error
      return rawValue.Substring(srcIndex, mediaPathLenght);
    }
  }
}

