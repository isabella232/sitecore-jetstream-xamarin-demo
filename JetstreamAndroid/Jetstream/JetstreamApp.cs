namespace JetstreamAndroid
{
  using Android.App;
  using Android.Content;
  using JetstreamAndroid.Models;
  using JetStreamCommons.Flight;

  [Application(Theme = "@android:style/Theme.Holo.Light")]
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
    public IJetStreamFlight SelectedFlight { get; set; }
  }
}