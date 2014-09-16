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
  using JetStreamCommons.FlightFilter;

  using JetStreamIOS.ViewControllers.FlightsTable;
  using JetStreamIOS.Helpers;


	public partial class FlightListViewController : UIViewController
	{
    private IEnumerable<IJetStreamFlight> allFlights;
    private IFlightFilterUserInput filterUserInput;

    #region Injected Variables
    private bool IsFlyingBack { get; set; }

    private IFlightSearchUserInput CurrentSearchOptions { get; set; }
    private JetStreamOrder OrderToAccumulate { get; set; }

    public IFlightSearchUserInput SearchOptionsFromUser { get; set; }
    #endregion Injected Variables

    #region UIViewController
		public FlightListViewController(IntPtr handle) : base (handle)
		{
		}

    public override void ViewWillAppear(bool animated)
    {
      base.ViewWillAppear(animated);
      this.StopLoading();

      if (null == this.CurrentSearchOptions)
      {
        this.CurrentSearchOptions = this.SearchOptionsFromUser;
      }
      if (null == this.OrderToAccumulate)
      {
        this.OrderToAccumulate = new JetStreamOrder(null, null);
      }

      this.SetDefaultValues();
      this.ReloadData();
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
      this.OrderToAccumulate = new JetStreamOrder(departureFlight, this.OrderToAccumulate.ReturnFlight);

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
      this.OrderToAccumulate = new JetStreamOrder(this.OrderToAccumulate.DepartureFlight, returnFlight);
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
          this.allFlights = flights;

          yesterday = await loader.GetPreviousDayAsync();
          tomorrow = await loader.GetNextDayAsync();
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

    private IEnumerable<IJetStreamFlight> FilterFlights(IEnumerable<IJetStreamFlight> allFlights)
    {
      if (null == this.filterUserInput)
      {
        return allFlights;
      }

      return allFlights.Where(singleFlight =>
      {
//        if (this.filterUserInput.IsRedEyeFlight.Equals(singleFlight.

        return true;
//        singleFlight.DepartureTime.
      });
    }
    #endregion UITableViewController

    #region Storyboard
    public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
    {
      base.PrepareForSegue(segue, sender);

      if (StoryboardHelper.IsSegueToReturnFlightsSearch(segue))
      {
        FlightListViewController targetController = segue.DestinationViewController as FlightListViewController;
        this.InitializeReturnFlightsSearchController(targetController);
      }
      else if (StoryboardHelper.IsSegueToOrderSummary(segue))
      {
        OrderSummaryViewController targetController = segue.DestinationViewController as OrderSummaryViewController;
        this.InitializeFlightSummaryViewController(targetController);
      }
      else if (StoryboardHelper.IsSegueToFlightsFilter(segue))
      {
        FlightsFilterViewController targetControllers = segue.DestinationViewController as FlightsFilterViewController;
        this.InitializeFilterViewController(targetControllers);
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
      FlightsFilterViewController filterController = unwindSegue.SourceViewController as FlightsFilterViewController;
      this.filterUserInput = filterController.FilterData;

      this.ReloadData();
    }
      
    private void InitializeReturnFlightsSearchController(FlightListViewController targetController)
    {
      targetController.OrderToAccumulate = 
        new JetStreamOrder(
          this.OrderToAccumulate.DepartureFlight, 
          this.OrderToAccumulate.ReturnFlight);

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

    private void InitializeFlightSummaryViewController(OrderSummaryViewController targetController)
    {
      // TODO : inject order data
    }
  
    private void InitializeFilterViewController(FlightsFilterViewController targetControllers)
    {
      targetControllers.AllFlights = this.allFlights;
      targetControllers.FilterData = this.filterUserInput;
      targetControllers.DepartureLocalDate = this.CurrentSearchOptions.ForwardFlightDepartureDate;
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
