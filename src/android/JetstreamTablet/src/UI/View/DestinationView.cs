namespace Jetstream.UI.View
{
  using System;
  using System.Linq;
  using Android.Graphics;
  using Android.Graphics.Drawables;
  using Android.Support.V7.Graphics;
  using Android.Text;
  using Android.Util;
  using Android.Views;
  using Android.Views.Animations;
  using Android.Widget;
  using Java.Lang;
  using Jetstream.Models;
  using Jetstream.UI.Activities;
  using Square.Picasso;
  using Xamarin.Android.ObservableScrollView;
  using Xamarin.NineOldAndroids.Animations;
  using Xamarin.NineOldAndroids.Views;

  public class DestinationView : Java.Lang.Object, IObservableScrollViewCallbacks, View.IOnClickListener, ITouchInterceptionListener, Palette.IPaletteAsyncListener
  {
//    Maximum distance that will be handled while performing touch event.
    private const float MaxDiffYAcceptable = 200;

    public Action OnBackButtonClicked { get; set; }

    private readonly DestinationActivity activity;
    private readonly DestinationAndroidSpec destination;

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

    private bool isFromToolbarTouch = false;

    public DestinationView(DestinationActivity activity, DestinationAndroidSpec destination)
    {
      this.activity = activity;
      this.destination = destination;
    }

    public void OnClick(View v)
    {
      if (this.OnBackButtonClicked != null)
      {
        this.OnBackButtonClicked.Invoke();
      }
    }

    public void InitViews()
    {
      this.InitContentViews();



      Picasso.With(this.activity).Load(this.destination.ImageUrl).Into(new DestinationImageTarget(this.destinationImageView, this));
    }

    public void OnGenerated(Palette palette)
    {
      if (palette == null)
      {
        return;
      }

      var maxSwatch = palette.Swatches.OrderByDescending(swatch => swatch.Population).First();

      var color = new Color(maxSwatch.Rgb);
      this.toolbar.SetBackgroundColor(color);
      this.headerBar.SetBackgroundColor(color);
      this.headerBackground.SetBackgroundColor(color);
    }

    #region Views operations internals
    private void InitContentViews()
    {
      this.toolbar = this.activity.FindViewById<Android.Support.V7.Widget.Toolbar>(Jetstream.Resource.Id.toolbar);

      this.toolbar.Title = this.destination.DisplayName;
      this.activity.SetSupportActionBar(this.toolbar);

      this.toolbar.SetNavigationIcon(Jetstream.Resource.Drawable.abc_ic_ab_back_mtrl_am_alpha);      
      this.toolbar.SetNavigationOnClickListener(this);

      this.flexibleSpaceImageHeight = this.activity.Resources.GetDimensionPixelSize(Jetstream.Resource.Dimension.flexible_space_image_height);
      this.actionBarSize = this.GetActionBarSize();
      this.headerBarHeight = this.activity.Resources.GetDimensionPixelSize(Jetstream.Resource.Dimension.header_bar_height);
      
      // Even when the top gap has began to change, header bar still can move
      // within intersectionHeight.
      this.intersectionHeight = this.activity.Resources.GetDimensionPixelSize(Jetstream.Resource.Dimension.intersection_height);

      this.bodyTextView = this.activity.FindViewById<TextView>(Jetstream.Resource.Id.text_container);
      this.bodyTextView.Text = Html.FromHtml(this.destination.Overview).ToString();

      this.destinationImageView = this.activity.FindViewById<ImageView>(Jetstream.Resource.Id.image);
      this.headerBar = this.activity.FindViewById<View>(Jetstream.Resource.Id.header_bar);
      this.headerBackground = this.activity.FindViewById<View>(Jetstream.Resource.Id.header_background);

      this.scrollView = this.activity.FindViewById<ObservableScrollView>(Jetstream.Resource.Id.scroll);
      this.scrollView.SetScrollViewCallbacks(this);

      this.interceptionFrameLayout = this.activity.FindViewById<TouchInterceptionFrameLayout>(Jetstream.Resource.Id.scroll_wrapper);
      this.interceptionFrameLayout.SetScrollInterceptionListener(this);

      ViewHelper.SetTranslationY(this.toolbar, (this.headerBarHeight - this.actionBarSize) / 2);
      this.mInitialTranslationY = this.GetImageTransition();

      ScrollUtils.AddOnGlobalLayoutListener(this.interceptionFrameLayout, new Runnable(delegate
        {
          isReady = true;
          UpdateViews(mInitialTranslationY, false);
        }));
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
      return this.flexibleSpaceImageHeight;
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
        animator.Update += (sender, args) =>
        {
          var height = (float)args.P0.AnimatedValue;
          this.ChangeHeaderBackgroundHeight(height, to, heightOnGapHidden);
        };

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

      var a = this.activity.ObtainStyledAttributes(typedValue.Data, textSizeAttr);
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

    private float GetMinInterceptionLayoutY()
    {
      //      return this.actionBarSize - this.intersectionHeight;
      // If you want to move header bar to the top, return 0 instead.
      return 0;
    }

    public bool ShouldInterceptTouchEvent(MotionEvent ev, bool moving, float diffX, float diffY)
    {
      if (ev.Action == MotionEventActions.Down)
      {
        this.isFromToolbarTouch = !IsPointInsideView(ev.RawX, ev.RawY, this.scrollView);
      }

      if (this.isFromToolbarTouch)
      {
        return false;
      }

      if (System.Math.Abs(diffY) < 0.1)
      {
        return false;
      }

      var layoutY = this.GetMinInterceptionLayoutY() < (int)ViewHelper.GetY(this.interceptionFrameLayout);
      var movOrScroll = (moving && this.scrollView.CurrentScrollY - diffY < 0);

      return layoutY || movOrScroll;
    }

    public bool IsPointInsideView(float x, float y, View view)
    {
      var location = new int[2];

      view.GetLocationOnScreen(location);

      int viewX = location[0];
      int viewY = location[1];

      //point is inside view bounds
      if(( x > viewX && x < (viewX + view.Width)) && ( y > viewY && y < (viewY + view.Height))){
        return true;
      } else {
        return false;
      }
    }

    public void OnDownMotionEvent(MotionEvent ev)
    {
      if (this.isFromToolbarTouch)
      {
        return;
      }

      this.scrollYOnDownMotion = this.scrollView.CurrentScrollY;
    }

    public void OnMoveMotionEvent(MotionEvent ev, float diffX, float diffY)
    {
      if (this.isFromToolbarTouch)
      {
        return;
      }

      if (System.Math.Abs(diffY - 0) < 0.1 || System.Math.Abs(diffY) > MaxDiffYAcceptable) 
      {
        return;
      }

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

    #endregion Views operations internals
  }

  class DestinationImageTarget : Java.Lang.Object, ITarget
  {
    private readonly ImageView image;
    private readonly Palette.IPaletteAsyncListener paletteAsyncListener;

    public DestinationImageTarget(ImageView image, Palette.IPaletteAsyncListener paletteAsyncListener)
    {
      this.image = image;
      this.paletteAsyncListener = paletteAsyncListener;
    }

    public void OnBitmapFailed(Drawable p0)
    {
    }

    public void OnBitmapLoaded(Bitmap bitmap, Picasso.LoadedFrom from)
    {
      this.image.SetImageBitmap(bitmap);
      Palette.From(bitmap).MaximumColorCount(16).Generate(this.paletteAsyncListener);
    }

    public void OnPrepareLoad(Drawable p0)
    {
    }
  }

  class AnimatorImpl : Java.Lang.Object, ValueAnimator.IAnimatorUpdateListener
  {
    private readonly Action<ValueAnimator> action;

    public AnimatorImpl(Action<ValueAnimator> action)
    {
      this.action = action;
    }

    public void OnAnimationUpdate(ValueAnimator animation)
    {
      this.action.Invoke(animation);
    }
  }
}