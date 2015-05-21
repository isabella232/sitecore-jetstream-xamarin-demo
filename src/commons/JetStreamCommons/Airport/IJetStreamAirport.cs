namespace JetStreamCommons.Airport
{
  public interface IJetStreamAirport
  {
    string Id { get; }
    string DisplayName { get; }


    string City { get; }
    string Country { get; }
    string Name { get; }
    string Code { get; }

    string TimeZoneId { get; }
  }
}

