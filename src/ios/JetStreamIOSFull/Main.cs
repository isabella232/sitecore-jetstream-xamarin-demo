using UIKit;

namespace JetStreamIOSFull
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
      Xamarin.Insights.Initialize("8148d1a71192f6c9ee9d584ca80713c566e6cb40");
      // if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}
