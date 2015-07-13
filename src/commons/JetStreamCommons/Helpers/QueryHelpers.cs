namespace JetStreamCommons.Helpers
{
  using System;

  public static class QueryHelpers
  {
    public static string QueryToSearchAllRegionsAndCountriesItems()
    {
      // TODO: Can't query "Plan And Book" item. Investigate why!!!
      return  "/sitecore/content/Home//*[@@templatename='Region Item' or @@templatename='Country Item']";
    }

    public static string QueryToSearchAllCityItems()
    {
      // TODO: Can't query "Plan And Book" item. Investigate why!!!
      // TODO: Theoretically we can receive here more than 100 items
      return "/sitecore/content/Home//*[@@templatename='City Item']";
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
      string formatedDate = StringFromDateForQuery(request.DepartDate.Value);

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
      string formatedDate = StringFromDateForQuery(request.ReturnDate.Value);

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

