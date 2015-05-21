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
  
    public static DateTime LocalDateTimeFromNSDate(NSDate nsDate)
    {
      NSCalendar cal = new NSCalendar(NSCalendarType.Gregorian);
      cal.Locale = NSLocale.CurrentLocale;

      NSCalendarUnit flags = 
        NSCalendarUnit.Year   |
        NSCalendarUnit.Month  |
        NSCalendarUnit.Day    |
        NSCalendarUnit.Hour   |
        NSCalendarUnit.Minute |
        NSCalendarUnit.Second ;

      NSDateComponents components = cal.Components(flags, nsDate);

      return new DateTime(
        components.Year, components.Month, components.Day, 
        components.Hour, components.Minute, components.Second, 
        DateTimeKind.Local);
    }

    public static string StringFromDollars(decimal dollars)
    {
      var locale = CultureInfo.CreateSpecificCulture("en-US");
      return dollars.ToString("C", locale);
    }
  }
}

