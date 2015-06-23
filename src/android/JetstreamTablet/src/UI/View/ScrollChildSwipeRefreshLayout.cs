namespace Jetstream.UI.View
{
  using System.Collections.Generic;
  using System.Linq;
  using Android.Content;
  using Android.Support.V4.View;
  using Android.Support.V4.Widget;
  using Android.Util;
  using Android.Views;

  public class ScrollChildSwipeRefreshLayout : SwipeRefreshLayout
	{
		public ScrollChildSwipeRefreshLayout (Context context) : base (context)
		{
		}

		public ScrollChildSwipeRefreshLayout (Context context, IAttributeSet attrs) : base (context, attrs)
		{
		}

		// The current SwipeRefreshLayout only check its immediate child scrollability.
		// In our case, ListFragment uses a ListView inside a parent FrameLayout which breaks this.
		public override bool CanChildScrollUp ()
		{
			return this.GetChildrenRecursively (this.GetChildAt (0)).Any (v => ViewCompat.CanScrollVertically (v, -1));
		}

		public IEnumerable<View> GetChildrenRecursively (View v)
		{
			if (v == null)
				yield break;

			yield return v;
			var grp = v as ViewGroup;
			if (grp != null) {
				for (int i = 0; i < grp.ChildCount; i++)
					foreach (var c in this.GetChildrenRecursively (grp.GetChildAt (i)))
						yield return c;
			}
		}
	}
}

