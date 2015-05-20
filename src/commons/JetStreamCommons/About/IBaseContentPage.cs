namespace JetStreamCommons.About
{
  public interface IBaseContentPage
  {
    string TitleHtml   { get; } // Rich Text
    string SummaryHtml { get; } // Rich Text
    string BodyHtml    { get; } // Rich Text

    //    string MenuTitle { get; }
    //    IEnumerable<string> MetaKeywords { get; }
    //    string MetaDescription { get; }

    // navigation
    //     side menu - checkbox
    //     main menu - checkbox

    // search
    //     Exclude from search - checkbox
  }
}

