using System;
using UIKit;

namespace JetStreamIOSFull
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

