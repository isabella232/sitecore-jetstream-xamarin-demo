using NUnit.Framework;
using System;
using JetStreamCommons.Airport;
using System.Collections.Generic;
using JetStreamCommons.SearchAirports;
using System.Linq;

namespace JetStreamUnitTests
{
  [TestFixture ()]
  public class AirportsFilteringTest
  {
    private IEnumerable<IJetStreamAirport> airports;

    [SetUp ()]
    public void SetUp()
    {
      this.airports = AirportsListPOD.GetAirports ();
    }

    [Test ()]
    public void AllAiprortWillBeFoundWithEmptySearchString()
    {
      var searchEngine = new AirportsCaseInsensitiveSearchEngine("");
      IEnumerable<IJetStreamAirport> searchResult = searchEngine.SearchAirports(this.airports);
      List<IJetStreamAirport> resultList = searchResult.ToList();
    }
  }
}

