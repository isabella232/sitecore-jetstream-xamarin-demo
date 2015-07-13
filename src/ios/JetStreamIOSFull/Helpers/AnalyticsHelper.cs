using System;
using Xamarin;
using System.Collections.Generic;

namespace JetStreamIOSFull.Helpers
{
  public static class AnalyticsHelper
  {
    public static void InitializeAnalytics()
    {
      #if DEBUG
      Insights.Initialize(Insights.DebugModeKey);
      #else
      Insights.Initialize("8148d1a71192f6c9ee9d584ca80713c566e6cb40");
      #endif
    }

    public static void TrackRefreshButtonTouch()
    {
      Insights.Track("RefreshButtonTouched", new Dictionary <string,string>{
        {"Event", "TotalCount"},
        {"OS", "iOS"}
      });
    }

    public static void TrackUrlChanged()
    {
      Insights.Track("NewUrlUsed", new Dictionary <string,string>{
        {"Event", "TotalCount"},
        {"OS", "iOS"}
      });
    }
  }
}

