using AbsListView = Android.Widget.AbsListView;
using Widget_ScrollState = Android.Widget.ScrollState;

namespace Xamarin.Android.ObservableScrollView
{
  internal interface IScrollChangeTarget
	{
		void OnScrollChanged();

		AbsListView.IOnScrollListener OriginalScrollListener { get;}
	}

	internal class ObservableScrollListener : Java.Lang.Object, AbsListView.IOnScrollListener
	{
	  readonly IScrollChangeTarget target;

		internal ObservableScrollListener (IScrollChangeTarget target)
		{
			this.target = target;
		}

		public void OnScrollStateChanged(AbsListView view, Widget_ScrollState scrollState) {
			if (this.target.OriginalScrollListener != null) {
				this.target.OriginalScrollListener.OnScrollStateChanged(view, scrollState);
			}
		}
			
		public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount) {
			if (this.target.OriginalScrollListener != null) {
				this.target.OriginalScrollListener.OnScroll(view, firstVisibleItem, visibleItemCount, totalItemCount);
			}
			// AbsListView#invokeOnItemScrollListener calls onScrollChanged(0, 0, 0, 0)
			// on Android 4.0+, but Android 2.3 is not. (Android 3.0 is unknown)
			// So call it with onScrollListener.
			this.target.OnScrollChanged();
		}
	}
}

