using System;

using Foundation;
using UIKit;

namespace JetStreamIOSFull.Menu
{
  public partial class MainMenuCell : MainMenuBaseCell
	{
		public MainMenuCell (IntPtr handle) : base (handle)
		{
		}

    public override void SetSelected(bool selected, bool animated)
    {
      this.selected = selected;
      UIColor color = this.defaultTintColor;
      if (this.selected)
      {
        color = this.selectedTintColor;
      }

      this.ImageView.TintColor = color;
      this.TitleLabel.TextColor = color;
    }

    public override string Title
    {
      set
      { 
        this.TitleLabel.Text = value;
      }
    }

    public override UIImage Image
    {
      set
      { 
        this.ImageView.Image = value.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
      }
    }
	}
}
