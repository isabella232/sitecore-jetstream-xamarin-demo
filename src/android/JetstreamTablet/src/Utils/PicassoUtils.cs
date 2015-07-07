namespace Jetstream.Utils
{
  using JetStreamCommons.Destinations;
  using Jetstream.UI.Activities;
  using Square.Picasso;
  using System;
  using System.Collections.Generic;
  using System.IO;

  public static class PicassoUtils
  {
    public static void ClearCache(List<IDestination> destinations, MainActivity activity)
    {
      var picasso = Picasso.With(activity);

      if(destinations != null)
      {
        using (var session = activity.GetSession())
        {
          foreach (var dest in destinations)
          {
            picasso.Invalidate(dest.ImageUrl(session));
          }
        }
      }

      var cachefolderPath = activity.ApplicationContext.CacheDir + "/picasso-cache";

      var cacheDir = new DirectoryInfo(cachefolderPath);
      if(cacheDir.Exists)
      {
        cacheDir.Delete(true);
      }
    }
  }
}

