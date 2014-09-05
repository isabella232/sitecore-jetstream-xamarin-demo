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
      //NOTE: case sensitive, no way to search @City field using case insensitive option
      return "/sitecore/content/Global/Airports//*[@@templatename='Airport' and (contains(@Airport Name, '" + name + "') or contains(@City, '" + name + "')]";
    }

    public static string QueryToSearchAllAirports()
    {
      return "/sitecore/content/Global/Airports//*[@@templatename='Airport']";
    }

    public static string QueryToSearchDepartFlightsWithRequest(SearchFlightsRequest request)
    {
      string formatedDate = QueryHelpers.StringFromDateForQuery(request.DepartDate);

      return "/sitecore/content/Global/Flights//*" +
        "[" +
        "@@templatename='Flight' " +
        "and contains(@Departure Airport, '" + request.FromAirportId + "') " +
        "and contains(@Arrival Airport, '" + request.ToAirportId + "') " +
        "and contains(@Departure Time, '" + formatedDate + "') " +
        "]";
    }

    public static string QueryToSearchReturnFlightsWithRequest(SearchFlightsRequest request)
    {
      string formatedDate = QueryHelpers.StringFromDateForQuery(request.ReturnDate);

      return "/sitecore/content/Global/Flights//*" +
        "[" +
        "@@templatename='Flight' " +
        "and contains(@Departure Airport, '" + request.ToAirportId + "') " +
        "and contains(@Arrival Airport, '" + request.FromAirportId + "') " +
        "and contains(@Departure Time, '" + formatedDate + "') " +
        "]";
    }

    private static string StringFromDateForQuery(DateTime date)
    {
      return string.Format("{0:0000}{1:00}{2:00}", date.Year, date.Month, date.Day );
    }
  }
}

