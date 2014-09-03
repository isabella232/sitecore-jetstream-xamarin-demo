using System;

namespace JetStreamCommons
{
  public class QueryHelpers
  {
    public QueryHelpers()
    {
    }

    public static string QueryToSearchAirportsWithName(string name)
    {
      return "/sitecore/content/Global/Airports//*[@@templatename='Airport' and contains(@Airport Name, '" + name + "')]";
    }

    public static string QueryToSearchDepartFlightsWithRequest(SearchFlightsRequest request)
    {
      return "query=sitecore/content/Global/Flights//*" +
        "[" +
        "@@templatename='Flight' " +
        "and contains(@Departure Airport, '" + request.FromAirportId + "') " +
        "and contains(@Arrival Airport, '" + request.ToAirportId + "') " +
        //TODO: date to string convertion
        "and contains(@Departure Time, '" + request.DepartDate.ToString() + "') " +
        "]";
    }

    public static string QueryToSearchReturnFlightsWithRequest(SearchFlightsRequest request)
    {
      return "query=sitecore/content/Global/Flights//*" +
        "[" +
        "@@templatename='Flight' " +
        "and contains(@Departure Airport, '" + request.ToAirportId + "') " +
        "and contains(@Arrival Airport, '" + request.FromAirportId + "') " +
        //TODO: date to string convertion
        "and contains(@Departure Time, '" + request.ReturnDate.ToString() + "') " +
        "]";
    }
  }
}

