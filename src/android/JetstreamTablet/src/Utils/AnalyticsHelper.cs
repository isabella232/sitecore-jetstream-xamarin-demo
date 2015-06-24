using Xamarin;
using System.Collections.Generic;
using Android.Content;

namespace Jetstream.Utils
{
  public static class AnalyticsHelper
  {
    public static void TrackRefreshButtonTouch()
    {
      Insights.Track("RefreshButtonTouched", new Dictionary <string,string>
        {
          { "Event", "TotalCount" },
          { "OS", "Android" }
        });
    }

    public static void TrackUrlChanged()
    {
      Insights.Track("NewUrlUsed", new Dictionary <string,string>
        {
          { "Event", "TotalCount" },
          { "OS", "Android" }
        });
    }

    public static void InitializeAnalytics(Context context)
    {
      #if DEBUG
      Insights.Initialize(Insights.DebugModeKey, context);
      #else
      Insights.Initialize("e61045d82e2d0a6adcbe404ce51e1fbc14efddb0", context);
      #endif
    }
  }
}

