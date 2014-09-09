namespace JetStreamIOS
{
  using System;
  using MonoTouch.Foundation;


  public static class DateConverter
  {
    public static string StringFromDateForUI(DateTime date)
    {
      return  NSDateFormatter.ToLocalizedString(date, NSDateFormatterStyle.Short, NSDateFormatterStyle.None);
    }
  }
}

