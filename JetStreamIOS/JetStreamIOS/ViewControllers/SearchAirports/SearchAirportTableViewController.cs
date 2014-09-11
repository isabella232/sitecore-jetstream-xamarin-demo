namespace JetStreamIOS
{
  using System;
  using System.Linq;
  using System.Threading.Tasks;
  using System.Collections.Generic;

  using MonoTouch.Foundation;
  using MonoTouch.UIKit;

  using JetStreamCommons;
  using JetStreamCommons.Airport;
  using JetStreamCommons.SearchAirports;
  using Sitecore.MobileSDK.API.Items;

	public partial class SearchAirportTableViewController : UIViewController
	{
    public delegate void AirportSelectedDelegate(
      IJetStreamAirport selectedAirport, 
      int airportIndexInTable);

		public SearchAirportTableViewController (IntPtr handle) : base (handle)
		{
		}

    #region UIViewController
    public override void ViewDidLoad()
    {
      base.ViewDidLoad ();

      this.tableViewSource = new SearchAirportsSource();
      this.TableView.Source = this.tableViewSource;

      this.tableViewDelegate = new SearchAirportsDelegate();
      this.tableViewDelegate.AirportsTable = this;
      this.TableView.Delegate = this.tableViewDelegate;

      this.searchBarDelegate = new AirportsSearchBarDelegate();
      this.searchBarDelegate.AirportsTable = this;
      this.SearchBar.Delegate = this.searchBarDelegate;

      this.GetAllAirports();
    }

    public override void ViewWillAppear(bool animated)
    {
      base.ViewWillAppear(animated);
      this.SearchBar.Text = this.SourceTextField.Text;
    }
    #endregion


    private async Task< IEnumerable<IJetStreamAirport> > DownloadAllAirportsAnimated()
    {
      this.ShowLoader();

      try
      {
        // It will automatically get values from the NSUserDefaults singleton
        var endpoint = new InstanceSettings();

        // It will be disposed by RestManager
        var session = endpoint.GetSession();
        using (var restManager = new RestManager(session))
        {
          return await restManager.SearchAllAirports();
        }
      }
      catch
      {
        AlertHelper.ShowLocalizedAlertWithOkOption("AIRPORTS_DOWNLOAD_FAILURE_ALERT_TITLE", "AIRPORTS_DOWNLOAD_FAILURE_ALERT_MESSAGE");
        return null;
      }
      finally
      {
        this.HideLoader();
      }
    }

    private async void GetAllAirports()
    {
      this.AllAirportsList = await this.DownloadAllAirportsAnimated();

      if (null == this.AllAirportsList)
      {
        return;
      }
      else if (this.AllAirportsList.Count() == 0)
      {
        AlertHelper.ShowLocalizedAlertWithOkOption("AIRPORTS_NOT_FOUND_ALERT_TITLE", "AIRPORTS_NOT_FOUND_ALERT_MESSAGE");
        return;
      }


      // triggers UITableView
      this.SearchAirportsAndUpdateTable();
    }

    public void SearchAirportsAndUpdateTable()
    {
      string stringToSearch = this.SearchBar.Text.ToLowerInvariant();

      var searchEngine = new AirportsCaseInsensitiveSearchEngine(stringToSearch);
      IEnumerable<IJetStreamAirport> searchResult = searchEngine.SearchAirports(this.AllAirportsList);
      this.ResultList = searchResult.ToList();

      this.tableViewSource.Items = this.ResultList;
      this.TableView.ReloadData();
    }

    public void HideSearchKeyboard()
    {
      this.SearchBar.ResignFirstResponder();
    }

    #region UITableViewDelegate
    public void RowSelected(int row)
    {
      IJetStreamAirport selectedAirport = this.ResultList[row];
      this.OnAirportSelected(selectedAirport, row);

      this.SourceTextField.Text = selectedAirport.DisplayName;
      this.NavigationController.PopViewControllerAnimated(true);
    }
    #endregion

    #region Progress indicator
    private void ShowLoader()
    {
      this.loadingOverlay = new LoadingOverlay (this.View.Bounds, NSBundle.MainBundle.LocalizedString ("Loading Data", null));
      this.View.Add (loadingOverlay);
    }

    private void HideLoader()
    {
      if (this.loadingOverlay != null)
      {
        this.loadingOverlay.Hide ();
      }
    }
    #endregion


    private SearchAirportsSource tableViewSource;
    private SearchAirportsDelegate tableViewDelegate;
    private AirportsSearchBarDelegate searchBarDelegate;

    private IEnumerable<IJetStreamAirport> AllAirportsList;
    private List<IJetStreamAirport> ResultList;
    private LoadingOverlay loadingOverlay;

    public UITextField SourceTextField { get; set; }
    public AirportSelectedDelegate OnAirportSelected {get; set;}
	}
}
