namespace JetstreamAndroid.Adapters
{
  using System.Collections.Generic;
  using Android.Content;
  using Android.Widget;
  using JetStreamCommons.Airport;

  class AutoCompleteAdapter : ArrayAdapter<string>
  {
    private Filter airportsFilter;

    public IList<IJetStreamAirport> SearchedAirports { get; set; }

    public AutoCompleteAdapter(Context context, int textViewResourceId, IList<string> objects)
      : base(context, textViewResourceId, objects)
    {
    }

    public override Filter Filter
    {
      get
      {
        return this.airportsFilter ?? (this.airportsFilter = new AirportsFilter(Context, this));
      }
    }
  }
}