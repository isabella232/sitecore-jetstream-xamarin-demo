// This file has been autogenerated from a class added in the UI designer.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using JetStreamCommons;
using Sitecore.MobileSDK.API.Items;
using System.Collections.Generic;
using Sitecore.MobileSDK.Items;

namespace JetStreamIOS
{
	public partial class SearchAirportTableViewController : UITableViewController
	{
		public SearchAirportTableViewController (IntPtr handle) : base (handle)
		{
		}

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

    private async void GetAllAirports()
    {
      this.ShowLoader();

      try
      {
        using (var restManager = new RestManager())
        {
          this.AllAirportsList = await restManager.SearchAllAirports();
        }
      }
      catch
      {
        AlertHelper.ShowAlertWithOkOption("Failure", "Unable to download airports");
        return;
      }
      finally
      {
        this.HideLoader();
      }

      if (null == this.AllAirportsList || this.AllAirportsList.ResultCount == 0)
      {
        AlertHelper.ShowAlertWithOkOption("Failure", "No Airports Found");
        return;
      }


      // triggers UITableView
      this.SearchAirports();
    }

    public override void ViewWillAppear(bool animated)
    {
      base.ViewWillAppear(animated);

      if (null == SearchTicketsBuilder)
      {
        throw new ArgumentNullException();
      }

      this.SearchBar.Text = this.SourceTextField.Text;
    }

    public void SearchAirports()
    {
      this.ResultList = new List<ISitecoreItem>();

      foreach (ISitecoreItem elem in AllAirportsList)
      {
        string AirportName = elem["Airport Name"].RawValue.ToLowerInvariant();
        string City = elem["City"].RawValue.ToLowerInvariant();
        string StringToSearch = this.SearchBar.Text.ToLowerInvariant();

        bool AirportNameContainSearchedString = AirportName.IndexOf(StringToSearch) >= 0;
        bool CityContainSearchedString = City.IndexOf(StringToSearch) >= 0;

        if (AirportNameContainSearchedString || CityContainSearchedString)
        {
          this.ResultList.Add (elem);
        }
      }

      this.tableViewSource.Items = this.ResultList;
      this.TableView.ReloadData();
    }

    public void RowSelected (int row)
    {
      ISitecoreItem selectedAirport = ResultList [row];

      string airportId = selectedAirport.Id;
      if (this.isFromAirportSearch)
      {
        SearchTicketsBuilder.SetFromAirportItem(airportId);
      }
      else
      {
        SearchTicketsBuilder.SetToAirportItem(airportId);
      }

      this.SourceTextField.Text = selectedAirport.DisplayName;
      this.NavigationController.PopViewControllerAnimated(true);
    }

    public void ShowLoader()
    {
      this.loadingOverlay = new LoadingOverlay (this.View.Bounds, NSBundle.MainBundle.LocalizedString ("Loading Data", null));
      this.View.Add (loadingOverlay);
    }

    public void HideLoader()
    {
      if (this.loadingOverlay != null)
      {
        this.loadingOverlay.Hide ();
      }
    }

    private SearchAirportsSource tableViewSource;
    private SearchAirportsDelegate tableViewDelegate;
    private AirportsSearchBarDelegate searchBarDelegate;

    private ScItemsResponse AllAirportsList;
    private List<ISitecoreItem> ResultList;
    private LoadingOverlay loadingOverlay;

    public UITextField SourceTextField;
    public SearchTicketsRequestBuilder SearchTicketsBuilder;
    public bool isFromAirportSearch = true;
	}
}
