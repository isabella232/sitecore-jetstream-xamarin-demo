// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace JetStreamIOSFull.About
{
	[Register ("AboutViewController")]
	partial class AboutViewController
	{
		[Outlet]
		UIKit.UITextView MainTextField { get; set; }

		[Outlet]
		UIKit.UILabel TitleLabel { get; set; }

		[Outlet]
		UIKit.UIImageView TopImageView { get; set; }

		[Outlet]
		UIKit.UILabel WelcomeLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (MainTextField != null) {
				MainTextField.Dispose ();
				MainTextField = null;
			}

			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}

			if (TopImageView != null) {
				TopImageView.Dispose ();
				TopImageView = null;
			}

			if (WelcomeLabel != null) {
				WelcomeLabel.Dispose ();
				WelcomeLabel = null;
			}
		}
	}
}
