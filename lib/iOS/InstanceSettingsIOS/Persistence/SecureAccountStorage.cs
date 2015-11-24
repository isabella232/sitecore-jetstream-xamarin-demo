
namespace SitecoreInstanceSettingsIOS
{
  using System;
  using Security;
  using Foundation;

  public class SecureAccountStorage
  {
    private const string ServiceName = "InstanceSettingsIOS";

    public SecureAccountStorage()
    {
    }

    public string GetAccountPassword(string username)
    {
      var existingRecord = new SecRecord(SecKind.GenericPassword)
      {
        Account = username,
        Label = username,
        Service = ServiceName
      };

      SecStatusCode resultCode;
      SecRecord record = SecKeyChain.QueryAsRecord(existingRecord, out resultCode);

      if (resultCode != SecStatusCode.Success)
      {
        throw new Exception("No password found for provided account");
      }

      return NSString.FromData(record.ValueData, NSStringEncoding.UTF8);
    }

    public void StoreUserAccount(string username, string password)
    {
      var existingRecord = new SecRecord(SecKind.GenericPassword)
      {
        Account = username,
        Label = username,
        Service = ServiceName
      };

      SecStatusCode resultCode;
      SecKeyChain.QueryAsRecord(existingRecord, out resultCode);

      if (resultCode == SecStatusCode.Success)
      {
        resultCode = SecKeyChain.Remove(existingRecord);

        if (resultCode != SecStatusCode.Success)
        {
          throw new AccessViolationException("Can not delete previous password from the keychane");
        }
      }

      SecRecord record = new SecRecord (SecKind.GenericPassword) {
        Label = username,
        Account = username,
        Service = ServiceName,
        ValueData = NSData.FromString(password, NSStringEncoding.UTF8)
      };

      SecKeyChain.Add(record);
    }
  }
}

