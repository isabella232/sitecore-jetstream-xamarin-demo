namespace JetstreamAndroid
{
  using Android.App;
  using Android.Content;
  using JetstreamAndroid.Models;

  public class JetstreamApp : Application
  {
    public static JetstreamApp From(Context context)
    {
      return context.ApplicationContext as JetstreamApp;
    }

    public FlightsContainer FlightsContainer { get; set; }
  }
}