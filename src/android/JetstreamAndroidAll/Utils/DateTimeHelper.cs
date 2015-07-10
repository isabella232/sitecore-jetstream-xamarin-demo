namespace JetstreamAndroid.Utils
{
  using System;

  class DateTimeHelper
  {
    public static string DateTimeTo24HourFormat(DateTime date)
    {
      return date.ToString("HH:mm");
    }
  }
}