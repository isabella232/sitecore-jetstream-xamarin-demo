using System;
using MonoTouch.Foundation;

namespace JetStreamIOS
{
  public class DateConverter
  {
    public DateConverter()
    {
    }

    public static string StringFromDateForUI(DateTime date)
    {
      return  NSDateFormatter.ToLocalizedString (date, NSDateFormatterStyle.Short, NSDateFormatterStyle.None);
    }

  }
}

