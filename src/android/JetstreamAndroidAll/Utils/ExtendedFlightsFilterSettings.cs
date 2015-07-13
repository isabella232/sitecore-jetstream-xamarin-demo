namespace JetstreamAndroid.Utils
{
  using JetStreamCommons.FlightFilter;

  public class ExtendedFlightsFilterSettings : MutableFlightsFilterSettings
  {
    public ExtendedFlightsFilterSettings(ExtendedFlightsFilterSettings source) : base (source)
    {
      this.MaxAvaliblePrice = source.MaxAvaliblePrice;
    }

    public ExtendedFlightsFilterSettings()
    {
    }

    public decimal MaxAvaliblePrice { get; set; }
  }
}