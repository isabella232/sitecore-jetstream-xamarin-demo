using NUnit.Framework;
using System;
using JetStreamCommons;

namespace JetStreamUnitTests
{
  [TestFixture ()]
  public class QueryBuilderTest
  {
    [Test ()]
    public void SearchAirportQuery()
    {
      string result = QueryHelpers.QueryToSearchAirportsWithName("test");
      string expected = "/sitecore/content/Global/Airports//*[@@templatename='Airport' and (contains(@Airport Name, 'test') or contains(@City, 'test')]";
      Assert.AreEqual (expected, result, "wrong result");
    }

    [Test ()]
    public void SearchAllAirportQuery()
    {
      string result = QueryHelpers.QueryToSearchAllAirports();
      string expected = "/sitecore/content/Global/Airports//*[@@templatename='Airport']";
      Assert.AreEqual (expected, result, "wrong result");
    }

    [Test ()]
    public void SearchDepartFlightsQuery()
    {
      string stringDate = "01/08/9000"; //future date to build correct request
      DateTime date = Convert.ToDateTime(stringDate);   

      SearchFlightsRequest request = new SearchTicketsRequestBuilder ()
        .SourceAirport("SourceAirport")
        .DestinationAirport("DestinationAirport")
        .DepartureDate(date)
        .ReturnDate(date)
        .RoundTrip(true)
        .Build();

      string result = QueryHelpers.QueryToSearchDepartFlightsWithRequest(request);
      string expected = "/sitecore/content/Global/Flights//*[@@templatename='Flight' and contains(@Departure Airport, 'SourceAirport') and contains(@Arrival Airport, 'DestinationAirport') and contains(@Departure Time, '90000108') ]";
      Assert.AreEqual (expected, result, "wrong result");
    }

    [Test ()]
    public void SearchReturnFlightsQuery()
    {
      string stringDate = "01/08/9000"; //future date to build correct request
      DateTime date = Convert.ToDateTime(stringDate);   

      SearchFlightsRequest request = new SearchTicketsRequestBuilder ()
        .SourceAirport("SourceAirport")
        .DestinationAirport("DestinationAirport")
        .DepartureDate(date)
        .ReturnDate(date)
        .RoundTrip(true)
        .Build();

      string result = QueryHelpers.QueryToSearchDepartFlightsWithRequest(request);
      string expected = "/sitecore/content/Global/Flights//*[@@templatename='Flight' and contains(@Departure Airport, 'DestinationAirport') and contains(@Arrival Airport, 'SourceAirport') and contains(@Departure Time, '90000108') ]";
      Assert.AreEqual (expected, result, "wrong result");
    }
  }
}

