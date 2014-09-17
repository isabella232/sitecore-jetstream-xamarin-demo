namespace JetStreamIOS
{
  using System;
  using System.Linq;
  using System.Collections.Generic;

  using MonoTouch.ObjCRuntime;
  using MonoTouch.Foundation;
  using MonoTouch.UIKit;

  using Sitecore.MobileSDK.API.Session;

  using JetStreamCommons;
  using JetStreamCommons.Flight;
  using JetStreamCommons.FlightSearch;

  using JetStreamIOS.ViewControllers.FlightsTable;
  using JetStreamIOS.Helpers;


	public partial class FlightListViewController : UIViewController
	{
    #region Injected Variables
    private bool IsFlyingBack { get; set; }

    public IFlightSearchUserInput SearchOptionsFromUser { get; set; }
    private IFlightSearchUserInput CurrentSearchOptions { get; set; }

    private JetStreamOrder OrderToAccumulate { get; set; } //user order!!!


    #endregion Injected Variables

    #region UIViewController
		public FlightListViewController(IntPtr handle) : base (handle)
		{
		}

    public override void ViewWillAppear(bool animated)
    {
      this.ShowScreenTitle ();
      this.StopLoading();

      if (null == this.CurrentSearchOptions)
      {
        this.CurrentSearchOptions = this.SearchOptionsFromUser;
      }
      if (null == this.OrderToAccumulate)
      {
        this.OrderToAccumulate = new JetStreamOrder(
          null, 
          null,
          this.CurrentSearchOptions.SourceAirport,
          this.CurrentSearchOptions.DestinationAirport,
          this.CurrentSearchOptions.TicketsCount
        );
      }

      this.SetDefaultValues();
      this.ReloadData();
    }

    private void ShowScreenTitle()
    {
      if (this.IsFlyingBack)
      {
        this.Title = NSBundle.MainBundle.LocalizedString ("RETURNING_SCREEN_TITLE", null);
      }
      else
      {
        this.Title = NSBundle.MainBundle.LocalizedString ("DEPARTING_SCREEN_TITLE", null);
      }
    }

    private void SetDefaultValues()
    {
      DateTime today = this.CurrentSearchOptions.ForwardFlightDepartureDate;
      DateTime yesterday = today.AddDays(-1);
      DateTime tomorrow  = today.AddDays(+1);

      this.TodayDateLabel.Text = DateConverter.StringFromDateForUI(today);
      this.YesterdayDateLabel.Text = DateConverter.StringFromDateForUI(yesterday);
      this.TomorrowDateLabel.Text = DateConverter.StringFromDateForUI(tomorrow);

      this.YesterdayPriceLabel.Text = NSBundle.MainBundle.LocalizedString("PRICE_UNAVAILABLE", null);
      this.TomorrowPriceLabel.Text = NSBundle.MainBundle.LocalizedString("PRICE_UNAVAILABLE", null);

      var tableSource = new FlightsTableViewEmptyDataSource();
      this.FlightsTableView.DataSource = tableSource;
      this.FlightsTableView.ReloadData();
    }
    #endregion UIViewController

    #region IBAction
    partial void OnTomorrowButtonPressed(MonoTouch.Foundation.NSObject sender)
    {
      DateTime dayIncrement = this.CurrentSearchOptions.ForwardFlightDepartureDate.AddDays(+1);
      this.ReloadDataForDate(dayIncrement);
    }

    partial void OnYesterdayButtonPressed(MonoTouch.Foundation.NSObject sender)
    {
      DateTime dayIncrement = this.CurrentSearchOptions.ForwardFlightDepartureDate.AddDays(-1);
      this.ReloadDataForDate(dayIncrement);
    }

    private void ReloadDataForDate(DateTime dayIncrement)
    {
      MutableFlightSearchUserInput newDay = new MutableFlightSearchUserInput();

      // same values
      {
        newDay.SourceAirport = this.CurrentSearchOptions.SourceAirport;
        newDay.DestinationAirport = this.CurrentSearchOptions.DestinationAirport;

        newDay.TicketClass  = this.CurrentSearchOptions.TicketClass;
        newDay.TicketsCount = this.CurrentSearchOptions.TicketsCount;
      }

      // changed values
      {
        newDay.ReturnFlightDepartureDate = null;
        newDay.ForwardFlightDepartureDate = dayIncrement;
      }

      this.CurrentSearchOptions = newDay;
      this.ReloadData();
    }
    #endregion IBAction

    #region Cell Input
    private void OnForwardFlightSelected(IJetStreamFlight departureFlight)
    {
      this.OrderToAccumulate = new JetStreamOrder(
        departureFlight, 
        this.OrderToAccumulate.ReturnFlight,
        this.OrderToAccumulate.DepartureAirport,
        this.OrderToAccumulate.DestinationAirport,
        this.OrderToAccumulate.TicketsCount
      );

      if (this.SearchOptionsFromUser.IsRoundTrip)
      {
        StoryboardHelper.NavigateToReturnFlightsListFromViewController(this);
      }
      else
      {
        StoryboardHelper.NavigateToOrderSummaryViewController(this);
      }
    }

    private void OnReturnFlightSelected(IJetStreamFlight returnFlight)
    {
      this.OrderToAccumulate = new JetStreamOrder(
        this.OrderToAccumulate.DepartureFlight, 
        returnFlight,
        this.OrderToAccumulate.DepartureAirport,
        this.OrderToAccumulate.DestinationAirport,
        this.OrderToAccumulate.TicketsCount
      );
      StoryboardHelper.NavigateToOrderSummaryViewController(this);
    }
    #endregion Cell Input

    #region UITableViewController
    private FlightCell.OnFlightSelectedDelegate GetCellButtonCallback()
    {
      FlightCell.OnFlightSelectedDelegate buttonCallback = null;
      if (this.IsFlyingBack)
      {
        buttonCallback = this.OnReturnFlightSelected;
      }
      else
      {
        buttonCallback = this.OnForwardFlightSelected;
      }

      return buttonCallback;
    }

    private async void ReloadData()
    {
      this.StartLoading();

      // @adk : on top of NSUserDefaults singleton
      InstanceSettings endpoint = new InstanceSettings();
      ISitecoreWebApiSession webApiSession = endpoint.GetSession();
      using (RestManager jetStreamSession = new RestManager(webApiSession))
      {
        var loader = new FlightSearchLoader(
          jetStreamSession,
          this.CurrentSearchOptions.SourceAirport,
          this.CurrentSearchOptions.DestinationAirport,
          this.CurrentSearchOptions.ForwardFlightDepartureDate);


        IEnumerable<IJetStreamFlight> flights = null;
        DaySummary yesterday = null;
        DaySummary tomorrow = null;

        try
        {
          flights = await loader.GetFlightsForTheGivenDateAsync();
          yesterday = await loader.GetPreviousDayAsync();
          tomorrow = await loader.GetNextDayAsync();
        }
        catch
        {
          string title = NSBundle.MainBundle.LocalizedString("ERROR_ALERT_TITLE", null);
          string newtworkErrorMessage = NSBundle.MainBundle.LocalizedString("LOADING_FLIGHTS_ERROR", null);
          AlertHelper.ShowLocalizedAlertWithOkOption(title, newtworkErrorMessage);
          return;
        }
        finally
        {
          this.StopLoading();
        }


        UITableViewDataSource tableSource = null;
        if (null == flights || 0 == flights.Count())
        {
          tableSource = new FlightsTableViewEmptyDataSource();
        }
        else
        {
          FlightCell.OnFlightSelectedDelegate buttonCallback = this.GetCellButtonCallback();            
          tableSource = new FlightsTableViewEnumerableDataSource(flights, buttonCallback);
        }
        this.FlightsTableView.DataSource = tableSource;
        this.FlightsTableView.ReloadData();




        this.TodayDateLabel.Text = DateConverter.StringFromDateForUI(this.CurrentSearchOptions.ForwardFlightDepartureDate);
        this.YesterdayDateLabel.Text = DateConverter.StringFromDateForUI(yesterday.DepartureDate);


        this.YesterdayPriceLabel.Text = yesterday.LowestPrice.HasValue ? 
            yesterday.LowestPrice.Value.ToString("C") : 
            NSBundle.MainBundle.LocalizedString("PRICE_UNAVAILABLE", null);

        this.TomorrowDateLabel.Text = DateConverter.StringFromDateForUI(tomorrow.DepartureDate);
        this.TomorrowPriceLabel.Text = tomorrow.LowestPrice.HasValue ? 
          tomorrow.LowestPrice.Value.ToString("C") :
          NSBundle.MainBundle.LocalizedString("PRICE_UNAVAILABLE", null);
      }
    }
    #endregion UITableViewController

    #region Storyboard
    public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
    {
      base.PrepareForSegue(segue, sender);

      if (StoryboardHelper.IsSegueToReturnFlightsSearch(segue))
      {
        FlightListViewController targetController = segue.DestinationViewController as FlightListViewController;
        this.ShowReturnFlightsSearch(targetController);
      }
      else if (StoryboardHelper.IsSegueToOrderSummary(segue))
      {
        OrderSummaryViewController targetController = segue.DestinationViewController as OrderSummaryViewController;
        this.ShowFlightSummaryScreen(targetController);
      }
      else if (StoryboardHelper.IsSegueToShowFlightDetails(segue))
      {
        if (sender.GetType () == typeof(FlightCell))
        {
          FlightCell cell = sender as FlightCell;
          FlightDetailsViewController targetController = segue.DestinationViewController as FlightDetailsViewController;
          this.ShowFlightDetailsScreen(targetController, cell);
        }
      }
    }
      
    public override UIViewController GetViewControllerForUnwind(
      Selector segueAction, 
      UIViewController fromViewController, 
      NSObject sender)
    {
      return this;
    }

    partial void unwindToFlightList(MonoTouch.UIKit.UIStoryboardSegue unwindSegue)
    {
      // TODO : apply filters
    }
      
    private void ShowReturnFlightsSearch(FlightListViewController targetController)
    {
      targetController.OrderToAccumulate = 
        new JetStreamOrder(
          this.OrderToAccumulate.DepartureFlight, 
          this.OrderToAccumulate.ReturnFlight,
          this.OrderToAccumulate.DepartureAirport,
          this.OrderToAccumulate.DestinationAirport,
          this.OrderToAccumulate.TicketsCount
        );

      targetController.SearchOptionsFromUser = this.SearchOptionsFromUser;


      // @adk reversed one way trip
      MutableFlightSearchUserInput returnFlightsConfig = new MutableFlightSearchUserInput();
      {
        returnFlightsConfig.SourceAirport = this.SearchOptionsFromUser.DestinationAirport;
        returnFlightsConfig.DestinationAirport = this.SearchOptionsFromUser.SourceAirport;
        returnFlightsConfig.ForwardFlightDepartureDate = this.SearchOptionsFromUser.ReturnFlightDepartureDate.Value;
        returnFlightsConfig.ReturnFlightDepartureDate = null;
        returnFlightsConfig.TicketClass = this.SearchOptionsFromUser.TicketClass;
        returnFlightsConfig.TicketsCount = this.SearchOptionsFromUser.TicketsCount;
      }
      targetController.CurrentSearchOptions = returnFlightsConfig;
      targetController.IsFlyingBack = true;
    }

    private void ShowFlightSummaryScreen(OrderSummaryViewController targetController)
    {
      targetController.Order = this.OrderToAccumulate;
    }

    private void ShowFlightDetailsScreen(FlightDetailsViewController targetController, FlightCell sender)
    {
      targetController.Order = this.OrderToAccumulate;
      targetController.SelectedFlight = sender;
    }
  
    #endregion Storyboard


    #region Progress
    private void StartLoading()
    {
      this.SetDefaultValues();

      this.ProgressIndicator.Hidden = false;
      this.ProgressIndicator.StartAnimating();
    }

    private void StopLoading()
    {
      this.ProgressIndicator.Hidden = true;
      this.ProgressIndicator.StopAnimating();
    }
    #endregion Progress
  }
}
