namespace JetstreamAndroid.Utils
{
  using System;
  using System.IO;
  using Android.Content;
  using JetStreamCommons.HtmlBuilders;

  class AndroidOrderSummaryHtmlBuilder : OrderSummaryHtmlBuilder
  {
    private readonly Context context;

    public AndroidOrderSummaryHtmlBuilder(Context context)
    {
      this.context = context;
    }

    protected override string OneWayHtmlTemlate()
    {
      var stream = context.Assets.Open("SummaryOneWayTemplate.html");
      return new StreamReader(stream).ReadToEnd();
    }

    protected override string RoundTripHtmlTemlate()
    {
      var stream = context.Assets.Open("SummaryWithRoundTrip.html");
      return new StreamReader(stream).ReadToEnd();
    }

    protected override string LocalizedStringFromDate(DateTime date)
    {
      return date.ToLongDateString();
    }
  }
}