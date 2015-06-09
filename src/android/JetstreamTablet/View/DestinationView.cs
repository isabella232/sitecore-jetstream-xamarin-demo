namespace Jetstream.View
{
  using System;
  using Android.Content;
  using Android.Util;
  using Android.Views;
  using Android.Views.Animations;
  using Android.Widget;
  using Java.Lang;
  using Jetstream.Map;
  using Squareup.Picasso;
  using Xamarin.Android.ObservableScrollView;
  using Xamarin.NineOldAndroids.Animations;
  using Xamarin.NineOldAndroids.Views;

  public class DestinationView : Java.Lang.Object, IObservableScrollViewCallbacks, View.IOnClickListener
  {
    private readonly Context context;

    #region Views
    private ObservableScrollView scrollView;
    private View header;
    private int flexibleSpaceImageHeight;
    private View headerBar;
    private int actionBarSize;

    private ImageView image;
    private Android.Support.V7.Widget.Toolbar toolbar;
    private TextView bodyTextView;
    private View headerBackground;
    private int prevScrollY;
    private bool gapIsChanging;
    private bool gapHidden;
    private bool isReady;
    #endregion

    public DestinationView(Context context)
    {
      this.context = context;
    }

    private View InitContentViews()
    {
      var v = LayoutInflater.From(this.context).Inflate(Jetstream.Resource.Layout.view_destination_details, null, false);

      this.flexibleSpaceImageHeight = this.context.Resources.GetDimensionPixelSize(Jetstream.Resource.Dimension.flexible_space_image_height);
      this.actionBarSize = this.GetActionBarSize();

      this.image = v.FindViewById<ImageView>(Jetstream.Resource.Id.image);
      this.header = v.FindViewById<View>(Jetstream.Resource.Id.header);
      this.headerBar = v.FindViewById<View>(Jetstream.Resource.Id.header_bar);
      this.headerBackground = v.FindViewById<View>(Jetstream.Resource.Id.header_background);

      this.toolbar = v.FindViewById<Android.Support.V7.Widget.Toolbar>(Jetstream.Resource.Id.toolbar);
      this.toolbar.SetNavigationIcon(Jetstream.Resource.Drawable.abc_ic_ab_back_mtrl_am_alpha);
      this.toolbar.SetNavigationOnClickListener(this);

      this.bodyTextView = v.FindViewById<TextView>(Jetstream.Resource.Id.text_container);

      this.scrollView = v.FindViewById<ObservableScrollView>(Jetstream.Resource.Id.scroll);
      this.scrollView.SetScrollViewCallbacks(this);

      ScrollUtils.AddOnGlobalLayoutListener(this.scrollView, new Runnable(delegate
      {
        isReady = true;
        UpdateViews(this.scrollView.CurrentScrollY, false);
      }));

      return v;
    }

    public View InitViewWithData(ClusterItem destination)
    {
      var view = this.InitContentViews();

      this.toolbar.Title = destination.Wrapped.DisplayName;
      this.bodyTextView.Text = destination.Wrapped.Overview;
      
      Picasso.With(this.context).Load(destination.ImageUrl).Into(this.image);

      return view;
    }

    private float GetHeaderTranslationY(int scrollY)
    {
      //      TODO: double to float.
      return ScrollUtils.GetFloat(-scrollY + this.flexibleSpaceImageHeight - this.header.Height, 0, (float)System.Double.MaxValue);
    }

    private void UpdateViews(int scrollY, bool animated)
    {
      // If it's ListView, onScrollChanged is called before ListView is laid out (onGlobalLayout).
      // This causes weird animation when onRestoreInstanceState occurred,
      // so we check if it's laid out already.
      if (!this.isReady)
      {
        return;
      }
      // Translate image
      ViewHelper.SetTranslationY(this.image, -scrollY / 2);

      // Translate header
      ViewHelper.SetTranslationY(this.header, this.GetHeaderTranslationY(scrollY));

      // Show/hide gap
      var headerHeight = this.headerBar.Height;
      var scrollUp = this.prevScrollY < scrollY;
      if (scrollUp)
      {
        if (this.flexibleSpaceImageHeight - headerHeight - this.actionBarSize <= scrollY)
        {
          this.ChangeHeaderBackgroundHeightAnimated(false, animated);
        }
      }
      else
      {
        if (scrollY <= this.flexibleSpaceImageHeight - headerHeight - this.actionBarSize)
        {
          this.ChangeHeaderBackgroundHeightAnimated(true, animated);
        }
      }
      this.prevScrollY = scrollY;
    }

    private void ChangeHeaderBackgroundHeight(float height, float to, float heightOnGapHidden)
    {
      var lp = (FrameLayout.LayoutParams)this.headerBackground.LayoutParameters;
      lp.Height = (int)height;
      lp.TopMargin = (int)(this.headerBar.Height - height);

      this.headerBackground.RequestLayout();
      this.gapIsChanging = (height != to);

      if (!this.gapIsChanging)
      {
        this.gapHidden = (height == heightOnGapHidden);
      }
    }

    private void ChangeHeaderBackgroundHeightAnimated(bool shouldShowGap, bool animated)
    {
      if (this.gapIsChanging)
      {
        return;
      }
      var heightOnGapShown = this.headerBar.Height;
      var heightOnGapHidden = this.headerBar.Height + this.actionBarSize;
      float from = this.headerBackground.LayoutParameters.Height;
      float to;
      if (shouldShowGap)
      {
        if (!this.gapHidden)
        {
          // Already shown
          return;
        }
        to = heightOnGapShown;
      }
      else
      {
        if (this.gapHidden)
        {
          // Already hidden
          return;
        }
        to = heightOnGapHidden;
      }
      if (animated)
      {
        Xamarin.NineOldAndroids.Views.ViewPropertyAnimator.Animate(this.headerBackground).Cancel();
        var a = ValueAnimator.OfFloat(from, to);
        a.SetDuration(100);
        a.SetInterpolator(new AccelerateDecelerateInterpolator());
        a.AddUpdateListener(new CustomAnimator(delegate(ValueAnimator animator)
        {
          var height = (float)animator.AnimatedValue;
          ChangeHeaderBackgroundHeight(height, to, heightOnGapHidden);
        }));
        a.Start();
      }
      else
      {
        this.ChangeHeaderBackgroundHeight(to, to, heightOnGapHidden);
      }
    }

    private int GetActionBarSize()
    {
      var typedValue = new TypedValue();
      int[] textSizeAttr = { Android.Resource.Attribute.ActionBarSize };

      var a = this.context.ObtainStyledAttributes(typedValue.Data, textSizeAttr);
      var barSize = a.GetDimensionPixelSize(0, -1);

      a.Recycle();
      return barSize;
    }

    public void OnScrollChanged(int scrollY, bool firstScroll, bool dragging)
    {
      this.UpdateViews(scrollY, true);
    }

    public void OnDownMotionEvent()
    {
    }

    public void OnUpOrCancelMotionEvent(Xamarin.Android.ObservableScrollView.ScrollState scrollState)
    {
    }

    public void OnClick(View v)
    {

    }
  }

  class CustomAnimator : Java.Lang.Object, ValueAnimator.IAnimatorUpdateListener
  {
    private readonly Action<ValueAnimator> action;

    public CustomAnimator(Action<ValueAnimator> action)
    {
      this.action = action;
    }

    public void OnAnimationUpdate(ValueAnimator animation)
    {
      this.action.Invoke(animation);
    }
  }
}