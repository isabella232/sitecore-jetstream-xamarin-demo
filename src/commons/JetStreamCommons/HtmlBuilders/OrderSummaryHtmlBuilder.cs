using System;

namespace JetStreamCommons.HtmlBuilders
{
  public class OrderSummaryHtmlBuilder
  {
    public OrderSummaryHtmlBuilder()
    {
    }

    protected virtual string OneWayHtmlTemlate()
    {
//10 parameters must be in template string!!!
//      0 - tikets count
//      1 - depart from city
//      2 - depart from country
//      3 - depart from airport code
//      4 - depart to city
//      5 - depart to country
//      6 - depart to airport code
//      7 - depart date: 8/29/2014 9:33 PM
//      8 - depart flight number
//      9 - price
      return null;
    }

    protected virtual string RoundTripHtmlTemlate()
    {
//14 parameters must be in template string!!!
//      0 - tikets count
//      1 - depart from city
//      2 - depart from country
//      3 - depart from airport code
//      4 - depart to city
//      5 - depart to country
//      6 - depart to airport code
//      7 - depart date: 8/29/2014 9:33 PM
//      8 - depart flight number
//      9 - return date: 8/29/2014 9:33 PM
//      10 - return flight number
//      11 - full depart price
//      12 - full returning price price
//      13 - summmary price
      return null;
    }

    protected virtual string LocalizedStringFromDate(DateTime date)
    {
      return null;
    }

    public string GetHtmlStringWithOrder(JetStreamOrder order)
    {
      if (null != order)
      {
        if (null == order.ReturnFlight)
        {
          return this.GetOneWayHtmlStringWithOrder(order);
        }
        else
        {
          return this.GetRoundTripHtmlStringWithOrder(order);
        }
      }

      return null;
    }

    private string GetOneWayHtmlStringWithOrder(JetStreamOrder order)
    {
      string resultHtmlString = null;
      try
      {
        string templateHtmlString = this.OneWayHtmlTemlate();

        string departDate = this.LocalizedStringFromDate(order.DepartureFlight.DepartureTime);

        decimal fullPrice = order.DepartureFlight.Price * order.TicketsCount;

        resultHtmlString = String.Format(
          templateHtmlString,
          order.TicketsCount.ToString(),
          order.DepartureAirport.City,
          order.DepartureAirport.Country,
          order.DepartureAirport.Code,
          order.DestinationAirport.City,
          order.DestinationAirport.Country,
          order.DestinationAirport.Code,
          departDate,
          order.DepartureFlight.FlightNumber,
          fullPrice.ToString("C")
        );
      }
      catch
      {
        resultHtmlString = null;
      }

      return resultHtmlString;
    }



    private string GetRoundTripHtmlStringWithOrder(JetStreamOrder order)
    {
      string resultHtmlString = null;

      try
      {
        string templateHtmlString = this.RoundTripHtmlTemlate();

        string departDate = this.LocalizedStringFromDate(order.DepartureFlight.DepartureTime);
        string returnDate = this.LocalizedStringFromDate(order.ReturnFlight.DepartureTime);

        decimal departPrice = order.DepartureFlight.Price * order.TicketsCount;
        decimal returnPrice = order.ReturnFlight.Price * order.TicketsCount;
        decimal fullPrice = departPrice + returnPrice;

        resultHtmlString = String.Format (templateHtmlString,
          order.TicketsCount.ToString(),
          order.DepartureAirport.City,
          order.DepartureAirport.Country,
          order.DepartureAirport.Code,
          order.DestinationAirport.City,
          order.DestinationAirport.Country,
          order.DestinationAirport.Code,
          departDate,
          order.DepartureFlight.FlightNumber,
          returnDate,
          order.ReturnFlight.FlightNumber,
          departPrice.ToString("C"),
          returnPrice.ToString("C"),
          fullPrice.ToString("C"));
      }
      catch
      {
        resultHtmlString = null;
      }
      return resultHtmlString;
    }
  }
}

