// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

namespace JetStreamIOSFull
{
	public partial class MainMenuCell : UITableViewCell
	{
    private UIColor defaultTintColor = UIColor.Blue;
    private UIColor selectedTintColor = UIColor.White;
    private bool selected = false;
		public MainMenuCell (IntPtr handle) : base (handle)
		{
		}

    public UIColor SelectedTintColor
    {
      set
      { 
        this.selectedTintColor = value;
        this.SetSelected(this.selected, true);
      }
    }

    public UIColor DefaultTintColor
    {
      set
      { 
        this.defaultTintColor = value;
        this.SetSelected(this.selected, true);
      }
    }

    public bool IsSelected
    {
      get
      { 
        return this.selected;
      }
    }

    public virtual void SetSelected(bool selected, bool animated)
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

    public string Title
    {
      set
      { 
        this.TitleLabel.Text = value;
      }
    }

    public UIImage Image
    {
      set
      { 
        this.ImageView.Image = value.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
      }
    }
	}
}
