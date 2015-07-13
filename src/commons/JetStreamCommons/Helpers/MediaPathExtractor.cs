using System;

namespace JetStreamCommons
{
  public static class MediaPathExtractor
  {
    public static string GetImagePathFromImageRawValue(string rawValue)
    {
      //<image src="~/media/605015FD900C488AAD0E8F3762F42A43.ashx" mediaid="{605015FD-900C-488A-AD0E-8F3762F42A43}" mediapath="/Images/Imported/8/0/b/6/a_tokyooverviewFEB09" />
      //<image mediaid="{903A0864-B02F-44CD-9DEB-45CAF0FE08A3}" mediapath="/Images/Planes/14061" src="~/media/903A0864B02F44CD9DEB45CAF0FE08A3.ashx" />
      //<image mediaid="{605015FD-900C-488A-AD0E-8F3762F42A43}"/>

      string prefixToSearch = "mediaid=\"";
      int idLenght = 38;

      string pathPrefix = "~/media/";
      string pathPostfix = ".ashx";

      int srcIndex = rawValue.IndexOf(prefixToSearch) + prefixToSearch.Length;
      int finishIndex = srcIndex+idLenght;
      int mediaPathLenght = finishIndex - srcIndex;

      string mediaId = rawValue.Substring(srcIndex, mediaPathLenght);
      mediaId = mediaId.Replace("{","");
      mediaId = mediaId.Replace("}","");
      mediaId = mediaId.Replace("-","");

      return pathPrefix+mediaId+pathPostfix;
    }
  }
}

