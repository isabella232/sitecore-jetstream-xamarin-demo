namespace JetStreamIOS.Helpers
{
  using System;
  using System.Globalization;
  using MonoTouch.Foundation;
  using Humanizer;


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
  
    public static string StringFromDollars(decimal dollars)
    {
      var locale = CultureInfo.CreateSpecificCulture("en-US");
      return dollars.ToString("C", locale);
    }
  }
}

