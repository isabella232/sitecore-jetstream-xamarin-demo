namespace Jetstream
{
  using System;
  using Android.App;
  using Android.Runtime;
  using Android.Util;
  using JetStreamCommons.Logging;
  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.PasswordProvider.Android;
  using Squareup.Picasso;

  [Application(Theme = "@style/Jetstream.App.Theme")]
  public class JetstreamApplication : Application
  {
    public JetstreamApplication(IntPtr handle, JniHandleOwnership ownerShip)
      : base(handle, ownerShip)
    {
#if DEBUG
      Picasso.With(this).LoggingEnabled = true;
#endif

      AppLog.InitLogger(this.GetString(Resource.String.app_name));
    }

    public override void OnCreate()
    {
      base.OnCreate();
    }

    public ScApiSession Session
    {
      get
      {
        ISitecoreWebApiSession session = null;
        using (var credentials = new SecureStringPasswordProvider("sitecore\\admin", "b"))
        {
          session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(Prefs.From(this).InstanceUrl)
                        .Credentials(credentials)
                          .DefaultDatabase("web")
                        .BuildSession();
        }

        return (ScApiSession)session;
      }
    }
  }

  public class AppLog : EmptyLogger
  {
    readonly string mainTag;
    static AppLog _instance;

    public static void InitLogger(string mainTag)
    {
      _instance = new AppLog(mainTag);
    }

    private AppLog(string mainTag)
    {
      this.mainTag = mainTag;
    }

    public static AppLog Logger
    {
      get
      {
        if (_instance == null)
        {
          throw new InvalidOperationException("Please init logger with InitLogger() before using it");
        }

        return _instance;
      }
    }

#if LOGGING
    public override void Debug(string tag, string message, params object[] args)
    {
      Log.Debug(tag, message, args);
    }

    public override void Debug(string tag, string message)
    {
      Log.Debug(tag, message);
    }

    public override void Debug(string format, params object[] args)
    {
      Log.Debug(this.mainTag, format, args);
    }

    public override void Debug(string message)
    {
      Log.Debug(this.mainTag, message);
    }
#endif

    public override void Error(string tag, string format, params object[] args)
    {
      Log.Error(tag, format, args);
    }

    public override void Error(string tag, string msg, Exception exception)
    {
      Log.Error(tag, msg, exception);
    }

    public override void Error(string msg, Exception exception)
    {
      Log.Error(this.mainTag, Java.Lang.Throwable.FromException(exception), msg);
    }

    public override void Error(string format, params object[] args)
    {
      Log.Error(this.mainTag, format, args);
    }

    public void Error(string message)
    {
      Log.Error(this.mainTag, message);
    }
  }
}

