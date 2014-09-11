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
  }
}

