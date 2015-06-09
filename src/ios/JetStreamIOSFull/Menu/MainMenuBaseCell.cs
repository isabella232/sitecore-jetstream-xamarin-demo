using System;
using UIKit;

namespace JetStreamIOSFull
{
  public class MainMenuBaseCell : UITableViewCell
  {
    protected UIColor defaultTintColor = UIColor.Blue;
    protected UIColor selectedTintColor = UIColor.White;
    protected bool selected = false;

    public MainMenuBaseCell (IntPtr handle) : base (handle)
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
      throw new NotImplementedException("Must be implemented");
    }

    public virtual string Title
    {
      set
      { 
        throw new NotImplementedException("Must be implemented");
      }
    }

    public virtual UIImage Image
    {
      set
      { 
        throw new NotImplementedException("Must be implemented");
      }
    }
  }
}

