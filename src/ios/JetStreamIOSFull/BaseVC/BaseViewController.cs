using System;
using UIKit;
using JetStreamIOSFull.Helpers;

namespace JetStreamIOSFull
{
  public partial class BaseViewController  : UIViewController
  {
    private IAppearanceHelper appearanceHelper;
    private InstanceSettings.InstanceSettings endpoint;

    public BaseViewController(IntPtr handle) : base(handle)
    {

    }

    public InstanceSettings.InstanceSettings Endpoint
    {
      get
      {
        if (this.endpoint == null)
        {
          throw new NullReferenceException("Endpoint value must be initialized");
        }

        return this.endpoint;
      }
      set
      { 
        this.endpoint = value;
      }
    }

    public IAppearanceHelper Appearance
    {
      get
      {
        if (this.appearanceHelper == null)
        {
          throw new NullReferenceException("Appearance value must be initialized");
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

