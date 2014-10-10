namespace JetstreamAndroid.Utils
{
  public class CurrencyUtil
  {
    private CurrencyUtil()
    {
    }

    public static string ConvertPriceToLocalString(decimal? price)
    {
      if(price == null)
      {
        return "N/A";
      }

      return ((decimal)price).ToString("C");
    }
  }
}

