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
      Insights.Initialize("e61045d82e2d0a6adcbe404ce51e1fbc14efddb0");
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

