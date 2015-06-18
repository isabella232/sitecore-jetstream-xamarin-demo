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
    public IDestination Destination { get; set; }

    public override Dialog OnCreateDialog(Bundle savedInstanceState)
    {
      var mainActivity = (MainActivity)this.Activity;
      this.destinationView = new DestinationView(this.Activity, mainActivity.Session);

      var rootView = this.destinationView.InitViewWithData(this.Destination);

      var builder = new Android.Support.V7.App.AlertDialog.Builder(this.Activity, Resource.Style.Destinations_Dialog_Theme);
      builder.SetView(rootView);

      var dialog = builder.Show();
      this.destinationView.OnBackButtonClicked = () => { dialog.Dismiss(); };

      int width = this.Resources.GetDimensionPixelSize(Resource.Dimension.destination_dialog_width);
      dialog.Window.SetLayout(width, ViewGroup.LayoutParams.WrapContent);

      return dialog;
    }

    public void OnClick(View v)
    {
      this.Dialog.Dismiss();
    }
  }
}