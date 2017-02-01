namespace JetStreamCommons.Helpers
{
  using System;

  public static class QueryHelpers
  {
    public static string QueryItemIdToSearchAllRegionsAndCountriesItems()
    {
      return  "5D08B344-1794-40C2-A0CE-E861A569B713";
    }

    public static string QueryItemIdToSearchAllCityItems()
    {
      return "28A734E1-194F-487E-A15E-C071E171541A";
    }

    public static string QueryToSearchAirportsWithName(string name)
    {
      //NOTE: case sensitive, no way to search @City field using case insensitive option
      return "/sitecore/content/Global/Airports//*[@@templatename='Airport' and (contains(@Airport Name, '" + name + "') or contains(@City, '" + name + "')]";
    }

    public static string QueryItemIdToSearchAllAirports()
    {
      return "A4FCD654-46E5-43A0-B80A-21EA3D9C3024";
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

