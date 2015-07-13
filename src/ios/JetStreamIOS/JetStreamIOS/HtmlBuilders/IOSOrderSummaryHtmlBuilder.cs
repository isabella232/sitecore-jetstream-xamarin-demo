namespace JetStreamIOS
{
  using System;
  using MonoTouch.Foundation;

  using JetStreamCommons;
  using JetStreamIOS.Helpers;
  using JetStreamCommons.HtmlBuilders;


  public class IOSOrderSummaryHtmlBuilder : OrderSummaryHtmlBuilder
  {
    public IOSOrderSummaryHtmlBuilder()
    {
    }

    protected override string OneWayHtmlTemlate()
    {
      string htmlFilePath = NSBundle.MainBundle.PathForResource("SummaryOneWayTemplate", "html");
      string templateHtmlString = System.IO.File.ReadAllText(htmlFilePath);
      return templateHtmlString;
    }

    protected override string RoundTripHtmlTemlate()
    {
      string htmlFilePath = NSBundle.MainBundle.PathForResource("SummaryWithRoundTrip", "html");
      string templateHtmlString = System.IO.File.ReadAllText(htmlFilePath);
      return templateHtmlString;
    }

    protected override string LocalizedStringFromDate(DateTime date)
    {
      return DateConverter.StringFromDateTimeForSummary(date);
    }
  }
}

