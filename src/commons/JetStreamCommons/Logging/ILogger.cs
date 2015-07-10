namespace JetStreamCommons.Logging
{
  using System;

  public interface ILogger
  {
    #region Debug
    void Debug(string tag, string message, params object[] args);
    void Debug(string tag, string message);

    void Debug(string format, params object[] args);
    void Debug(string format);
    #endregion Debug

    #region Error
    void Error(string tag, string format, params object[] args);
    void Error(string tag, string msg, Exception exception);

    void Error(string msg, Exception exception);
    void Error(string format, params object[] args);
    #endregion Error
  }
}
