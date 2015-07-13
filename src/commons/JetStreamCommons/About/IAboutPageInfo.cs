namespace JetStreamCommons.About
{
  public interface IAboutPageInfo : IBaseContentPage
  {
    string TitlePlainText   { get; }
    string SummaryPlainText { get; }
    string BodyPlainText    { get; }
  }
}

