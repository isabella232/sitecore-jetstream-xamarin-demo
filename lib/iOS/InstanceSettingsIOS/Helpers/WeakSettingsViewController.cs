using System;
using SitecoreInstanceSettingsIOS;

namespace InstanceSettingsIOS
{
  public class WeakSettingsViewController : WeakReference
  {
    public WeakSettingsViewController(object target) : base(target)
    {
      this.CheckTargetType(target);
    }

    public WeakSettingsViewController(object target, bool trackResurrection) : base(target, trackResurrection)
    {
      this.CheckTargetType(target);
    }

    public SettingsViewController GetSettingsViewController()
    {
      if (this.Target == null)
      {
        return null;
      }

      this.CheckTargetType(this.Target);
      return this.Target as SettingsViewController;
    }

    private void CheckTargetType(object target)
    {
      if (!(target is SettingsViewController))
      {
        throw new InvalidCastException();
      }
    }
  }
}

