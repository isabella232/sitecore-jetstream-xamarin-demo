using System;
using Foundation;
using System.Collections.Generic;
using Newtonsoft.Json;


//TODO: NSUserDefaults is not secure enough for credentials.
//  Please use either of :
// * native iOS keychain
// * C# SecureString class

namespace InstanceSettings
{
  public class InstancesManager
  {
    private static NSString StorageKey = new NSString("InstancesManagerData");
    private List<InstanceSettings> storedInstances = null;


    public InstancesManager()
    {
      string storage = NSUserDefaults.StandardUserDefaults.StringForKey(StorageKey);

      if (storage != null)
      {
        this.storedInstances = JsonConvert.DeserializeObject<List<InstanceSettings>>(storage);
      }

      if (this.storedInstances == null)
      {
        this.storedInstances = new List<InstanceSettings>();
      }
    }

    public int Count
    {
      get
      { 
        return this.storedInstances.Count;
      }
    }

    public InstanceSettings InstanceAtIndex(int index)
    {
      if (index > this.Count - 1)
      {
        throw new System.IndexOutOfRangeException(); 
      }

      return this.storedInstances[index];
    }

    public void AddInstance(InstanceSettings instance)
    {
      if (this.storedInstances.Contains(instance))
      {
        return;
      }

      this.storedInstances.Add(instance);

      string objects = JsonConvert.SerializeObject(this.storedInstances, Formatting.Indented);

      NSUserDefaults.StandardUserDefaults.SetString(objects, StorageKey);
      NSUserDefaults.StandardUserDefaults.Synchronize();
    }
   
  }
}

