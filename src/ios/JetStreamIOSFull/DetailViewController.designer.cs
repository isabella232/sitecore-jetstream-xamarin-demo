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
		UIKit.UICollectionView countriesCarousel { get; set; }

		[Outlet]
		UIKit.UILabel detailDescriptionLabel { get; set; }

		[Outlet]
		MapKit.MKMapView map { get; set; }

		[Outlet]
		UIKit.UIToolbar toolbar { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (detailDescriptionLabel != null) {
				detailDescriptionLabel.Dispose ();
				detailDescriptionLabel = null;
			}

			if (map != null) {
				map.Dispose ();
				map = null;
			}

			if (toolbar != null) {
				toolbar.Dispose ();
				toolbar = null;
			}

			if (countriesCarousel != null) {
				countriesCarousel.Dispose ();
				countriesCarousel = null;
			}
		}
	}
}
