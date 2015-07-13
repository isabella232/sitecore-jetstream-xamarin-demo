using System;
using JetStreamCommons.Flight;

namespace JetStreamCommons.HtmlBuilders
{
  public class FlightDetailsHtmlBuilder
  {
    public FlightDetailsHtmlBuilder()
    {
    }

    protected virtual string FlightDetailsHtmlTemlate()
    {
      //13 parameters must be in template string!!!
//      0 - flight number
//      1 - flight price
//      2 - departure time
//      3 - departure city
//      4 - departure country
//      5 - departure airport code
//      6 - arrival time
//      7 - arrival city
//      8 - arrival country
//      9 - arrival airport code
//      10 - first badge resource name
//      11 - second badge resource name
//      12 - third badge resource name
      return null;
    }

    protected virtual string LocalizedStringFromDate(DateTime date)
    {
      return null;
    }

    public string GetHtmlStringWithFlight(IJetStreamFlight flight, JetStreamOrder order)
    {
      string resultHtmlString = null;

      try
      {
        string templateHtmlString = this.FlightDetailsHtmlTemlate();

        resultHtmlString = String.Format(templateHtmlString,
          flight.FlightNumber,
          flight.Price.ToString("C"),
          this.LocalizedStringFromDate(flight.DepartureTime),
          order.DepartureAirport.City,
          order.DepartureAirport.Country,
          order.DepartureAirport.Code,
          this.LocalizedStringFromDate(flight.ArrivalTime),
          order.DestinationAirport.City,
          order.DestinationAirport.Country,
          order.DestinationAirport.Code,
          //badges hardcoded since item have no such information
          BadgeNames.BagBadgeResourceName(),
          BadgeNames.CrowBadgeResourceName(),
          BadgeNames.DrinkBadgeResourceName()
        );
      }
      catch
      {
        resultHtmlString = null;
      }

      return resultHtmlString;
    }
  }
}

