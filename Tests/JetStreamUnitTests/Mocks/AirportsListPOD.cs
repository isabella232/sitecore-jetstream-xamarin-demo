using System;
using JetStreamCommons.Airport;
using System.Collections;
using System.Collections.Generic;

namespace JetStreamUnitTests
{
  public static class AirportsListPOD
  {
    public static IEnumerable<IJetStreamAirport> GetAirports()
    {
      List<IJetStreamAirport> result = new List<IJetStreamAirport>();

      result.Add(new AirportPOD("11111111", "22222222"));
      result.Add(new AirportPOD("33333333", "44444444"));
      result.Add(new AirportPOD("55555555", "66666666"));
      result.Add(new AirportPOD("77777777", "88888888"));
      result.Add(new AirportPOD("88888888", "77777777"));
      result.Add(new AirportPOD("99999999", "aaaaaaaa"));
      result.Add(new AirportPOD("bbbbbbb", "bbbbbbb"));

      return result;
    }
  }
}

