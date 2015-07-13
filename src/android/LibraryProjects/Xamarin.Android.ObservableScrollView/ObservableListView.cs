using AbsListView = Android.Widget.AbsListView;
using Context = Android.Content.Context;
using IAttributeSet = Android.Util.IAttributeSet;
using IParcelable = Android.OS.IParcelable;
using ListView = Android.Widget.ListView;
using MotionEvent = Android.Views.MotionEvent;
using MotionEventActions = Android.Views.MotionEventActions;
using SparseIntArray = Android.Util.SparseIntArray;
using View = Android.Views.View;
using ViewGroup = Android.Views.ViewGroup;

namespace Xamarin.Android.ObservableScrollView
{
  public class ObservableListView : ListView, IScrollable, IScrollChangeTarget
	{
		// Fields that should be saved onSaveInstanceState
		int mPrevFirstVisiblePosition;
		int mPrevFirstVisibleChildHeight = -1;
		int mPrevScrolledChildrenHeight;
		int mPrevScrollY;
		int mScrollY;
		SparseIntArray mChildrenHeights;

		// Fields that don't need to be saved onSaveInstanceState
		IObservableScrollViewCallbacks mCallbacks;
		ScrollState mScrollState;
		bool mFirstScroll;
		bool mDragging;
		bool mIntercepted;
		MotionEvent mPrevMoveEvent;
		ViewGroup mTouchInterceptionViewGroup;
		IOnScrollListener mOriginalScrollListener;
		IOnScrollListener mScrollListener;

		public IOnScrollListener OriginalScrollListener { 
			get { return this.mOriginalScrollListener; }
		}

		public ObservableListView (Context context) :
			base (context)
		{
			this.Initialize ();
		}

		public ObservableListView (Context context, IAttributeSet attrs) :
			base (context, attrs)
		{
			this.Initialize ();
		}

		public ObservableListView (Context context, IAttributeSet attrs, int defStyle) :
			base (context, attrs, defStyle)
		{
			this.Initialize ();
		}

		void Initialize ()
		{
			this.mChildrenHeights = new SparseIntArray ();
			this.mScrollListener = new ObservableScrollListener (this);
			base.SetOnScrollListener (this.mScrollListener);
		}

		public int CurrentScrollY { get { return this.mScrollY; } }

		public void ScrollVerticallyTo (int y)
		{	
			View firstVisibleChild = this.GetChildAt(0);
			if (firstVisibleChild != null) {
				int baseHeight = firstVisibleChild.Height;
				int position = y / baseHeight;
				this.SetSelection(position);
			}
		}

		public override void OnRestoreInstanceState (IParcelable state)
		{
			CompleteSavedState ss = (CompleteSavedState)state;
			this.mPrevFirstVisiblePosition = ss.PrevFirstVisiblePosition;
			this.mPrevFirstVisibleChildHeight = ss.PrevFirstVisibleChildHeight;
			this.mPrevScrolledChildrenHeight = ss.PrevScrolledChildrenHeight;
			this.mPrevScrollY = ss.PrevScrollY;
			this.mScrollY = ss.ScrollY;
			this.mChildrenHeights = ss.ChildrenHeights;
			base.OnRestoreInstanceState (ss.SuperState);
		}

		public override IParcelable OnSaveInstanceState ()
		{
			IParcelable superState = base.OnSaveInstanceState ();
			CompleteSavedState ss = new CompleteSavedState (superState);
			ss.PrevFirstVisiblePosition = this.mPrevFirstVisiblePosition;
			ss.PrevFirstVisibleChildHeight = this.mPrevFirstVisibleChildHeight;
			ss.PrevScrolledChildrenHeight = this.mPrevScrolledChildrenHeight;
			ss.PrevScrollY = this.mPrevScrollY;
			ss.ScrollY = this.mScrollY;
			ss.ChildrenHeights = this.mChildrenHeights;
			return ss;
		}

		public override bool OnInterceptTouchEvent (MotionEvent ev)
		{
			if (this.mCallbacks != null) {
				switch (ev.ActionMasked) {
				case MotionEventActions.Down:
					// Whether or not motion events are consumed by children,
					// flag initializations which are related to ACTION_DOWN events should be executed.
					// Because if the ACTION_DOWN is consumed by children and only ACTION_MOVEs are
					// passed to parent (this view), the flags will be invalid.
					// Also, applications might implement initialization codes to onDownMotionEvent,
					// so call it here.
					this.mFirstScroll = this.mDragging = true;
					this.mCallbacks.OnDownMotionEvent ();
					break;
				}
			}
			return base.OnInterceptTouchEvent (ev);
		}

		public override bool OnTouchEvent (MotionEvent ev)
		{
			if (this.mCallbacks == null)
				return base.OnTouchEvent (ev);

			switch (ev.ActionMasked) {
			case MotionEventActions.Up:
			case MotionEventActions.Cancel:
				this.mIntercepted = false;
				this.mDragging = false;
				this.mCallbacks.OnUpOrCancelMotionEvent (this.mScrollState);
				break;
			case MotionEventActions.Move:
				if (this.mPrevMoveEvent == null) {
					this.mPrevMoveEvent = ev;
				} 
				float diffY = ev.GetY () - this.mPrevMoveEvent.GetY ();
				this.mPrevMoveEvent = MotionEvent.ObtainNoHistory (ev);

				if (this.CurrentScrollY - diffY <= 0) {
					// Can't scroll anymore.

					if (this.mIntercepted) {	
						// Already dispatched ACTION_DOWN event to parents, so stop here.
						return false;
					}

					// Apps can set the interception target other than the direct parent.
					ViewGroup parent;
					if (this.mTouchInterceptionViewGroup == null) {
						parent = (ViewGroup)this.Parent;
					} else {
						parent = this.mTouchInterceptionViewGroup;
					}

					// Get offset to parents. If the parent is not the direct parent,
					// we should aggregate offsets from all of the parents.
					float offsetX = 0;
					float offsetY = 0;
					for (View v = this; v != null && v != parent; v = (View)v.Parent) {
						offsetX += v.Left - v.ScrollX;
						offsetY += v.Top - v.ScrollY;
					}
					MotionEvent @event = MotionEvent.ObtainNoHistory (ev);
					@event.OffsetLocation (offsetX, offsetY);

					if (parent.OnInterceptTouchEvent (@event)) {
						this.mIntercepted = true;

						// If the parent wants to intercept ACTION_MOVE events,
						// we pass ACTION_DOWN event to the parent
						// as if these touch events just have began now.
						@event.Action = (MotionEventActions.Down);

						this.Post (() => parent.DispatchTouchEvent (@event));
						// Return this onTouchEvent() first and set ACTION_DOWN event for parent
						// to the queue, to keep events sequence.

						return false;
					}
					// Even when this can't be scrolled anymore,
					// simply returning false here may cause subView's click,
					// so delegate it to super.
					return base.OnTouchEvent (ev);
				}
				break;
			}

			return base.OnTouchEvent (ev);
		}

		public override void SetOnScrollListener (IOnScrollListener l)
		{
			// Don't set l to super.setOnScrollListener().
			// l receives all events through mScrollListener.
			this.mOriginalScrollListener = l;
		}

		public void SetTouchInterceptionViewGroup(ViewGroup viewGroup) {
			this.mTouchInterceptionViewGroup = viewGroup;
		}

		public void SetScrollViewCallbacks(IObservableScrollViewCallbacks listener) {
			this.mCallbacks = listener;
		}



		public void OnScrollChanged()
		{
			if (this.mCallbacks == null)
				return;

			if (this.ChildCount > 0) {
				int firstVisiblePosition = this.FirstVisiblePosition;
				for (int i = this.FirstVisiblePosition, j = 0; i <= this.LastVisiblePosition; i++, j++) {
					if (this.mChildrenHeights.IndexOfKey(i) < 0 || this.GetChildAt(j).Height != this.mChildrenHeights.Get(i)) {
						this.mChildrenHeights.Put(i, this.GetChildAt(j).Height);
					}
				}

				View firstVisibleChild = this.GetChildAt(0);
				if (firstVisibleChild != null) {
					if (this.mPrevFirstVisiblePosition < firstVisiblePosition) {
						// scroll down
						int skippedChildrenHeight = 0;
						if (firstVisiblePosition - this.mPrevFirstVisiblePosition != 1) {
							for (int i = firstVisiblePosition - 1; i > this.mPrevFirstVisiblePosition; i--) {
								if (0 < this.mChildrenHeights.IndexOfKey(i)) {
									skippedChildrenHeight += this.mChildrenHeights.Get(i);
								}
							}
						}
						this.mPrevScrolledChildrenHeight += this.mPrevFirstVisibleChildHeight + skippedChildrenHeight;
						this.mPrevFirstVisibleChildHeight = firstVisibleChild.Height;
					} else if (firstVisiblePosition < this.mPrevFirstVisiblePosition) {
						// scroll up
						int skippedChildrenHeight = 0;
						if (this.mPrevFirstVisiblePosition - firstVisiblePosition != 1) {
							for (int i = this.mPrevFirstVisiblePosition - 1; i > firstVisiblePosition; i--) {
								if (0 < this.mChildrenHeights.IndexOfKey(i)) {
									skippedChildrenHeight += this.mChildrenHeights.Get(i);
								}
							}
						}
						this.mPrevScrolledChildrenHeight -= firstVisibleChild.Height + skippedChildrenHeight;
						this.mPrevFirstVisibleChildHeight = firstVisibleChild.Height;
					} else if (firstVisiblePosition == 0) {
						this.mPrevFirstVisibleChildHeight = firstVisibleChild.Height;
					}
					if (this.mPrevFirstVisibleChildHeight < 0) {
						this.mPrevFirstVisibleChildHeight = 0;
					}
					this.mScrollY = this.mPrevScrolledChildrenHeight - firstVisibleChild.Top;
					this.mPrevFirstVisiblePosition = firstVisiblePosition;

					this.mCallbacks.OnScrollChanged(this.mScrollY, this.mFirstScroll, this.mDragging);
					if (this.mFirstScroll) {
						this.mFirstScroll = false;
					}

					if (this.mPrevScrollY < this.mScrollY) {
						this.mScrollState = ScrollState.Up;
					} else if (this.mScrollY < this.mPrevScrollY) {
						this.mScrollState = ScrollState.Down;
					} else {
						this.mScrollState = ScrollState.Stop;
					}
					this.mPrevScrollY = this.mScrollY;
				}
			}
		}

	}
}

