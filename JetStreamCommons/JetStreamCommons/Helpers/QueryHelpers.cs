using System;

namespace JetStreamCommons
{
  public class QueryHelpers
  {
    public QueryHelpers()
    {
    }

    public static string QueryToGetAllFlights()
    {
      return "/sitecore/content/Global/Airports//*";
    }

    public static string QueryToSearchTicketsWithRequest(SearchTicketsRequest request)
    {
      return "query=sitecore/content/Global/Flights//*" +
        "[" +
        "@@templatename='Flight' " +
        "and contains(@Departure Airport, '{2F8696E9-150B-43B1-B08B-EEEF82683B67}') " +
        "and contains(@Arrival Airport, '{4961A465-7684-4C9F-A9F8-C63ACBE778C0}') " +
        //TODO: date to string convertion
        "and contains(@Departure Time, '" +request.FlightDate.ToString()+ "') " +
       
        "]";
    }
  }
}

