namespace JetStreamCommons.About
{
  using Sitecore.MobileSDK.API.Items;

  public class AboutPageWithItem : IAboutPageInfo
  {
    private ISitecoreItem item;

    public string TitlePlainText   
    { 
      get
      { 
        return StringProcessor.PrimitiveHTMLTagsRemove(this.TitleHtml);
      }
    }

    public string SummaryPlainText 
    { 
      get
      { 
        return StringProcessor.PrimitiveHTMLTagsRemove(this.SummaryHtml);
      } 
    }

    public string BodyPlainText   
    { 
      get
      { 
        return StringProcessor.PrimitiveHTMLTagsRemove(this.BodyHtml);
      }
    }


    public AboutPageWithItem(ISitecoreItem item)
    {
      this.item = item;
    }

    public string TitleHtml   
    { 
      get
      {
        return this.item["Title"].RawValue;
      }
    } 

    public string SummaryHtml
    { 
      get
      {
        return this.item["Summary"].RawValue;
      }
    } 

    public string BodyHtml
    { 
      get
      {
        return this.item["Body"].RawValue;
      }
    } 
  }
}

