// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using JetStreamIOSFull.Helpers;

namespace JetStreamIOSFull
{
	public partial class HistoryTableCell : UITableViewCell
	{
		public HistoryTableCell (IntPtr handle) : base (handle)
		{
		}

    public void SetChecked(bool isChecked)
    {
      if (isChecked)
      {
        this.ImageView.Image = IconsHelper.SelectedInstanceIcon;
      }
      else
      {
        this.ImageView.Image = IconsHelper.UnselectedInstanceIcon;
      }
    }
	}
}