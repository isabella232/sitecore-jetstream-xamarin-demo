
using ViewGroup = Android.Views.ViewGroup;

namespace Xamarin.Android.ObservableScrollView
{
  public interface IScrollable
	{
		/**
	     * Sets a callback listener.
	     *
	     */
		void SetScrollViewCallbacks (IObservableScrollViewCallbacks listener);

		/**
		 * Scrolls vertically to the absolute Y.
		 * Implemented classes are expected to scroll to the exact Y pixels from the top,
		 * but it depends on the type of the widget.
		 *
		 * @param y vertical position to scroll to
		 */
		void ScrollVerticallyTo (int y);

		/**
	     * Returns the current Y of the scrollable view.
	     *
	     * @return current Y pixel
	     */
		int CurrentScrollY{ get; }

		/**
	     * Sets a touch motion event delegation ViewGroup.
	     * This is used to pass motion events back to parent view.
	     * It's up to the implementation classes whether or not it works.
	     *
	     * @param viewGroup ViewGroup object to dispatch motion events
	     */
		void SetTouchInterceptionViewGroup (ViewGroup viewGroup);
	}
}

