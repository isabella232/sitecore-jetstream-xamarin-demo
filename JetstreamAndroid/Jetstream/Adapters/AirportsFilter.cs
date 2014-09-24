namespace JetstreamAndroid.Adapters
{
  using System.Linq;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.Threading.Tasks;
  using Android.Content;
  using Android.Widget;
  using Java.Lang;
  using JetstreamAndroid.Utils;
  using JetStreamCommons;
  using JetStreamCommons.Airport;
  using JetStreamCommons.SearchAirports;
  using Sitecore.MobileSDK.API.Exceptions;

  class AirportsFilter : Filter
  {
    private IEnumerable<IJetStreamAirport> allAirports;
    private List<IJetStreamAirport> searchedAirports = new List<IJetStreamAirport>();

    private readonly Context context;
    private readonly AutoCompleteAdapter adapter;

    private readonly IOperationListener listener;

    public AirportsFilter(Context context, AutoCompleteAdapter adapter, IOperationListener listener)
    {
      this.context = context;
      this.adapter = adapter;
      this.listener = listener;
    }

    protected override FilterResults PerformFiltering(ICharSequence constraint)
    {
      this.searchedAirports.Clear();

      var threshold = context.Resources.GetInteger(Resource.Integer.airport_form_threshold);
      if(constraint == null || constraint.Length() < threshold)
      {
        return null;
      }

      if(this.allAirports == null)
      {
        try
        {
          this.listener.OnOperationStarted();
          this.ReceiveAirports();
          this.listener.OnOperationFinished();
        }
        catch (SitecoreMobileSdkException exception)
        {
          this.listener.OnOperationFailed();
          Debug.WriteLine(exception);
          return null;
        }
      }

      if(this.allAirports != null)
      {
        var searchEngine = new AirportsCaseInsensitiveSearchEngine(constraint.ToString());
        this.searchedAirports = new List<IJetStreamAirport>(searchEngine.SearchAirports(this.allAirports));
      }

      return new FilterResults();
    }

    private void ReceiveAirports()
    {
      try
      {
        var session = Prefs.From(this.context).Session;
        using (var restManager = new RestManager(session))
        {
          var airportsTask = restManager.SearchAllAirports();
          Task.WhenAll(airportsTask);

          this.allAirports = airportsTask.Result;
          LogUtils.Info(typeof(AirportsFilter), string.Format("Found {0} airports", this.allAirports.Count()));
        }
      }
      catch (System.Exception exception)
      {
        LogUtils.Error(typeof(AirportsFilter), "Exception during retrieving airports list\n" + exception);
        this.allAirports = Enumerable.Empty<IJetStreamAirport>();
      }
    }

    protected override void PublishResults(ICharSequence constraint, FilterResults results)
    {
      if(this.searchedAirports.Count == 0)
        this.adapter.NotifyDataSetInvalidated();
      else
      {
        this.adapter.SearchedAirports = this.searchedAirports;

        this.adapter.Clear();
        this.adapter.AddAll(this.searchedAirports.ConvertAll(input => input.DisplayName));

        this.adapter.NotifyDataSetChanged();
      }
    }
  }
}