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
	[Register ("DestinationDetailsViewController")]
	partial class DestinationDetailsViewController
	{
		[Outlet]
		UIKit.UIButton BackButton { get; set; }

		[Outlet]
		UIKit.UITableView ImagesTableView { get; set; }

		[Outlet]
		UIKit.UILabel TitleLabel { get; set; }

		[Outlet]
		UIKit.UIImageView TopBarImage { get; set; }

		[Outlet]
		UIKit.UIWebView WebDestinationDescroption { get; set; }

		[Action ("BackgroundViewTouched:")]
		partial void BackgroundViewTouched (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (BackButton != null) {
				BackButton.Dispose ();
				BackButton = null;
			}

			if (WebDestinationDescroption != null) {
				WebDestinationDescroption.Dispose ();
				WebDestinationDescroption = null;
			}

			if (ImagesTableView != null) {
				ImagesTableView.Dispose ();
				ImagesTableView = null;
			}

			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}

			if (TopBarImage != null) {
				TopBarImage.Dispose ();
				TopBarImage = null;
			}
		}
	}
}
