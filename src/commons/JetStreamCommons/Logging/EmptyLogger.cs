namespace JetStreamCommons.Logging
{
  using System;

  public class EmptyLogger : ILogger
  {
    public virtual void Debug(string tag, string format, params object[] args)
    {
    }

    public virtual void Debug(string tag, string message)
    {
    }

    public virtual void Debug(string format, params object[] args)
    {
    }

    public virtual void Debug(string format)
    {
    }

    public virtual void Error(string tag, string format, params object[] args)
    {
    }

    public virtual void Error(string tag, string msg, Exception exception)
    {
    }

    public virtual void Error(string msg, Exception exception)
    {
    }

    public virtual void Error(string format, params object[] args)
    {
    }
  }
}