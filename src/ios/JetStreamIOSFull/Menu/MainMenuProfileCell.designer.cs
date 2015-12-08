// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace JetStreamIOSFull.Menu
{
	[Register ("MainMenuProfileCell")]
	partial class MainMenuProfileCell
	{
		[Outlet]
		UIKit.UIImageView BackgroundImageView { get; set; }

		[Outlet]
		UIKit.UIImageView IconImageView { get; set; }

		[Outlet]
		UIKit.UILabel TextLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (BackgroundImageView != null) {
				BackgroundImageView.Dispose ();
				BackgroundImageView = null;
			}
			if (IconImageView != null) {
				IconImageView.Dispose ();
				IconImageView = null;
			}
			if (TextLabel != null) {
				TextLabel.Dispose ();
				TextLabel = null;
			}
		}
	}
}
