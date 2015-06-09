using Build = Android.OS.Build;
using BuildVersionCodes = Android.OS.BuildVersionCodes;
using View = Android.Views.View;
using ViewTreeObserver = Android.Views.ViewTreeObserver;

namespace Xamarin.Android.ObservableScrollView
{
  using Java.Lang;

  internal class GlobalLayoutListener : Object, ViewTreeObserver.IOnGlobalLayoutListener
	{
    readonly Runnable runnable;
		View view;

		public GlobalLayoutListener (Runnable runnable, View provider)
		{
			this.runnable = runnable;
			this.view = provider;

		}

		public void OnGlobalLayout ()
		{
			if (Build.VERSION.SdkInt < BuildVersionCodes.JellyBean) {
				this.view.ViewTreeObserver.RemoveGlobalOnLayoutListener (this);
			} else {
				this.view.ViewTreeObserver.RemoveOnGlobalLayoutListener (this);
			}
			this.runnable.Run ();
		}

		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);
			this.view = null;
		}
	}
}

