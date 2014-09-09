using System;
using MonoTouch.UIKit;

namespace JetStreamIOS
{
  public class AirportsSearchBarDelegate : UISearchBarDelegate
  {
    public AirportsSearchBarDelegate()
    {
    }

    public override void SearchButtonClicked (UISearchBar searchBar)
    {
      AirportsTable.SearchAirportsAndUpdateTable();
      searchBar.ResignFirstResponder();
    }
    public override void TextChanged (UISearchBar searchBar, string searchText)
    {
      AirportsTable.SearchAirportsAndUpdateTable();
    }

    public SearchAirportTableViewController AirportsTable;
  }
}

