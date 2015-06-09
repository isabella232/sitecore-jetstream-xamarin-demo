using Jetstream.Map;

namespace Jetstream.UI.Dialogs
{
  using Android.App;
  using Android.OS;
  using Android.Views;
  using Jetstream.View;
  using JetStreamCommons.Destinations;

  public class DestinationDetailsDialog : DialogFragment, View.IOnClickListener
  {
    private DestinationView destinationView;
    public ClusterItem Destination { get; set; }

    public override Dialog OnCreateDialog(Bundle savedInstanceState)
    {
      this.destinationView = new DestinationView(this.Activity);

      var rootView = this.destinationView.InitViewWithData(this.Destination);

      var builder = new Android.Support.V7.App.AlertDialog.Builder(this.Activity, Resource.Style.AppCompatAlertDialogStyle);
      builder.SetView(rootView);

      var dialog = builder.Show();
      dialog.SetCanceledOnTouchOutside(false);
      return dialog;
    }

    public void OnClick(View v)
    {
      this.Dialog.Dismiss();
    }
  }
}