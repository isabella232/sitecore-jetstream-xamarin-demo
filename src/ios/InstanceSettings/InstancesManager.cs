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
    private static string StorageKey = "InstancesManagerData";
    private static string ActiveIndexKey = "InstancesActiveIndex";
    private List<InstanceSettings> storedInstances = null;
    private int activeIndex = -1;

    public InstancesManager()
    {
      this.activeIndex = (int)(NSUserDefaults.StandardUserDefaults.IntForKey(ActiveIndexKey));
      this.storedInstances = this.RestoreInstancesFromStorage();
    }

    public int Count
    {
      get
      { 
        return this.storedInstances.Count;
      }
    }

    public void SetInstanceActive(InstanceSettings instance)
    {
      if (instance == null)
      {
        this.ActiveIndex = -1;
        return;
      }
        
      this.ActiveIndex = this.storedInstances.IndexOf(instance);
    }

    public int ActiveIndex
    {
      get
      {
        return this.activeIndex;
      }
      set
      {
        this.activeIndex = value;
        NSUserDefaults.StandardUserDefaults.SetInt(this.activeIndex, ActiveIndexKey);
        NSUserDefaults.StandardUserDefaults.Synchronize();
      }
    }

    public InstanceSettings InstanceAtIndex(int index)
    {
      if (index > this.Count - 1 || index < 0)
      {
        return null;
      }

      return this.storedInstances[index];
    }

    public InstanceSettings ActiveInstance
    {
      get
      { 
        return this.InstanceAtIndex(this.ActiveIndex);
      }
    }

    public void AddInstance(InstanceSettings instance)
    {
      if (this.storedInstances.Contains(instance))
      {
        return;
      }

      this.storedInstances.Add(instance);

      this.SaveInstancesToStorage(this.storedInstances);
    }
      
    public void DeleteInstanceAtIndex(int index)
    {
      InstanceSettings activeInstance = this.ActiveInstance;
      this.storedInstances.RemoveAt(index);
      this.SetInstanceActive(activeInstance);

      this.SaveInstancesToStorage(this.storedInstances);
    }

    private void SaveInstancesToStorage(List<InstanceSettings> instances)
    {
      string objects = JsonConvert.SerializeObject(instances, Formatting.Indented);

      NSUserDefaults.StandardUserDefaults.SetString(objects, StorageKey);
      NSUserDefaults.StandardUserDefaults.Synchronize();
    }

    private List<InstanceSettings> RestoreInstancesFromStorage()
    {
      string storage = NSUserDefaults.StandardUserDefaults.StringForKey(StorageKey);
      List<InstanceSettings> instances = null;

      if (storage != null)
      {
        instances = JsonConvert.DeserializeObject<List<InstanceSettings>>(storage);
      }

      if (instances == null)
      {
        instances = new List<InstanceSettings>();
      }

      return instances;
    }
   
  }
}

