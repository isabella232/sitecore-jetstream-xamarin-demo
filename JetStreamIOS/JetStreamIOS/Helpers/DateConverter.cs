using System;

namespace JetStreamIOS
{
  public class DateConverter
  {
    public DateConverter()
    {
    }

    public static string StringFromDateForUI(DateTime date)
    {
      return string.Format("{0:00}/{1:00}/{2:0000}", date.Month, date.Day, date.Year);
    }

  }
}

