using System;
using System.Threading.Tasks;
using UIKit;
using Foundation;

namespace JetStreamIOSFull.Helpers
{
	public class AlertHelper
	{
		public AlertHelper()
		{
		}

    private static void ShowAlertWithOkOption(string title, string message)
		{
			AlertHelper.ShowAlertWithSingleButton(title, message, "OK");
		}

    private static void ShowAlertWithSingleButton(string title, string message, string buttonTitle)
		{
			UIAlertView alert = new UIAlertView() 
			{ 
				Title = title, 
				Message = message
			};

			alert.AddButton(buttonTitle);
			alert.Show();
		}

		public static void ShowLocalizedAlertWithOkOption(string title, string message)
		{
			string localizedTitle 		  = NSBundle.MainBundle.LocalizedString(title, null);
			string localizedMessage 	  = NSBundle.MainBundle.LocalizedString(message, null);
			string localizedButtonTitle = NSBundle.MainBundle.LocalizedString("OK", null);

			AlertHelper.ShowAlertWithSingleButton(localizedTitle, localizedMessage, localizedButtonTitle);
		}

		public static void ShowLocalizedNotImlementedAlert()
		{
			AlertHelper.ShowLocalizedAlertWithOkOption("Alert", "Not implemented yet");
		}
      
	}
}

