namespace JetStreamIOS
{
  using System;
  using Humanizer;
  using MonoTouch.Foundation;


  public static class DateConverter
  {
    public static string StringFromDateForUI(DateTime date)
    {
      return  NSDateFormatter.ToLocalizedString(date, NSDateFormatterStyle.Short, NSDateFormatterStyle.None);
    }

    public static string StringFromDateTimeForSummary(DateTime date)
    {
      return  NSDateFormatter.ToLocalizedString(date, NSDateFormatterStyle.Short, NSDateFormatterStyle.Short);
    }

    public static string StringFromTimeForUI(DateTime date)
    {
      return  NSDateFormatter.ToLocalizedString(date, NSDateFormatterStyle.None, NSDateFormatterStyle.Short);
    }

    public static string StringFromTimeSpanForUI(TimeSpan timeInterval)
    {
      return timeInterval.Humanize();
    }
  }
}

