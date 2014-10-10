namespace JetstreamAndroid.Adapters
{
  using System.Collections.Generic;
  using Android.Content;
  using Android.Views;
  using Android.Widget;
  using Java.Lang;
  using JetStreamCommons.Airport;

  class AutoCompleteAdapter : BaseAdapter<string>, IFilterable
  {
    public AirportsFilter AirportsFilter { get; set; }
    public IList<IJetStreamAirport> SearchedAirports { get; set; }

    private readonly LayoutInflater layoutInflater;

    public AutoCompleteAdapter(Context context)
    {
      this.layoutInflater = LayoutInflater.From(context);
    }

    public override View GetView(int position, View convertView, ViewGroup parent)
    {
      if (convertView == null)
      {
        convertView = this.layoutInflater.Inflate(Resource.Layout.airport_item, null);
      }

      var airpotTitle = convertView.FindViewById<TextView>(Resource.Id.textview_airport_title);
      var airpotCity = convertView.FindViewById<TextView>(Resource.Id.textview_airport_city);

      var airport = this.SearchedAirports[position];

      airpotTitle.Text = airport.Name;
      airpotCity.Text = airport.City;

      return convertView;
    }

    public override Object GetItem(int position)
    {
      if (this.SearchedAirports == null)
      {
        return "";
      }
      return this.SearchedAirports[position].DisplayName;
    }

    public override long GetItemId(int position)
    {
      return position;
    }

    public override int Count
    {
      get
      {
        if (this.SearchedAirports == null)
        {
          return 0;
        }

        return this.SearchedAirports.Count;
      }
    }

    public override string this[int position]
    {
      get
      {
        return this.SearchedAirports[position].DisplayName;
      }
    }

    public Filter Filter
    {
      get
      {
        return this.AirportsFilter;
      }
    }
  }
}