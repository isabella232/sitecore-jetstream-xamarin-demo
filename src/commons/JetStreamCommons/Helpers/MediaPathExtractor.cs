using System;

namespace JetStreamCommons
{
  public static class MediaPathExtractor
  {
    public static string GetImagePathFromImageRawValue(string rawValue)
    {
      //<image src="~/media/605015FD900C488AAD0E8F3762F42A43.ashx" mediaid="{605015FD-900C-488A-AD0E-8F3762F42A43}" mediapath="/Images/Imported/8/0/b/6/a_tokyooverviewFEB09" />
     
      //TODO: igk magik numbers, must be ok, but possible error
      return rawValue.Substring(12, 45);
    }
  }
}

