namespace Jetstream.UI.Activities
{
  using System;
  using Android.App;
  using Android.Graphics;
  using Android.OS;
  using Android.Support.V7.App;
  using Jetstream.Models;
  using Jetstream.UI.View;

  [Activity(Theme = "@style/Settings.Activity.Theme")]
  public class DestinationActivity : AppCompatActivity
  {
    public const string DestinationParamIntentKey = "destination";
    public const float ActivityHeight = 0.8f;

    private DestinationView destinationView;
    private DestinationAndroidSpec destination;

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      this.SetContentView(Resource.Layout.activity_destination_details);

      Point point = new Point();
      this.WindowManager.DefaultDisplay.GetSize(point);

      var activtiyHeight = point.Y;

      var param = this.Window.Attributes;
      param.Height = (int) (activtiyHeight * ActivityHeight);
      param.Width = (int)this.Resources.GetDimension(Resource.Dimension.destination_dialog_width);

      this.Window.Attributes = param;

      if (!this.Intent.HasExtra(DestinationParamIntentKey))
      {
        throw new InvalidOperationException("To start this activity specify destination for intent");
      }

      this.destination = new DestinationAndroidSpec(this.Intent.GetStringExtra(DestinationParamIntentKey));

      this.destinationView = new DestinationView(this, this.destination);
      this.destinationView.OnBackButtonClicked += this.Finish;

      this.destinationView.InitViews();
    }
  }
}