namespace JetstreamTablet
{
  using Android.App;
  using Android.OS;
  using Android.Widget;
  using JetStreamCommons;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.PasswordProvider.Android;

  [Activity(MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity
  {
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      this.SetContentView(Resource.Layout.Main);

      var button = this.FindViewById<Button>(Resource.Id.myButton);

      button.Click += async delegate
      {
        var manager = new DestinationsLoader(this.Session);
        var response = await manager.LoadOnlyDestionations();

        Toast.MakeText(this, "Loaded destionations: " + response.Count, ToastLength.Long).Show();
      };
    }

    public ISitecoreWebApiSession Session
    {
      get
      {
        ISitecoreWebApiSession session = null;
        //        if (isAuthentiated)
        //        {
        using (var credentials = new SecureStringPasswordProvider("sitecore\\admin", "b"))
        {
          session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("jetstream800394rev150402.test24dk1.dk.sitecore.net")
                    .Credentials(credentials)
                      .DefaultDatabase("web")
                    .BuildSession();
        }
        //        }
        //        else
        //        {
        //        session = SitecoreWebApiSessionBuilder.AnonymousSessionWithHost("jetstream800394rev150402.test24dk1.dk.sitecore.net")
        //          .DefaultDatabase("web")
        //            .BuildSession();
        //        }

        return session;
      }
    }
  }
}


