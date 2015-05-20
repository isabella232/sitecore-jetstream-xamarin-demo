// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace JetStreamIOS
{
	[Register ("SettingsViewController")]
	partial class SettingsViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton CheckConnectionButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField DatabaseField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField InstanceUrlField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField LanguageField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField LoginField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField PasswordField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField SiteField { get; set; }

		[Action ("OnCheckConnectionButtonTapped:")]
    partial void OnCheckConnectionButtonTapped (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (CheckConnectionButton != null) {
				CheckConnectionButton.Dispose ();
				CheckConnectionButton = null;
			}

			if (DatabaseField != null) {
				DatabaseField.Dispose ();
				DatabaseField = null;
			}

			if (InstanceUrlField != null) {
				InstanceUrlField.Dispose ();
				InstanceUrlField = null;
			}

			if (LanguageField != null) {
				LanguageField.Dispose ();
				LanguageField = null;
			}

			if (LoginField != null) {
				LoginField.Dispose ();
				LoginField = null;
			}

			if (PasswordField != null) {
				PasswordField.Dispose ();
				PasswordField = null;
			}

			if (SiteField != null) {
				SiteField.Dispose ();
				SiteField = null;
			}
		}
	}
}
