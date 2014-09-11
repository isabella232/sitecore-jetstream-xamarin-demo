namespace JetStreamCommons.SearchAirports
{
  using System;
  using System.Linq;
  using System.Collections.Generic;

  using JetStreamCommons.Airport;
  using Sitecore.MobileSDK.API.Items;


  public class AirportsCaseInsensitiveSearchEngine
  {
    private string textToSearch;

    public AirportsCaseInsensitiveSearchEngine(string textToSearch)
    {
      if (null == textToSearch)
      {
        this.textToSearch = "";
      }
      else
      {
        this.textToSearch = textToSearch.ToLowerInvariant ();
      }
    }

    public bool IsAirportMatchingPredicate(IJetStreamAirport singleAirport)
    {
      string stringToSearch = this.textToSearch;

      string airportName = singleAirport.Name.ToLowerInvariant();
      string city = singleAirport.City.ToLowerInvariant();

      bool isAirportNameContainsSearchedString = airportName.IndexOf(stringToSearch) >= 0;
      bool isCityContainsSearchedString = city.IndexOf(stringToSearch) >= 0;

      bool isAirportMatchesSearchPredicate = (isAirportNameContainsSearchedString || isCityContainsSearchedString);
      return isAirportMatchesSearchPredicate;
    }

    public IEnumerable<IJetStreamAirport> SearchAirports(IEnumerable<IJetStreamAirport> multipleAirports)
    {
      Func<IJetStreamAirport, bool> filter = 
        singleAirport =>
        {
          return this.IsAirportMatchingPredicate(singleAirport);
        };

      IEnumerable<IJetStreamAirport> searchResult = multipleAirports.Where(filter);
      return searchResult;
    }
  }
}

