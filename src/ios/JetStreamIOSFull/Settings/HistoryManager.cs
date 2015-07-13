using System;
using Foundation;
using System.Collections.Generic;
using JetStreamIOSFull.Helpers;

namespace JetStreamIOSFull.Settings
{
  public class HistoryManager
  {
    private const int HISTORY_LIMIT = 10;
    private NSString STORAGE_KEY = new NSString("JET_STREAM_URLS_HISTORY");

    private NSMutableArray currentHistory = null;

    public HistoryManager()
    {
      this.currentHistory = this.History.MutableCopy() as NSMutableArray;
    }

    private NSArray History
    {
      get 
      {
        NSMutableArray value = NSUserDefaults.StandardUserDefaults.ValueForKey(STORAGE_KEY) as NSMutableArray;

        if (value == null)
        {
          value = new NSMutableArray();
        }
     
        return value;
      }
    }

    public bool AddUrlToHistory(string url)
    {
      NSString value = new NSString(url);

      for (nuint i = 0; i < this.currentHistory.Count; ++i)
      {
        NSString elem = this.currentHistory.GetItem<NSString>(i);
        if (elem.IsEqual(value))
        {
          return false;
        }
      }

      this.currentHistory.Add(value);

      AnalyticsHelper.TrackUrlChanged();

      if (this.currentHistory.Count > HISTORY_LIMIT)
      {
        this.currentHistory.RemoveObject(0);
      }

      this.SaveCurrentHistory();

      return true;
    }

    private void SaveCurrentHistory()
    {
      NSUserDefaults.StandardUserDefaults.SetValueForKey(this.currentHistory, STORAGE_KEY);
      NSUserDefaults.StandardUserDefaults.Synchronize();
    }

    public nuint Count
    {
      get
      { 
        return this.currentHistory.Count;
      }
    }

    public string UrlAtIndex(nuint index)
    {
      if (index > this.currentHistory.Count - 1)
      {
        return null;
      }

      NSString value = this.currentHistory.GetItem<NSString>(index);

      return value.ToString();
    }


  }
}

