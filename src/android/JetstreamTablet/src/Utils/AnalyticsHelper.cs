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
      Insights.Initialize("8148d1a71192f6c9ee9d584ca80713c566e6cb40", context);
      #endif
    }
  }
}

