namespace JetstreamAndroid.Fragments
{
  using System;
  using Android.App;
  using Android.OS;
  using Android.Views;
  using Android.Widget;
  using JetstreamAndroid.Utils;

  public class FilterFragment : DialogFragment
  {
    public interface IFilterActionListener
    {
      void ApplyFilter(ExtendedFlightsFilterSettings filter);
      void ClearFilter();
      ExtendedFlightsFilterSettings GetFilter();
      ExtendedFlightsFilterSettings GetDefaultFilter();
    }

    private const int DialogEarliestTime = 1;
    private const int DialogLatestTime = 2;

    private IFilterActionListener filterActionListener;
    private ExtendedFlightsFilterSettings oldFilter;

    private DateTime earliestTime;
    private DateTime latestTime;

    #region Views
    private SeekBar maxPriceSeekBar;
    private TextView seekBarValue;

    private Button earliestTimeButon;
    private Button latestTimeButon;

    private CheckBox foodServiceCheckBox;
    private CheckBox flightWifiCheckBox;
    private CheckBox entertainmentCheckBox;
    #endregion

    public static FilterFragment NewInstance()
    {
      return new FilterFragment();
    }

    public override void OnAttach(Activity activity)
    {
      this.filterActionListener = (IFilterActionListener)activity;
      base.OnAttach(activity);
    }

    public override Dialog OnCreateDialog(Bundle savedInstanceState)
    {
      var builder = new AlertDialog.Builder(Activity);

      builder.SetTitle("Filter");
      builder.SetView(this.CreateView());
      builder.SetPositiveButton("Apply", (sender, args) =>
      {
        this.ApplyFilter();
        this.Dialog.Dismiss();
      });

      builder.SetNegativeButton("Clear", (sender, args) =>
      {
        this.ClearFilter();
        this.Dialog.Cancel();
      });

      return builder.Create();
    }

    private View CreateView()
    {
      var rootView = Activity.LayoutInflater.Inflate(Resource.Layout.fragment_filter, null);

      this.InitViews(rootView);

      this.oldFilter = this.filterActionListener.GetFilter() ?? this.filterActionListener.GetDefaultFilter();

      this.UpdateVews(this.oldFilter);

      return rootView;
    }

    private void InitViews(View root)
    {
      this.maxPriceSeekBar = root.FindViewById<SeekBar>(Resource.Id.seekBar_max_price);
      this.seekBarValue = root.FindViewById<TextView>(Resource.Id.textView_seekbar_value);

      this.earliestTimeButon = root.FindViewById<Button>(Resource.Id.button_earliest_departure_time);
      earliestTimeButon.Click += (sender, args) => this.ShowDialogForDate(this.earliestTime, DialogEarliestTime);

      this.latestTimeButon = root.FindViewById<Button>(Resource.Id.button_latest_departure_time);
      latestTimeButon.Click += (sender, args) => this.ShowDialogForDate(this.latestTime, DialogLatestTime);

      this.flightWifiCheckBox = root.FindViewById<CheckBox>(Resource.Id.checkBox_flight_wifi);
      this.entertainmentCheckBox = root.FindViewById<CheckBox>(Resource.Id.checkBox_personal_entertainment);
      this.foodServiceCheckBox = root.FindViewById<CheckBox>(Resource.Id.checkBox_food_service);
    }

    private void UpdateVews(ExtendedFlightsFilterSettings filter)
    {
      this.maxPriceSeekBar.ProgressChanged += (sender, args) =>
      {
        this.seekBarValue.Text = args.Progress.ToString();
      };

      this.maxPriceSeekBar.Max = (int)filter.MaxAvaliblePrice;
      this.maxPriceSeekBar.Progress = (int)filter.MaxPrice;
      this.seekBarValue.Text = filter.MaxPrice.ToString();

      this.flightWifiCheckBox.Checked = filter.IsInFlightWifiIncluded;
      this.entertainmentCheckBox.Checked = filter.IsPersonalEntertainmentIncluded;
      this.foodServiceCheckBox.Checked = filter.IsFoodServiceIncluded;

      this.earliestTime = filter.EarliestDepartureTime;
      this.latestTime = filter.LatestDepartureTime;

      this.earliestTimeButon.Text = DateTimeHelper.DateTimeTo24HourFormat(this.earliestTime);
      this.latestTimeButon.Text = DateTimeHelper.DateTimeTo24HourFormat(this.latestTime);
    }

    private void ShowDialogForDate(DateTime time, int dialogId)
    {
      EventHandler<TimePickerDialog.TimeSetEventArgs> action = (sender, args) =>
      {
        var ts = new TimeSpan(args.HourOfDay, args.Minute, 0);
        switch (dialogId)
        {
          case DialogEarliestTime:
            this.earliestTime = this.earliestTime.Date + ts;
            this.earliestTimeButon.Text = DateTimeHelper.DateTimeTo24HourFormat(this.earliestTime);

            break;
          case DialogLatestTime:
            this.latestTime = this.latestTime.Date + ts;
            this.latestTimeButon.Text = DateTimeHelper.DateTimeTo24HourFormat(this.latestTime);

            break;
        }
      };

      new TimePickerDialog(Activity, action, time.Hour, time.Minute, true).Show();
    }

    private void ApplyFilter()
    {
      int priceValue = this.maxPriceSeekBar.Progress;

      var newFilter = new ExtendedFlightsFilterSettings(oldFilter)
      {
        MaxPrice = priceValue,
        EarliestDepartureTime = this.earliestTime,
        LatestDepartureTime = this.latestTime,
        IsInFlightWifiIncluded = this.flightWifiCheckBox.Checked,
        IsPersonalEntertainmentIncluded = this.entertainmentCheckBox.Checked,
        IsFoodServiceIncluded = this.foodServiceCheckBox.Checked
      };

      this.filterActionListener.ApplyFilter(newFilter);
    }

    private void ClearFilter()
    {
      this.filterActionListener.ClearFilter();
    }
  }
}