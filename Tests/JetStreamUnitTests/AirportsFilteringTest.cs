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

      Assert.AreEqual (this.airports.Count(), resultList.Count(), "wrong airports count");
    }

    [Test ()]
    public void AllAiprortWillBeFoundWithNullSearchString()
    {
      var searchEngine = new AirportsCaseInsensitiveSearchEngine(null);
      IEnumerable<IJetStreamAirport> searchResult = searchEngine.SearchAirports(this.airports);
      List<IJetStreamAirport> resultList = searchResult.ToList();

      Assert.AreEqual (this.airports.Count(), resultList.Count(), "wrong airports count");
    }

    [Test ()]
    public void AiprortCorrectSearchByName()
    {
      var searchEngine = new AirportsCaseInsensitiveSearchEngine("11");
      IEnumerable<IJetStreamAirport> searchResult = searchEngine.SearchAirports(this.airports);
      List<IJetStreamAirport> resultList = searchResult.ToList();

      Assert.AreEqual (1, resultList.Count(), "wrong airports count");
      Assert.AreEqual ( "11111111", resultList[0].Name, "wrong airport name");
    }

    [Test ()]
    public void AiprortCorrectSearchByCity()
    {
      var searchEngine = new AirportsCaseInsensitiveSearchEngine("22");
      IEnumerable<IJetStreamAirport> searchResult = searchEngine.SearchAirports(this.airports);
      List<IJetStreamAirport> resultList = searchResult.ToList();

      Assert.AreEqual (1, resultList.Count(), "wrong airports count");
      Assert.AreEqual ("22222222", resultList[0].City, "wrong airport name");
    }

    [Test ()]
    public void ResultWillNotBeDuplicated()
    {
      var searchEngine = new AirportsCaseInsensitiveSearchEngine("bb");
      IEnumerable<IJetStreamAirport> searchResult = searchEngine.SearchAirports(this.airports);
      List<IJetStreamAirport> resultList = searchResult.ToList();

      Assert.AreEqual (1, resultList.Count(), "wrong airports count");
    }

    [Test ()]
    public void NoneAiprortWillBeFoundWithNotExistentSearchString()
    {
      var searchEngine = new AirportsCaseInsensitiveSearchEngine("zzzzz");
      IEnumerable<IJetStreamAirport> searchResult = searchEngine.SearchAirports(this.airports);
      List<IJetStreamAirport> resultList = searchResult.ToList();

      Assert.AreEqual (0, resultList.Count(), "wrong airports count");
    }

    [Test ()]
    public void CorrectSearchByCityandName()
    {
      var searchEngine = new AirportsCaseInsensitiveSearchEngine("77");
      IEnumerable<IJetStreamAirport> searchResult = searchEngine.SearchAirports(this.airports);
      List<IJetStreamAirport> resultList = searchResult.ToList();

      Assert.AreEqual (2, resultList.Count(), "wrong airports count");
      Assert.AreEqual ("77777777", resultList[0].Name, "wrong airport name");
      Assert.AreEqual ("88888888", resultList[1].Name, "wrong airport name");
    }
  }
}

