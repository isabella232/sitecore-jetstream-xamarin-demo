namespace JetStreamCommons
{
  using System;


  public static class RawValueConverter
  {
    public static bool ToBoolean(string rawValue)
    {
      if (string.IsNullOrWhiteSpace(rawValue))
      {
        string message = "RawValue does not have a boolean value because it is empty.";
        throw new ArgumentException(message);
      }

      const string strTrue = "true";
      const string strFalse = "false";

      string lowerCaseInput = rawValue.ToLowerInvariant();

      if (strTrue.Equals(lowerCaseInput))
      {
        return true;
      }
      else if (strFalse.Equals(lowerCaseInput))
      {
        return false;
      }
      else
      {
        string message = string.Format("RawValue does not have a boolean value. Actual value : |{0}| ", rawValue);
        throw new ArgumentException(message);
      }
    }

    public static decimal ToMoney(string rawValue)
    {
      if (string.IsNullOrWhiteSpace(rawValue))
      {
        string message = "RawValue does not have a decimal value because it is empty.";
        throw new ArgumentException(message);
      }

      return Decimal.Parse(rawValue);
    }
  
    public static DateTime ToDateTime(string rawValue)
    {
      if (string.IsNullOrWhiteSpace(rawValue))
      {
        string message = "RawValue does not have a date value because it is empty.";
        throw new ArgumentException(message);
      }


      //      20140817T125041
      string rawYear = rawValue.Substring(0, 4);
      string rawMonth = rawValue.Substring(4, 2);
      string rawDay = rawValue.Substring(6, 2);
//      string t = rawValue.Substring(8, 1);
      string rawHour = rawValue.Substring(9, 2);
      string rawMinute = rawValue.Substring(11, 2);
      string rawSecond = rawValue.Substring(13, 2);

      int year = Convert.ToInt32(rawYear);
      int month = Convert.ToInt32(rawMonth);
      int day = Convert.ToInt32(rawDay);

      int hour = Convert.ToInt32(rawHour);
      int minute = Convert.ToInt32(rawMinute);
      int second = Convert.ToInt32(rawSecond);

      DateTime result = new DateTime(
        year,
        month,
        day,
        hour,
        minute,
        second,
        DateTimeKind.Local);
        
      return result;
    }
  }
}

