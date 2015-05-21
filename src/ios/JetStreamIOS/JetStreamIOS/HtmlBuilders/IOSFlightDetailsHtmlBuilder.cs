namespace JetStreamIOS
{
  using System;
  using MonoTouch.Foundation;

  using JetStreamCommons;
  using JetStreamCommons.HtmlBuilders;
  using JetStreamIOS.Helpers;


  public class IOSFlightDetailsHtmlBuilder : FlightDetailsHtmlBuilder
  {
    public IOSFlightDetailsHtmlBuilder()
    {
    }

    protected override string FlightDetailsHtmlTemlate()
    {
      string htmlFilePath = NSBundle.MainBundle.PathForResource("flightDetailsTemplate", "html");
      string templateHtmlString = System.IO.File.ReadAllText(htmlFilePath);
      return templateHtmlString;
    }

    protected override string LocalizedStringFromDate(DateTime date)
    {
      return DateConverter.StringFromDateTimeForSummary(date);
    }

  }
}

