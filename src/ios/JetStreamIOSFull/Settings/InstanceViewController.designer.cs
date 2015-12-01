// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace JetStreamIOSFull
{
	[Register ("InstanceViewController")]
	partial class InstanceViewController
	{
		[Outlet]
		UIKit.UIButton ApplyButton { get; set; }

		[Outlet]
		UIKit.UIButton CancelButton { get; set; }

		[Outlet]
		UIKit.UILabel DatabaseLabel { get; set; }

		[Outlet]
		UIKit.UITextField DatabaseTextField { get; set; }

		[Outlet]
		UIKit.UILabel InstanceUrlLabel { get; set; }

		[Outlet]
		UIKit.UITextField InstanceUrlTextField { get; set; }

		[Outlet]
		UIKit.UILabel LanguageLabel { get; set; }

		[Outlet]
		UIKit.UITextField LanguageTextField { get; set; }

		[Outlet]
		UIKit.UILabel LoginLabel { get; set; }

		[Outlet]
		UIKit.UITextField LoginTextField { get; set; }

		[Outlet]
		UIKit.UILabel PasswordLabel { get; set; }

		[Outlet]
		UIKit.UITextField PasswordTextField { get; set; }

		[Outlet]
		UIKit.UIScrollView ScrollView { get; set; }

		[Action ("ApplyButtonTouched:")]
		partial void ApplyButtonTouched (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ScrollView != null) {
				ScrollView.Dispose ();
				ScrollView = null;
			}

			if (ApplyButton != null) {
				ApplyButton.Dispose ();
				ApplyButton = null;
			}

			if (CancelButton != null) {
				CancelButton.Dispose ();
				CancelButton = null;
			}

			if (DatabaseLabel != null) {
				DatabaseLabel.Dispose ();
				DatabaseLabel = null;
			}

			if (DatabaseTextField != null) {
				DatabaseTextField.Dispose ();
				DatabaseTextField = null;
			}

			if (InstanceUrlLabel != null) {
				InstanceUrlLabel.Dispose ();
				InstanceUrlLabel = null;
			}

			if (InstanceUrlTextField != null) {
				InstanceUrlTextField.Dispose ();
				InstanceUrlTextField = null;
			}

			if (LanguageLabel != null) {
				LanguageLabel.Dispose ();
				LanguageLabel = null;
			}

			if (LanguageTextField != null) {
				LanguageTextField.Dispose ();
				LanguageTextField = null;
			}

			if (LoginLabel != null) {
				LoginLabel.Dispose ();
				LoginLabel = null;
			}

			if (LoginTextField != null) {
				LoginTextField.Dispose ();
				LoginTextField = null;
			}

			if (PasswordLabel != null) {
				PasswordLabel.Dispose ();
				PasswordLabel = null;
			}

			if (PasswordTextField != null) {
				PasswordTextField.Dispose ();
				PasswordTextField = null;
			}
		}
	}
}
