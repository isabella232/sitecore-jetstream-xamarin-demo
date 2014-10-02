namespace JetstreamAndroid.Adapters
{
  using System;
  using System.Collections.Generic;
  using Android.Content;
  using Android.Views;
  using Android.Widget;
  using Java.Security;
  using JetstreamAndroid.Activities;
  using JetStreamCommons.Flight;

  public class FlightsListAdapter : BaseAdapter<IJetStreamFlight>
  {
    public interface IFlightOrderSelectedListener
    {
      void OnFlightOrderSelected(IJetStreamFlight flight);
    }

    private readonly List<IJetStreamFlight> flights;
    private readonly LayoutInflater layoutInflater;
    private readonly Context context;

    private readonly IFlightOrderSelectedListener orderSelectedListener;

    public FlightsListAdapter(Context context, List<IJetStreamFlight> fligths, IFlightOrderSelectedListener orderSelectedListener)
    {
      this.orderSelectedListener = orderSelectedListener;
      this.flights = fligths;
      this.context = context;
      this.layoutInflater = LayoutInflater.From(context);
    }

    public override long GetItemId(int position)
    {
      return position;
    }

    public override View GetView(int position, View convertView, ViewGroup parent)
    {
      if (convertView == null)
      {
        convertView = layoutInflater.Inflate(Resource.Layout.flights_list_item, parent, false);
      }

      var priceTextView = convertView.FindViewById<TextView>(Resource.Id.textview_flight_price);
      var departTextView = convertView.FindViewById<TextView>(Resource.Id.textview_flight_depart);
      var arrivalTextView = convertView.FindViewById<TextView>(Resource.Id.textview_flight_arrival);
      var orderButton = convertView.FindViewById<Button>(Resource.Id.button_order_ticket);

      var flight = this[position];

      var departText = flight.DepartureTime.ToShortTimeString();
      var arrivalText = flight.ArrivalTime.ToShortTimeString();

      priceTextView.Text = "$" + flight.Price;
      departTextView.Text = departText;
      arrivalTextView.Text = arrivalText;

      orderButton.Click += (sender, args) =>
      {
        if (this.orderSelectedListener != null)
        {
          this.orderSelectedListener.OnFlightOrderSelected(flight);
        }
      };

      return convertView;
    }

    public override int Count
    {
      get
      {
        return this.flights.Count;
      }
    }

    public override IJetStreamFlight this[int position]
    {
      get
      {
        return this.flights[position];
      }
    }
  }
}

