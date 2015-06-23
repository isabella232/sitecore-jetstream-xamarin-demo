namespace Jetstream.Utils
{
  using Com.Rengwuxian.Materialedittext.Validation;
  using Java.Lang;

  public class SitecoreUrlValidator : METValidator
  {
    public SitecoreUrlValidator(string message)
      : base(message)
    {
      this.ErrorMessage = message;
    }

    public override bool IsValid(ICharSequence text, bool p1)
    {
      return !System.String.IsNullOrWhiteSpace(text.ToString());
    }
  }
}