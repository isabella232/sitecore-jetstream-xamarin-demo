namespace Jetstream
{
  using Android.App;
  using Android.OS;
  using Sitecore.MobileSDK;
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
    }

    public ScApiSession Session
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

        return (ScApiSession)session;
      }
    }
  }
}