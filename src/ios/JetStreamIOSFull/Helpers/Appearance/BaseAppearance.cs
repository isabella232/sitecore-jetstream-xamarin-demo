using System;

namespace JetStreamIOSFull.Apearance
{
  public class BaseAppearance
  {
    protected IColorTheme ColorsTheme;

    public BaseAppearance(IColorTheme colorsTheme)
    {
      this.ColorsTheme = colorsTheme;
    }
  }
}

