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
	[Register ("AboutViewController")]
	partial class AboutViewController
	{
		[Outlet]
		MonoTouch.UIKit.UITextView AboutTextContainer { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel ViewControllerNameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AboutTextContainer != null) {
				AboutTextContainer.Dispose ();
				AboutTextContainer = null;
			}

			if (ViewControllerNameLabel != null) {
				ViewControllerNameLabel.Dispose ();
				ViewControllerNameLabel = null;
			}
		}
	}
}
