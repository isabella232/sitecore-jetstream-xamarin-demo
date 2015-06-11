using System;
using UIKit;
using System.Collections.Generic;
using JetStreamCommons.Destinations;
using Foundation;
using JetStreamIOSFull.MapUI;

namespace JetStreamIOSFull
{
  public class CarouselDataSource : UICollectionViewSource
  {
    private const string CELL_ID = "DestinationCell";
    List<DestinationAnnotation> destinations;

    public CarouselDataSource(List<DestinationAnnotation> destinations)
    {
      this.destinations = destinations;
    }

    public override nint NumberOfSections(UICollectionView collectionView)
    {
      return 1;
    }

    public override nint GetItemsCount(UICollectionView collectionView, nint section)
    {
      return destinations.Count;
    }

    public override Boolean ShouldHighlightItem(UICollectionView collectionView, NSIndexPath indexPath)
    {
      return true;
    }

    public override void ItemHighlighted(UICollectionView collectionView, NSIndexPath indexPath)
    {
//      DestinationCarouselCell cell = (DestinationCarouselCell)collectionView.CellForItem(indexPath);
//      cell.ImageView.Alpha = 0.5f;
    }

    public override void ItemUnhighlighted(UICollectionView collectionView, NSIndexPath indexPath)
    {
//      var cell = (UserCell) collectionView.CellForItem(indexPath);
//      cell.ImageView.Alpha = 1;
//
//      UserElement row = Rows[indexPath.Row];
//      row.Tapped.Invoke();
    }

    public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
    {
      DestinationCarouselCell cell = (DestinationCarouselCell)collectionView.DequeueReusableCell(CELL_ID, indexPath);

      DestinationAnnotation destination = this.destinations[indexPath.Row];

      cell.FillWithDestination(destination);

      return cell;
    }
  }

}

