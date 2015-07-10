
namespace Xamarin.Android.ObservableScrollView
{
	public interface IObservableScrollViewCallbacks
	{
		/**
	     * Called when the scroll change events occurred.
	     * This won't be called just after the view is laid out, so if you'd like to
	     * initialize the position of your views with this method, you should call this manually
	     * or invoke scroll as appropriate.
	     */
		void OnScrollChanged (int scrollY, bool firstScroll, bool dragging);

		/**
	     * Called when the down motion event occurred.
	     */
		void OnDownMotionEvent ();

		/**
	     * Called when the dragging ended or canceled.
	     */
		void OnUpOrCancelMotionEvent (ScrollState scrollState);
	}
}

