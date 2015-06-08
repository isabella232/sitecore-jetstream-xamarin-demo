namespace Xamarin.Android.ObservableScrollView
{
  public enum ScrollState
	{
		/**
     * Widget is stopped.
     * This state does not always mean that this widget have never been scrolled.
     */
		Stop,

		/**
     * Widget is scrolled up by swiping it down.
     */
		Up,

		/**
     * Widget is scrolled down by swiping it up.
     */
		Down,
	}
}

