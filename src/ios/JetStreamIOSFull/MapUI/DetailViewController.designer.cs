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
	[Register ("DetailViewController")]
	partial class DetailViewController
	{
		[Outlet]
		UIKit.UICollectionView DetailsCarousel { get; set; }

		[Outlet]
		MapKit.MKMapView map { get; set; }

		[Action ("RefreshButtonTouched:")]
		partial void RefreshButtonTouched (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (DetailsCarousel != null) {
				DetailsCarousel.Dispose ();
				DetailsCarousel = null;
			}

			if (map != null) {
				map.Dispose ();
				map = null;
			}
		}
	}
}
