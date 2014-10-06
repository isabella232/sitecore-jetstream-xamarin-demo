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

    public override string ToString()
    {
      return string.Format("{0}, MaxAvaliblePrice: {1}", base.ToString(), this.MaxAvaliblePrice);
    }
  }
}