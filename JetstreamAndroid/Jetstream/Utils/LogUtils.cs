namespace JetstreamAndroid.Utils
{
  using System;
  using Android.Util;

  public class LogUtils
  {
    private const string LogPrefix = "jetstream_";

    private static string MakeLogTag(Type type)
    {
      return LogPrefix + type.Name;
    }

    public static void Error(Type type, string message, Exception cause = null)
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

