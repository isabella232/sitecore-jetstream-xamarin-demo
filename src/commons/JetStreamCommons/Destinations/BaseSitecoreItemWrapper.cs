namespace JetStreamCommons.Destinations
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Fields;
  using Sitecore.MobileSDK.API.Items;

  public class BaseSitecoreItemWrapper : ISitecoreItem
  {
    protected ISitecoreItem Wrapped { get; private set; }

    public BaseSitecoreItemWrapper(ISitecoreItem wrapped)
    {
      this.Wrapped = wrapped;
    }

    public IItemSource Source
    {
      get
      {
        return this.Wrapped.Source;
      }
    }

    public string DisplayName
    {
      get
      {
        return this.Wrapped.DisplayName;
      }
    }

    public bool HasChildren
    {
      get
      {
        return this.Wrapped.HasChildren;
      }
    }

    public string Id
    {
      get
      {
        return this.Wrapped.Id;
      }
    }

    public string TemplateId 
    {
      get 
      {
        return this.Wrapped.TemplateId;
      }
    }

    public string Path
    {
      get
      {
        return this.Wrapped.Path;
      }
    }

    public int FieldsCount
    {
      get
      {
        return this.Wrapped.FieldsCount;
      }
    }

    public IField this[string caseInsensitiveFieldName]
    {
      get
      {
        return this.Wrapped[caseInsensitiveFieldName];
      }
    }

    public IEnumerable<IField> Fields
    {
      get
      {
        return this.Wrapped.Fields;
      }
    }
  }
}
