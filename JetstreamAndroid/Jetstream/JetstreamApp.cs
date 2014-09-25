namespace JetstreamAndroid
{
  using Android.App;
  using Android.Content;
  using JetstreamAndroid.Models;

  [Application]
  public class JetstreamApp : Application
  {
    public JetstreamApp(System.IntPtr javaReference, Android.Runtime.JniHandleOwnership transfer) : base(javaReference, transfer)
    {
    }

    public static JetstreamApp From(Context context)
    {
      return context.ApplicationContext as JetstreamApp;
    }

    public FlightsContainer FlightsContainer { get; set; }
  }
}