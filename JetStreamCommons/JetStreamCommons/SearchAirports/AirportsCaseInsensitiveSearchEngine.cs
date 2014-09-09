namespace JetStreamCommons.SearchAirports
{
  using System;
  using System.Linq;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Items;


  public class AirportsCaseInsensitiveSearchEngine
  {
    private string textToSearch;

    public AirportsCaseInsensitiveSearchEngine(string textToSearch)
    {
      this.textToSearch = textToSearch.ToLowerInvariant();
    }

    public bool IsAirportMatchingPredicate(ISitecoreItem singleAirport)
    {
      string stringToSearch = this.textToSearch;

      string airportName = singleAirport["Airport Name"].RawValue.ToLowerInvariant();
      string city = singleAirport["City"].RawValue.ToLowerInvariant();

      bool isAirportNameContainsSearchedString = airportName.IndexOf(stringToSearch) >= 0;
      bool isCityContainsSearchedString = city.IndexOf(stringToSearch) >= 0;

      bool isAirportMatchesSearchPredicate = (isAirportNameContainsSearchedString || isCityContainsSearchedString);
      return isAirportMatchesSearchPredicate;
    }

    public IEnumerable<ISitecoreItem> SearchAirports(IEnumerable<ISitecoreItem> multipleAirports)
    {
      Func<ISitecoreItem, bool> filter = 
        singleAirport =>
        {
          return this.IsAirportMatchingPredicate(singleAirport);
        };

      IEnumerable<ISitecoreItem> searchResult = multipleAirports.Where(filter);
      return searchResult;
    }
  }
}

