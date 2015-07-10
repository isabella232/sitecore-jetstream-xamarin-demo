using System;
using UIKit;

namespace JetStreamIOSFull.Menu
{
  public interface IMenuItem
  {
    string Title
    {
      get;
    }

    UIImage Image
    {
      get;
    }

    MenuItemTypes Type
    {
      get;
    }
  }
}

