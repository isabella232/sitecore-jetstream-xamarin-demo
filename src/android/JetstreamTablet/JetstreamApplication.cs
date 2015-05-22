namespace Jetstream
{
  using System;
  using Android.App;
  using Android.Runtime;
  using Squareup.Picasso;

  [Application]
  public class JetstreamApplication : Application
  {
    public JetstreamApplication(IntPtr handle, JniHandleOwnership ownerShip)
      : base(handle, ownerShip)
    {
#if DEBUG
      Picasso.With(this).LoggingEnabled = true;
#endif
    }

    public override void OnCreate()
    {
      base.OnCreate();
    }
  }
}

