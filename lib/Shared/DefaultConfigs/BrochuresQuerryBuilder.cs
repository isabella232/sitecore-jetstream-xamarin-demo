
namespace DefaultConfigsStorage
{
  using System;
  using SCBrochureViewerCommon;

  public class BrochuresQuerryBuilder : IBrochuresQuerryBuilder
  {
    private string pathToBrochures;
    private string brochuresTemplate;

    public IBrochuresQuerryBuilder ShallowCopy()
    {
      BrochuresQuerryBuilder newBuilder = new BrochuresQuerryBuilder(this.pathToBrochures, this.brochuresTemplate);

      return newBuilder;
    }

    public BrochuresQuerryBuilder(string path, string templateName)
    {
      if (path == null || templateName == null)
      {
        throw new ArgumentNullException("Arguments must not be null");
      }

      if (!(path.StartsWith("/")))
      {
        throw new ArgumentNullException("Path must start with \"/\" symbol");
      }

      this.brochuresTemplate = templateName;
      this.pathToBrochures = path;
    }

    public string BrochuresQuerry
    {
      get 
      {
        return this.pathToBrochures + "/*[@@templatename='" + this.brochuresTemplate + "']";
      }
    }
  }
}

