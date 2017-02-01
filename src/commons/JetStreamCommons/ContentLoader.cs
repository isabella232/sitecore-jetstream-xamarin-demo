using Sitecore.MobileSDK.API.Request.Parameters;
using JetStreamCommons.About;

namespace JetStreamCommons
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using JetStreamCommons.Destinations;
  using JetStreamCommons.Helpers;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Session;

  public class ContentLoader : IDisposable
  {
    private ISitecoreSSCSession session;

    private bool disposed = false;

    public ContentLoader(ISitecoreSSCSession session)
    {
      this.session = session;
    }

    #region Public API


    public async Task<IAboutPageInfo> LoadAboutInfo()
    {

      var request = ItemSSCRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/Home/About")
        .Build();

      ScItemsResponse responce = await this.session.ReadItemAsync(request);

      if (responce.ResultCount > 0)
      {

        IAboutPageInfo result = new AboutPageWithItem(responce [0]);

        return result;
      }

      return null;
    }

    #endregion Public API

    #region IDisposable implementation
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.disposed)
      {
        return;
      }

      // Free any other managed objects here.
      if (disposing)
      {
        if (null != this.session)
        {
          this.session.Dispose();
          this.session = null;
        }
      }


      this.disposed = true;
    }

    ~ContentLoader()
    {
      this.Dispose(false);
    }
    #endregion IDisposable implementation
  }
}