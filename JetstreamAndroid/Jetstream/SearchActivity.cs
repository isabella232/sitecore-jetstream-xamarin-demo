namespace JetstreamAndroid
{
  using Android.App;
  using Android.OS;

//  [Activity(Label = "Jetstream", MainLauncher = true, Icon = "@drawable/icon")]
  [Activity(Label = "Jetstream", Icon = "@drawable/icon")]	
  public class SearchActivity : Activity
  {
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      this.SetContentView(Resource.Layout.activity_search);
    }
  }
}

