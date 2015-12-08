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

namespace JetStreamIOSFull.Navigation
{
	[Register ("NavigationManagerViewController")]
	partial class NavigationManagerViewController
	{
		[Outlet]
		UIKit.UIView PanView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (PanView != null) {
				PanView.Dispose ();
				PanView = null;
			}
		}
	}
}
