
using MotionEvent = Android.Views.MotionEvent;

namespace Xamarin.Android.ObservableScrollView
{
  public interface ITouchInterceptionListener
	{
		/**
         * Determines whether the layout should intercept this event.
         *
         * @param ev     motion event
         * @param moving true if this event is ACTION_MOVE type
         * @param diffX  difference between previous X and current X, if moving is true
         * @param diffY  difference between previous Y and current Y, if moving is true
         * @return true if the layout should intercept
         */
		bool ShouldInterceptTouchEvent(MotionEvent ev, bool moving, float diffX, float diffY);

		/**
         * Called if the down motion event is intercepted by this layout.
         *
         * @param ev motion event
         */
		void OnDownMotionEvent(MotionEvent ev);

		/**
         * Called if the move motion event is intercepted by this layout.
         *
         * @param ev    motion event
         * @param diffX difference between previous X and current X
         * @param diffY difference between previous Y and current Y
         */
		void OnMoveMotionEvent(MotionEvent ev, float diffX, float diffY);

		/**
         * Called if the up (or cancel) motion event is intercepted by this layout.
         *
         * @param ev motion event
         */
		void OnUpOrCancelMotionEvent(MotionEvent ev);
	}
}

