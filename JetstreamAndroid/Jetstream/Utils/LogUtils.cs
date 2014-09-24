namespace JetstreamAndroid
{
  using System;
  using Android.Util;

  public class LogUtils
  {
    private const string LOG_PREFIX = "jetstream_";

    private static string MakeLogTag(Type type)
    {
      return LOG_PREFIX + type.Name;
    }

    public static void Error(Type type, string message, Exception cause)
    {
      Log.Error(MakeLogTag(type), message, cause);
    }

    public static void Info(Type type, string message)
    {
      #if DEBUG
      Log.Info(MakeLogTag(type), message);
      #endif
    }

  }
}

