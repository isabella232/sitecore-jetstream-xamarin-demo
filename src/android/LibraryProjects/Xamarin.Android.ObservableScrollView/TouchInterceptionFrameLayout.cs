using Context = Android.Content.Context;
using FrameLayout = Android.Widget.FrameLayout;
using IAttributeSet = Android.Util.IAttributeSet;
using MotionEvent = Android.Views.MotionEvent;
using MotionEventActions = Android.Views.MotionEventActions;
using PointF = Android.Graphics.PointF;
using Rect = Android.Graphics.Rect;
using View = Android.Views.View;
using ViewGroup = Android.Views.ViewGroup;

namespace Xamarin.Android.ObservableScrollView
{
  using System;
  using Java.Util;

  public class TouchInterceptionFrameLayout : FrameLayout
  {
    bool mIntercepting;
    bool mDownMotionEventPended;
    bool mBeganFromDownMotionEvent;
    bool mChildrenEventsCanceled;
    PointF mInitialPoint;
    MotionEvent mPendingDownMotionEvent;
    ITouchInterceptionListener mTouchInterceptionListener;

    private const int MAX_CLICK_DURATION = 400;
    private const int MAX_CLICK_DISTANCE = 5;
    private long pressStartTime;
    private bool stayedWithinClickDistance;

    public TouchInterceptionFrameLayout(Context context)
      : base(context)
    {
    }

    public TouchInterceptionFrameLayout(Context context, IAttributeSet attrs)
      : base(context, attrs)
    {
    }

    public TouchInterceptionFrameLayout(Context context, IAttributeSet attrs, int defStyleAttr)
      : base(context, attrs, defStyleAttr)
    {
    }

    //		@TargetApi(Build.VERSION_CODES.LOLLIPOP)
    //		public TouchInterceptionFrameLayout(Context context, AttributeSet attrs, int defStyleAttr, int defStyleRes) {
    //			super(context, attrs, defStyleAttr, defStyleRes);
    //		}
    public void SetScrollInterceptionListener(ITouchInterceptionListener listener)
    {
      this.mTouchInterceptionListener = listener;
    }

    public override bool OnInterceptTouchEvent(MotionEvent ev)
    {
      if (this.mTouchInterceptionListener == null)
        return false;

      // In here, we must initialize touch state variables
      // and ask if we should intercept this event.
      // Whether we should intercept or not is kept for the later event handling.
      switch (ev.ActionMasked)
      {
        case MotionEventActions.Down:
          this.mInitialPoint = new PointF(ev.GetX(), ev.GetY());
          this.mPendingDownMotionEvent = MotionEvent.ObtainNoHistory(ev);
          this.mDownMotionEventPended = true;
          this.mIntercepting = this.mTouchInterceptionListener.ShouldInterceptTouchEvent(ev, false, 0, 0);
          this.mBeganFromDownMotionEvent = this.mIntercepting;
          this.mChildrenEventsCanceled = false;
          return this.mIntercepting;
        case MotionEventActions.Move:
          // ACTION_MOVE will be passed suddenly, so initialize to avoid exception.
          if (this.mInitialPoint == null)
          {
            this.mInitialPoint = new PointF(ev.GetX(), ev.GetY());
          }
          // diffX and diffY are the origin of the motion, and should be difference
          // from the position of the ACTION_DOWN event occurred.
          float diffX = ev.GetX() - this.mInitialPoint.X;
          float diffY = ev.GetY() - this.mInitialPoint.Y;
          this.mIntercepting = this.mTouchInterceptionListener.ShouldInterceptTouchEvent(ev, true, diffX, diffY);
          return this.mIntercepting;
      }
      return false;
    }

    public override bool OnTouchEvent(MotionEvent ev)
    {
      if (this.mTouchInterceptionListener == null)
        return base.OnTouchEvent(ev);

      switch (ev.ActionMasked)
      {
        case MotionEventActions.Down:
          this.pressStartTime = Calendar.Instance.TimeInMillis;
          this.stayedWithinClickDistance = true;

          if (this.mIntercepting)
          {
            this.mTouchInterceptionListener.OnDownMotionEvent(ev);
            this.DuplicateTouchEventForChildren(ev);
            return true;
          }
          break;
        case MotionEventActions.Move:
          // ACTION_MOVE will be passed suddenly, so initialize to avoid exception.
          if (this.mInitialPoint == null)
          {
            this.mInitialPoint = new PointF(ev.GetX(), ev.GetY());
          }

          // diffX and diffY are the origin of the motion, and should be difference
          // from the position of the ACTION_DOWN event occurred.
          float diffX = ev.GetX() - this.mInitialPoint.X;
          float diffY = ev.GetY() - this.mInitialPoint.Y;

          if (this.stayedWithinClickDistance && this.Distance(diffX, diffY) > MAX_CLICK_DISTANCE)
          {
            this.stayedWithinClickDistance = false;
          }

          this.mIntercepting = this.mTouchInterceptionListener.ShouldInterceptTouchEvent(ev, true, diffX, diffY);
          if (this.mIntercepting)
          {
            // If this layout didn't receive ACTION_DOWN motion event,
            // we should generate ACTION_DOWN event with current position.
            if (!this.mBeganFromDownMotionEvent)
            {
              this.mBeganFromDownMotionEvent = true;

              MotionEvent @event = MotionEvent.ObtainNoHistory(this.mPendingDownMotionEvent);
              @event.SetLocation(ev.GetX(), ev.GetY());
              this.mTouchInterceptionListener.OnDownMotionEvent(@event);

              this.mInitialPoint = new PointF(ev.GetX(), ev.GetY());
              diffX = diffY = 0;
            }

            // Children's touches should be canceled
            if (!this.mChildrenEventsCanceled)
            {
              this.mChildrenEventsCanceled = true;
              this.DuplicateTouchEventForChildren(this.ObtainMotionEvent(ev, MotionEventActions.Cancel));
            }

            this.mTouchInterceptionListener.OnMoveMotionEvent(ev, diffX, diffY);

            // If next mIntercepting become false,
            // then we should generate fake ACTION_DOWN event.
            // Therefore we set pending flag to true as if this is a down motion event.
            this.mDownMotionEventPended = true;

            // Whether or not this event is consumed by the listener,
            // assume it consumed because we declared to intercept the event.
            return true;
          }
          else
          {
            if (this.mDownMotionEventPended)
            {
              this.mDownMotionEventPended = false;
              MotionEvent @event = MotionEvent.ObtainNoHistory(this.mPendingDownMotionEvent);
              @event.SetLocation(ev.GetX(), ev.GetY());
              this.DuplicateTouchEventForChildren(ev, @event);
            }
            else
            {
              this.DuplicateTouchEventForChildren(ev);
            }

            // If next mIntercepting become true,
            // then we should generate fake ACTION_DOWN event.
            // Therefore we set beganFromDownMotionEvent flag to false
            // as if we haven't received a down motion event.
            this.mBeganFromDownMotionEvent = false;

            // Reserve children's click cancellation here if they've already canceled
            this.mChildrenEventsCanceled = false;
          }
          break;
        case MotionEventActions.Up:
        case MotionEventActions.Cancel:
          this.mBeganFromDownMotionEvent = false;
          if (this.mIntercepting)
          {
            this.mTouchInterceptionListener.OnUpOrCancelMotionEvent(ev);
          }

          long pressDuration = Calendar.Instance.TimeInMillis - this.pressStartTime;
          if (pressDuration < MAX_CLICK_DURATION && this.stayedWithinClickDistance)
          {
            this.PerformClickOnChildren(this, ev);
          }

          // Children's touches should be canceled regardless of
          // whether or not this layout intercepted the consecutive motion events.
          if (!this.mChildrenEventsCanceled)
          {
            this.mChildrenEventsCanceled = true;
            if (this.mDownMotionEventPended)
            {
              this.mDownMotionEventPended = false;
              MotionEvent @event = MotionEvent.ObtainNoHistory(this.mPendingDownMotionEvent);
              @event.SetLocation(ev.GetX(), ev.GetY());
              this.DuplicateTouchEventForChildren(ev, @event);
            }
            else
            {
              this.DuplicateTouchEventForChildren(ev);
            }
          }
          return true;
      }
      return base.OnTouchEvent(ev);
    }

    private void PerformClickOnChildren(View view, MotionEvent ev)
    {
      if (view is ViewGroup)
      {
        ViewGroup viewGroup = (ViewGroup)view;
        if (viewGroup.ChildCount > 0)
        {
          for (int i = 0; i < viewGroup.ChildCount; i++)
          {
            this.PerformClickOnChildren(viewGroup.GetChildAt(i), ev);
          }
        }
      }
      else
      {
        Rect childRect = new Rect();
        view.GetHitRect(childRect);

        if (childRect.Contains((int)ev.GetX(), (int)ev.GetY()))
        {
          view.PerformClick();
        }
      }
    }


    private float Distance(float diffX, float diffY)
    {
      float distanceInPx = (float)Math.Sqrt(diffX * diffX + diffY * diffY);
      return this.PxToDp(distanceInPx);
    }

    private float PxToDp(float px)
    {
      return px / this.Resources.DisplayMetrics.Density;
    }

    private MotionEvent ObtainMotionEvent(MotionEvent evBase, MotionEventActions action)
    {
      var ev = MotionEvent.ObtainNoHistory(evBase);
      ev.Action = action;
      return ev;
    }

    /**
     * Duplicate touch events to child views.
     * We want to dispatch a down motion event and the move events to
     * child views, but calling dispatchTouchEvent() causes StackOverflowError.
     * Therefore we do it manually.
     */
    private void DuplicateTouchEventForChildren(MotionEvent ev, params MotionEvent[] pendingEvents)
    {
      if (ev == null)
      {
        return;
      }
      for (int i = this.ChildCount - 1; 0 <= i; i--)
      {
        View childView = this.GetChildAt(i);
        if (childView != null)
        {
          Rect childRect = new Rect();
          childView.GetHitRect(childRect);
          MotionEvent @event = MotionEvent.ObtainNoHistory(ev);
          if (!childRect.Contains((int)@event.GetX(), (int)@event.GetY()))
          {
            continue;
          }
          float offsetX = -childView.Left;
          float offsetY = -childView.Top;
          bool consumed = false;
          if (pendingEvents != null)
          {
            foreach (MotionEvent pe in pendingEvents)
            {
              if (pe != null)
              {
                MotionEvent peAdjusted = MotionEvent.ObtainNoHistory(pe);
                peAdjusted.OffsetLocation(offsetX, offsetY);
                consumed |= childView.DispatchTouchEvent(peAdjusted);
              }
            }
          }
          @event.OffsetLocation(offsetX, offsetY);
          consumed |= childView.DispatchTouchEvent(@event);
          if (consumed)
          {
            break;
          }
        }
      }
    }
  }
}

