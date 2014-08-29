
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Jetstream
{
  [Activity(Label = "Jetstream", MainLauncher = true, Icon = "@drawable/icon")]	
  public class SearchActivity : Activity
  {
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      SetContentView(Resource.Layout.Activity_Search);
      // Create your application here
    }
  }
}

