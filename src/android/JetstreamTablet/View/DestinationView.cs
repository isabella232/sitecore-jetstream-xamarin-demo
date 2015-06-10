namespace Jetstream.View
{
  using System;
  using Android.Content;
  using Android.Text;
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

  public class DestinationView : Java.Lang.Object, IObservableScrollViewCallbacks, View.IOnClickListener, ITouchInterceptionListener
  {
    public Action OnBackButtonClicked { get; set; }

    private readonly Context context;

    #region Views

    private View headerBar;
    private ImageView destinationImageView;
    private View headerBackground;
    private TextView bodyTextView;
    private ObservableScrollView scrollView;
    private Android.Support.V7.Widget.Toolbar toolbar;
    private TouchInterceptionFrameLayout interceptionFrameLayout;

    #endregion

    // Fields that needs to saved
    private float mInitialTranslationY;

    // Fields that just keep constants like resource values
    private int actionBarSize;
    private int flexibleSpaceImageHeight;
    private int intersectionHeight;
    private int headerBarHeight;

    // Temporary states
    private float scrollYOnDownMotion;

    private float prevTranslationY;
    private bool isGapChanging;
    private bool isGapHidden;
    private bool isReady;

    public DestinationView(Context context)
    {
      this.context = context;
    }

    private View InitContentViews()
    {
      var v = LayoutInflater.From(this.context).Inflate(Jetstream.Resource.Layout.view_destination_details, null, false);

      this.toolbar = v.FindViewById<Android.Support.V7.Widget.Toolbar>(Jetstream.Resource.Id.toolbar);
      this.toolbar.SetNavigationIcon(Jetstream.Resource.Drawable.abc_ic_ab_back_mtrl_am_alpha);
      this.toolbar.SetNavigationOnClickListener(this);

      this.flexibleSpaceImageHeight = this.context.Resources.GetDimensionPixelSize(Jetstream.Resource.Dimension.flexible_space_image_height);
      this.actionBarSize = this.GetActionBarSize();
      this.headerBarHeight = this.context.Resources.GetDimensionPixelSize(Jetstream.Resource.Dimension.header_bar_height);
      // Even when the top gap has began to change, header bar still can move
      // within intersectionHeight.
      this.intersectionHeight = this.context.Resources.GetDimensionPixelSize(Jetstream.Resource.Dimension.intersection_height);

      this.bodyTextView = v.FindViewById<TextView>(Jetstream.Resource.Id.text_container);
      this.destinationImageView = v.FindViewById<ImageView>(Jetstream.Resource.Id.image);
      this.headerBar = v.FindViewById<View>(Jetstream.Resource.Id.header_bar);
      this.headerBackground = v.FindViewById<View>(Jetstream.Resource.Id.header_background);

      this.scrollView = v.FindViewById<ObservableScrollView>(Jetstream.Resource.Id.scroll);
      this.scrollView.SetScrollViewCallbacks(this);

      this.interceptionFrameLayout = v.FindViewById<TouchInterceptionFrameLayout>(Jetstream.Resource.Id.scroll_wrapper);
      this.interceptionFrameLayout.SetScrollInterceptionListener(this);

      ViewHelper.SetTranslationY(this.toolbar, (this.headerBarHeight - this.actionBarSize) / 2);
      this.mInitialTranslationY = this.GetImageTransition();

      ScrollUtils.AddOnGlobalLayoutListener(this.interceptionFrameLayout, new Runnable(delegate
      {
        isReady = true;
        UpdateViews(mInitialTranslationY, false);
      }));

      return v;
    }

    public View InitViewWithData(ClusterItem destination)
    {
      var view = this.InitContentViews();

      this.toolbar.Title = destination.Wrapped.DisplayName;
      this.bodyTextView.Text = Html.FromHtml(destination.Wrapped.Overview).ToString();

      Picasso.With(this.context).Load(destination.ImageUrl).Into(this.destinationImageView);

      return view;
    }

    private void UpdateViews(float translationY, bool animated)
    {
      // If it's ListView, onScrollChanged is called before ListView is laid out (onGlobalLayout).
      // This causes weird animation when onRestoreInstanceState occurred,
      // so we check if it's laid out already.
      if (!this.isReady)
      {
        return;
      }
      ViewHelper.SetTranslationY(this.interceptionFrameLayout, translationY);

      // Translate image
      ViewHelper.SetTranslationY(this.destinationImageView, (translationY - (this.GetImageTransition())) / 2);

      // Translate title
      ViewHelper.SetTranslationY(this.toolbar, System.Math.Min(this.intersectionHeight, (this.headerBarHeight - this.actionBarSize) / 2));

      // Show/hide gap
      var scrollUp = translationY < this.prevTranslationY;
      if (scrollUp)
      {
        if (translationY <= this.actionBarSize)
        {
          this.ChangeHeaderBackgroundHeightAnimated(false, animated);
        }
      }
      else
      {
        if (this.actionBarSize <= translationY)
        {
          this.ChangeHeaderBackgroundHeightAnimated(true, animated);
        }
      }
      this.prevTranslationY = translationY;
    }

    private int GetImageTransition()
    {
      return this.flexibleSpaceImageHeight + this.headerBarHeight;
    }

    private void ChangeHeaderBackgroundHeight(float height, float to, float heightOnGapHidden)
    {
      var lp = (FrameLayout.LayoutParams)this.headerBackground.LayoutParameters;
      lp.Height = (int)height;
      lp.TopMargin = (int)(this.headerBar.Height - height);

      this.headerBackground.RequestLayout();
      this.isGapChanging = (height != to);

      if (!this.isGapChanging)
      {
        this.isGapHidden = (height == heightOnGapHidden);
      }
    }

    private void ChangeHeaderBackgroundHeightAnimated(bool shouldShowGap, bool animated)
    {
      if (this.isGapChanging)
      {
        return;
      }
      int heightOnGapShown = this.headerBar.Height;
      int heightOnGapHidden = this.headerBar.Height + this.actionBarSize;
      float from = this.headerBackground.LayoutParameters.Height;
      float to;

      if (shouldShowGap)
      {
        if (!this.isGapHidden)
        {
          // Already shown
          return;
        }
        to = heightOnGapShown;
      }
      else
      {
        if (this.isGapHidden)
        {
          // Already hidden
          return;
        }
        to = heightOnGapHidden;
      }
      if (animated)
      {
        Xamarin.NineOldAndroids.Views.ViewPropertyAnimator.Animate(this.headerBackground).Cancel();

        ValueAnimator animator = ValueAnimator.OfFloat(from, to);
        animator.SetDuration(100);
        animator.SetInterpolator(new AccelerateDecelerateInterpolator());
        animator.AddUpdateListener(new CustomAnimator(delegate(ValueAnimator a)
        {
          var height = (float)a.AnimatedValue;
          ChangeHeaderBackgroundHeight(height, to, heightOnGapHidden);
        }));

        animator.Start();
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
    }

    public void OnDownMotionEvent()
    {
    }

    public void OnUpOrCancelMotionEvent(Xamarin.Android.ObservableScrollView.ScrollState scrollState)
    {
    }

    public void OnClick(View v)
    {
      if (this.OnBackButtonClicked != null)
      {
        this.OnBackButtonClicked.Invoke();
      }
    }

    private float GetMinInterceptionLayoutY()
    {
      //      return this.actionBarSize - this.intersectionHeight;
      // If you want to move header bar to the top, return 0 instead.
      return 0;
    }

    public bool ShouldInterceptTouchEvent(MotionEvent ev, bool moving, float diffX, float diffY)
    {
      return this.GetMinInterceptionLayoutY() < (int)ViewHelper.GetY(this.interceptionFrameLayout)
                    || (moving && this.scrollView.CurrentScrollY - diffY < 0);
    }

    public void OnDownMotionEvent(MotionEvent ev)
    {
      this.scrollYOnDownMotion = this.scrollView.CurrentScrollY;
    }

    public void OnMoveMotionEvent(MotionEvent ev, float diffX, float diffY)
    {
      float translationY = ViewHelper.GetTranslationY(this.interceptionFrameLayout) - this.scrollYOnDownMotion + diffY;
      float minTranslationY = this.GetMinInterceptionLayoutY();
      if (translationY < minTranslationY)
      {
        translationY = minTranslationY;
      }
      else if (this.GetImageTransition() < translationY)
      {
        translationY = this.GetImageTransition();
      }

      this.UpdateViews(translationY, true);
    }

    public void OnUpOrCancelMotionEvent(MotionEvent ev)
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