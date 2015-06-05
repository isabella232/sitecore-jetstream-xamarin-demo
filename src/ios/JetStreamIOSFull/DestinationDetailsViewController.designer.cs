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
	[Register ("DestinationDetailsViewController")]
	partial class DestinationDetailsViewController
	{
		[Outlet]
		UIKit.UITextView DestinationDescription { get; set; }

		[Outlet]
		UIKit.UIImageView DestinationImageView { get; set; }

		[Outlet]
		UIKit.UILabel TitleLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}

			if (DestinationDescription != null) {
				DestinationDescription.Dispose ();
				DestinationDescription = null;
			}

			if (DestinationImageView != null) {
				DestinationImageView.Dispose ();
				DestinationImageView = null;
			}
		}
	}
}
