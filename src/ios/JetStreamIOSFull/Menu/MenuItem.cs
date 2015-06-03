using System;
using UIKit;

namespace JetStreamIOSFull
{
  public class MenuItem : IMenuItem
  {
    private string title;
    private UIImage image;

    public MenuItem(string title, UIImage image)
    {
      this.title = title;
      this.image = image;
    }

    public string Title
    {
      get
      { 
        if (this.title == null)
        {
          throw new NullReferenceException ("Menu item title must not be null");
        }

        return this.title;
      }
    }

    public UIImage Image
    {
      get
      { 
        if (this.image == null)
        {
          throw new NullReferenceException ("Menu item image must not be null");
        }

        return this.image;
      }
    }
  }
}

