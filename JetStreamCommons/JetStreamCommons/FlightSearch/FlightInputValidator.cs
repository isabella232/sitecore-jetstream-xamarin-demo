namespace JetStreamCommons.FlightSearch
{
  public static class FlightInputValidator
  {
    public static bool IsFlightInputValid(IFlightSearchUserInput flightInput)
    {
      bool isSourceAirportSet = ( null != flightInput.SourceAirport );
      bool isDestinationAirportSet = ( null != flightInput.DestinationAirport );

      return isSourceAirportSet && isDestinationAirportSet;
    }
  }
}

