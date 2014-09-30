namespace JetstreamAndroid.Utils
{
  using System;
  using System.IO;
  using Android.Content;
  using JetStreamCommons.HtmlBuilders;

  class AndroidFlightDetailsHtmlBuilder : FlightDetailsHtmlBuilder
  {
    private readonly Context context;

    public AndroidFlightDetailsHtmlBuilder(Context context)
    {
      this.context = context;
    }

    protected override string FlightDetailsHtmlTemlate()
    {
      var stream = context.Assets.Open("flightDetailsTemplate.html");
      return new StreamReader(stream).ReadToEnd();
    }

    protected override string LocalizedStringFromDate(DateTime date)
    {
      return date.ToLongDateString();
    }
  }
}