namespace Jetstream.UI.Fragments
{
  using Android.Support.V4.App;
  using DSoft.Messaging;

  public abstract class BaseFragment : Fragment
  {
    MessageBusEventHandler refreshEventHandler;
    MessageBusEventHandler updateInstanceUrlEventHandler;

    protected bool RefreshRequired { get; set; }

    public abstract void OnRefreshed();
    protected abstract bool IsRefreshing { get; }

    public override void OnHiddenChanged(bool hidden)
    {
      base.OnHiddenChanged(hidden);

      if (!hidden && this.RefreshRequired)
      {
        this.RefreshRequired = false;
        this.OnRefreshed();
      }
    }

    public override void OnStart()
    {
      base.OnStart();

      if (this.refreshEventHandler == null)
      {
        this.refreshEventHandler = new MessageBusEventHandler()
        {
          EventId = EventIdsContainer.RefreshMenuActionClickedEvent,
          EventAction = (sender, evnt) =>
          {
            if (this.IsHidden || this.IsRefreshing)
            {
              return;
            }

            this.Activity.RunOnUiThread(this.OnRefreshed);
          }
        };
      }

      if (this.updateInstanceUrlEventHandler == null)
      {
        this.updateInstanceUrlEventHandler = new MessageBusEventHandler()
        {
          EventId = EventIdsContainer.SitecoreInstanceUrlUpdateEvent,
          EventAction = (sender, evnt) =>
          {
            if (this.IsHidden)
            {
              this.RefreshRequired = true;
              return;
            }

            if (this.IsRefreshing)
            {
              return;
            }

            this.Activity.RunOnUiThread(this.OnRefreshed);
          }
        };
      }

      MessageBus.Default.Register(this.refreshEventHandler);
      MessageBus.Default.Register(this.updateInstanceUrlEventHandler);
    }

    public override void OnDestroy()
    {
      base.OnDestroy();

      MessageBus.Default.DeRegister(this.refreshEventHandler);
      MessageBus.Default.DeRegister(this.updateInstanceUrlEventHandler);
    }
  }
}

