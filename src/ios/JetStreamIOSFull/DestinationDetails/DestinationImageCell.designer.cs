// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace JetStreamIOSFull.DestinationDetails
{
	[Register ("DestinationImageCell")]
	partial class DestinationImageCell
	{
		[Outlet]
		UIKit.UIImageView DestinationImage { get; set; }

		[Outlet]
		UIKit.UIActivityIndicatorView LoadingIndicator { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DestinationImage != null) {
				DestinationImage.Dispose ();
				DestinationImage = null;
			}

			if (LoadingIndicator != null) {
				LoadingIndicator.Dispose ();
				LoadingIndicator = null;
			}
		}
	}
}
