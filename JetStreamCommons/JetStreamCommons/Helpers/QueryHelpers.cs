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
  }
}

