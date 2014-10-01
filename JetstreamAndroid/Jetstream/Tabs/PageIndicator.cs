namespace JetstreamAndroid.Tabs
{
  using Android.Support.V4.View;

  public interface PageIndicator : ViewPager.IOnPageChangeListener
	{
		/**
	     * Bind the indicator to a ViewPager.
	     *
	     * @param pager
	     */
		void SetViewPager (ViewPager pager);
	
		/**
	     * Bind the indicator to a ViewPager.
	     *
	     * @param pager
	     * @param initialPosition
	     */
		void SetViewPager (ViewPager pager, int initialPosition);
	
		/**
	     * <p>Set the current page of both the ViewPager and indicator.</p>
	     *
	     * <p>This <strong>must</strong> be used if you need to set the page before
	     * the views are drawn on screen (e.g., default start page).</p>
	     *
	     * @param item
	     */
		void SetCurrentItem (int item);
	
		/**
	     * Set a page change listener which will receive forwarded events.
	     *
	     * @param listener
	     */
		void SetOnPageChangeListener (ViewPager.IOnPageChangeListener listener);
	}
}

