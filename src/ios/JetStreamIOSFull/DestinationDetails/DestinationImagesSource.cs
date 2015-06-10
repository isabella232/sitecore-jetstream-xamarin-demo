using System;
using UIKit;
using Foundation;
using System.Collections.Generic;
using JetStreamCommons.Destinations;
using SDWebImage;

namespace JetStreamIOSFull
{
  public class DestinationImagesSource : UITableViewSource
  {
    List<IAttraction> tableItems;
    string CellIdentifier = "DestinationImageCellId";
    private InstanceSettings.InstanceSettings endpoint;

    public DestinationImagesSource(List<IAttraction> items, InstanceSettings.InstanceSettings endpoint)
    {
      this.endpoint = endpoint;
      this.tableItems = items;
    }

    public override nint RowsInSection (UITableView tableview, nint section)
    {
      return this.tableItems.Count;
    }

    public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
    {
      DestinationImageCell cell = tableView.DequeueReusableCell(CellIdentifier) as DestinationImageCell;
      IAttraction item = this.tableItems[indexPath.Row];

      cell.SetImage(null);
      cell.ActivityIndicator.StartAnimating();

      this.DownloadAndShowDestinationImage(item, tableView, indexPath);

      return cell;
    }

    private void DownloadAndShowDestinationImage(IAttraction attraction, UITableView tableView, NSIndexPath indexPath)
    {
      string imagePath = null;

      try
      {
        imagePath = String.Concat(this.endpoint.InstanceUrl, attraction.ImagePath);

        NSUrl imageUrl = new NSUrl(imagePath);

        SDWebImageDownloader.SharedDownloader.DownloadImage(
          url: imageUrl,
          options: SDWebImageDownloaderOptions.LowPriority,
          progressHandler: (receivedSize, expectedSize) =>
        {
          // Track progress...
        },
          completedHandler: (image, data, error, finished) =>
        {
          if (image != null)
          {
            InvokeOnMainThread(() =>
            {
              DestinationImageCell cell = tableView.CellAt(indexPath) as DestinationImageCell;

              if (cell != null)
              {
                NSIndexPath[] visiblePaths = tableView.IndexPathsForVisibleRows;
                for (int i = 0; i < visiblePaths.Length; ++i)
                {
                  if (visiblePaths[i].Row == indexPath.Row)
                  {
                    cell.ActivityIndicator.StopAnimating();
                    cell.SetImage(image);
                    break;
                  }
                }
              }

            });
          }
        }
        );
      }
      catch(Exception ex)
      {
        Console.WriteLine("Can not downloaad image: " + ex.ToString());
        Console.WriteLine("Image path: " + imagePath);
      }
    }
  }
}

