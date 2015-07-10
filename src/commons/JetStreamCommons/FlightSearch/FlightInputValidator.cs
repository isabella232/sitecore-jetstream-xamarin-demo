
namespace JetStreamCommons.FlightSearch
{
  using System;

  public static class FlightInputValidator
  {
    public static bool IsFlightInputValid(IFlightSearchUserInput flightInput)
    {
      bool isSourceAirportSet = ( null != flightInput.DepartureAirport );
      bool isDestinationAirportSet = ( null != flightInput.DestinationAirport );

      return isSourceAirportSet && isDestinationAirportSet;
    }

    public static void CheckIsDataCorrectWithException(IFlightSearchUserInput flightInput)
    {
      if (null == flightInput.DepartureAirport)
      {
        throw new ArgumentNullException("FROM_AIRPORT_IS_NULL");
      }

      if (null == flightInput.DestinationAirport)
      {
        throw new ArgumentNullException("TO_AIRPORT_IS_NULL");
      }

      if (null == flightInput.ForwardFlightDepartureDate)
      {
        throw new ArgumentNullException("DEPART_DATE_IS_NULL");
      }

      DateTime pastDate = DateTime.Now;

      DateTime departDateValue = flightInput.ForwardFlightDepartureDate.Value;

      if (pastDate.Date > departDateValue.Date)
      {
        throw new ArgumentNullException("DEPART_DATE_MUST_BE_A_FUTURE");
      }

      if (flightInput.IsRoundTrip)
      {
        DateTime returnDateValue = flightInput.ReturnFlightDepartureDate.Value;

        if (pastDate.Date > returnDateValue.Date)
        {
          throw new ArgumentNullException ("RETURN_DATE_MUST_BE_A_FUTURE");
        }

        if (departDateValue.Date > returnDateValue.Date)
        {
          throw new ArgumentException ("RETURN_DATE_MUST_BE_AFTER_DEPARTURE");
        }
      }
    }

  }
}

