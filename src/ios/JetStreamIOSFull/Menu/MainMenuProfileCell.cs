using System;

using Foundation;
using UIKit;

namespace JetStreamIOSFull.Menu
{
  public partial class MainMenuProfileCell : MainMenuBaseCell
	{
		public MainMenuProfileCell (IntPtr handle) : base (handle)
		{
		}

    public override void SetSelected(bool selected, bool animated)
    {
      this.IconImageView.TintColor = this.defaultTintColor;
      this.TextLabel.TextColor = this.defaultTintColor;;
    }

    public override string Title
    {
      set
      { 
        this.TextLabel.Text = value;
      }
    }

    public override UIImage Image
    {
      set
      { 
        this.IconImageView.Image = value.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
      }
    }

    public UIImage BackgroundImage
    {
      set
      { 
        this.BackgroundImageView.Image = value;
      }
    }
	}
}
