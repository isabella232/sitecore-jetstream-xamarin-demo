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
      AirportsTable.SearchAirports();
      searchBar.ResignFirstResponder();
    }
    public override void TextChanged (UISearchBar searchBar, string searchText)
    {
      AirportsTable.SearchAirports();
    }

    public SearchAirportTableViewController AirportsTable;
  }
}

