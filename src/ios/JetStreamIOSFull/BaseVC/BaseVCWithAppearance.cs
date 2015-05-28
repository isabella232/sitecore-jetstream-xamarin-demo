using System;
using UIKit;
using JetStreamIOSFull.Helpers;

namespace JetStreamIOSFull
{
  public partial class BaseVCWithAppearance  : UIViewController
  {
    private IAppearanceHelper appearanceHelper;

    public BaseVCWithAppearance(IntPtr handle) : base(handle)
    {

    }

    public IAppearanceHelper Appearance
    {
      get
      {
        if (this.appearanceHelper == null)
        {
          throw new NullReferenceException ("Appearance value must be initialized");
        }

        return this.appearanceHelper;
      }
      set
      { 
        this.appearanceHelper = value;
      }
    }
  }
}

