// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SitecoreInstanceSettingsIOS
{
	[Register ("SettingsViewController")]
	partial class SettingsViewController
	{
		[Outlet]
		public UIKit.UILabel AnonymousLabel { get; private set; }

		[Outlet]
		public UIKit.UISwitch AnonymousSwitch { get; private set; }

		[Outlet]
    public UIKit.UIButton CancelButton { get; set; }

		[Outlet]
		public UIKit.UILabel DatabaseLabel { get; private set; }

		[Outlet]
		public UIKit.UITextField DatabaseTextField { get; private set; }

		[Outlet]
    public UIKit.UIButton DoneButton { get; set; }

		[Outlet]
		public UIKit.UILabel InstanceUrlLabel { get; private set; }

		[Outlet]
		public UIKit.UITextField InstanceUrlTextField { get; private set; }

		[Outlet]
		public UIKit.UILabel LanguageLabel { get; private set; }

		[Outlet]
		public UIKit.UITextField LanguageTextField { get; private set; }

		[Outlet]
		public UIKit.UILabel LoginLabel { get; private set; }

		[Outlet]
		public UIKit.UITextField LoginTextField { get; private set; }

		[Outlet]
		public UIKit.UILabel PasswordLabel { get; private set; }

		[Outlet]
		public UIKit.UITextField PasswordTextField { get; private set; }

		[Outlet]
		public UIKit.UILabel PathToItemsLabel { get; private set; }

		[Outlet]
		public UIKit.UITextField PathToItemsTextField { get; private set; }

		[Outlet]
		public UIKit.UILabel SiteLabel { get; private set; }

		[Outlet]
		public UIKit.UITextField SiteTextField { get; private set; }

		[Action ("AnonymousSwitchValueChanged:")]
		partial void AnonymousSwitchValueChanged (UIKit.UISwitch sender);

		[Action ("CancelButtonTouched:")]
		partial void CancelButtonTouched (UIKit.UIButton sender);

		[Action ("DoneButtonTouched:")]
		partial void DoneButtonTouched (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (AnonymousLabel != null) {
				AnonymousLabel.Dispose ();
				AnonymousLabel = null;
			}

			if (AnonymousSwitch != null) {
				AnonymousSwitch.Dispose ();
				AnonymousSwitch = null;
			}

			if (DatabaseLabel != null) {
				DatabaseLabel.Dispose ();
				DatabaseLabel = null;
			}

			if (DatabaseTextField != null) {
				DatabaseTextField.Dispose ();
				DatabaseTextField = null;
			}

			if (DoneButton != null) {
				DoneButton.Dispose ();
				DoneButton = null;
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

			if (PathToItemsLabel != null) {
				PathToItemsLabel.Dispose ();
				PathToItemsLabel = null;
			}

			if (PathToItemsTextField != null) {
				PathToItemsTextField.Dispose ();
				PathToItemsTextField = null;
			}

			if (SiteLabel != null) {
				SiteLabel.Dispose ();
				SiteLabel = null;
			}

			if (SiteTextField != null) {
				SiteTextField.Dispose ();
				SiteTextField = null;
			}

			if (CancelButton != null) {
				CancelButton.Dispose ();
				CancelButton = null;
			}
		}
	}
}
