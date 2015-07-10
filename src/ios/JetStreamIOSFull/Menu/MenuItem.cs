using System;
using UIKit;

namespace JetStreamIOSFull.Menu
{
  public class MenuItem : IMenuItem
  {
    private string title;
    private UIImage image;
    private MenuItemTypes type;

    public MenuItem(string title, UIImage image, 
      MenuItemTypes type)
    {
      this.title = title;
      this.image = image;
      this.type = type;
    }

    public MenuItemTypes Type
    {
      get 
      {
        return this.type;
      }
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

