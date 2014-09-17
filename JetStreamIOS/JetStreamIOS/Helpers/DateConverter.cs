namespace JetStreamIOS
{
  using System;
  using Humanizer;
  using MonoTouch.Foundation;


  public static class DateConverter
  {
    public static string StringFromDateForUI(DateTime date)
    {
      NSDate convertedDate = DateTime.SpecifyKind(date, DateTimeKind.Local);
      return  NSDateFormatter.ToLocalizedString(convertedDate, NSDateFormatterStyle.Short, NSDateFormatterStyle.None);
    }

    public static string StringFromDateTimeForSummary(DateTime date)
    {
      NSDate convertedDate = DateTime.SpecifyKind(date, DateTimeKind.Local);
      return  NSDateFormatter.ToLocalizedString(convertedDate, NSDateFormatterStyle.Short, NSDateFormatterStyle.Short);
    }

    public static string StringFromTimeForUI(DateTime date)
    {
      NSDate convertedDate = DateTime.SpecifyKind(date, DateTimeKind.Local);
      return  NSDateFormatter.ToLocalizedString(convertedDate, NSDateFormatterStyle.None, NSDateFormatterStyle.Short);
    }

    public static string StringFromTimeSpanForUI(TimeSpan timeInterval)
    {
      return timeInterval.Humanize();
    }
  }
}

