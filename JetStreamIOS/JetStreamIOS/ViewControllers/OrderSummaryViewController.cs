
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using JetStreamCommons.FlightSearch;

namespace JetStreamIOS
{
  public partial class OrderSummaryViewController : UIViewController
  {
    public IFlightSearchUserInput CurrentSearchOptions { get; set; }

    public OrderSummaryViewController (IntPtr handle) : base (handle)
    {
    }

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();
        
      if (null != this.CurrentSearchOptions)
      {
        this.FillPriceSummaryData();
      }
    }
     
    private void FillPriceSummaryData()
    {
      string htmlFilePath = NSBundle.MainBundle.PathForResource("SummaryTemplate", "html");
      string templateHtmlString = System.IO.File.ReadAllText(htmlFilePath);

      int ticketCount = this.CurrentSearchOptions.TicketsCount;

      string departDate = DateConverter.StringFromDateTimeForSummary(this.CurrentSearchOptions.ForwardFlightDepartureDate);
      string returnDate = DateConverter.StringFromDateTimeForSummary(this.CurrentSearchOptions.ReturnFlightDepartureDate.Value);

      int departPrice = 55 * this.CurrentSearchOptions.TicketsCount;
      int returnPrice = 33 * this.CurrentSearchOptions.TicketsCount;
      int fullPrice = departPrice + returnPrice;
    

      string resultHtmlString = String.Format(templateHtmlString,
        ticketCount.ToString(),
        this.CurrentSearchOptions.SourceAirport.City,
        this.CurrentSearchOptions.SourceAirport.Country,
        this.CurrentSearchOptions.SourceAirport.Code,
        this.CurrentSearchOptions.DestinationAirport.City,
        this.CurrentSearchOptions.DestinationAirport.Country,
        this.CurrentSearchOptions.DestinationAirport.Code,
        departDate,
        "33333",
        returnDate,
        "44444",
        departPrice.ToString(),
        returnPrice.ToString(),
        fullPrice.ToString()
      );

      this.SummaryInfoWebView.LoadHtmlString (resultHtmlString, null);
    }

  }
}

