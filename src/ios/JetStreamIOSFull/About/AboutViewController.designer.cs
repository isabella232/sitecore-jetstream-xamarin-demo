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
	[Register ("AboutViewController")]
	partial class AboutViewController
	{
		[Outlet]
		UIKit.UITextView MaainTextField { get; set; }

		[Outlet]
		UIKit.UILabel TitleLabel { get; set; }

		[Outlet]
		UIKit.UIImageView TopImageView { get; set; }

		[Outlet]
		UIKit.UILabel WelcomeLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TopImageView != null) {
				TopImageView.Dispose ();
				TopImageView = null;
			}

			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}

			if (WelcomeLabel != null) {
				WelcomeLabel.Dispose ();
				WelcomeLabel = null;
			}

			if (MaainTextField != null) {
				MaainTextField.Dispose ();
				MaainTextField = null;
			}
		}
	}
}
