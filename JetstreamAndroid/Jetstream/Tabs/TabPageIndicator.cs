namespace JetstreamAndroid.Tabs
{
  using System;
  using Android.Content;
  using Android.Support.V4.View;
  using Android.Util;
  using Android.Views;
  using Android.Widget;
  using Java.Lang;
  using JetStreamCommons.FlightSearch;

  public class TabPageIndicator : HorizontalScrollView, PageIndicator
  {
    private readonly LinearLayout mTabLayout;
    private ViewPager mViewPager;
    private ViewPager.IOnPageChangeListener mListener;
    private readonly LayoutInflater mInflater;
    int mMaxTabWidth;
    private int mSelectedTabIndex;

    public TabPageIndicator(Context context)
      : base(context, null)
    {
    }

    public TabPageIndicator(Context context, IAttributeSet attrs)
      : base(context, attrs)
    {
      this.HorizontalScrollBarEnabled = false;

      this.mInflater = LayoutInflater.From(context);

      this.mTabLayout = new LinearLayout(this.Context);
      this.AddView(this.mTabLayout, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.MatchParent));
    }

    protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
    {
      var widthMode = MeasureSpec.GetMode(widthMeasureSpec);
      var lockedExpanded = widthMode == MeasureSpecMode.Exactly;
      this.FillViewport = lockedExpanded;

      int childCount = this.mTabLayout.ChildCount;
      if (childCount > 1 && (widthMode == MeasureSpecMode.Exactly || widthMode == MeasureSpecMode.AtMost))
      {
        if (childCount > 2)
        {
          this.mMaxTabWidth = (int)(MeasureSpec.GetSize(widthMeasureSpec) * 0.4f);
        }
        else
        {
          this.mMaxTabWidth = MeasureSpec.GetSize(widthMeasureSpec) / 2;
        }
      }
      else
      {
        this.mMaxTabWidth = -1;
      }

      int oldWidth = this.MeasuredWidth;
      base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
      int newWidth = this.MeasuredWidth;

      if (lockedExpanded && oldWidth != newWidth)
      {
        this.SetCurrentItem(this.mSelectedTabIndex);
      }
    }

    private void AnimateToTab(int position)
    {
      var tabView = this.mTabLayout.GetChildAt(position);

      this.Post(() =>
      {
        var scrollPos = tabView.Left - (this.Width - tabView.Width) / 2;
        this.SmoothScrollTo(scrollPos, 0);
      });
    }

    public void SetCurrentItem(int item)
    {
      if (this.mViewPager == null)
      {
        throw new IllegalStateException("ViewPager has not been bound.");
      }
      this.mSelectedTabIndex = item;
      var tabCount = this.mTabLayout.ChildCount;
      for (int i = 0; i < tabCount; i++)
      {
        var child = this.mTabLayout.GetChildAt(i);
        var isSelected = (i == item);
        child.Selected = isSelected;
        if (isSelected)
        {
          this.AnimateToTab(item);
        }
      }
    }

    public void AddTab(string price, string date, int index)
    {
      var tabView = (TabView)this.mInflater.Inflate(Resource.Layout.tab_item, null);

      tabView.Init(this, price, date, index);

      tabView.Focusable = true;
      tabView.Click += delegate(object sender, EventArgs e)
      {
        var tView = (TabView)sender;

        this.mViewPager.CurrentItem = tView.GetIndex();
      };

      this.mTabLayout.AddView(tabView, new LinearLayout.LayoutParams(0, height: ViewGroup.LayoutParams.MatchParent, weight: 1));
    }

    public void OnPageScrollStateChanged(int p0)
    {
      if (this.mListener != null)
      {
        this.mListener.OnPageScrollStateChanged(p0);
      }
    }

    public void OnPageScrolled(int p0, float p1, int p2)
    {
      if (this.mListener != null)
      {
        this.mListener.OnPageScrolled(p0, p1, p2);
      }
    }

    public void OnPageSelected(int position)
    {
      this.SetCurrentItem(position);
      if (this.mListener != null)
      {
        this.mListener.OnPageSelected(position);
      }
    }

    public void SetViewPager(ViewPager view)
    {
      var adapter = view.Adapter;
      if (adapter == null)
      {
        throw new IllegalStateException("ViewPager does not have adapter instance.");
      }
      this.mViewPager = view;
      view.SetOnPageChangeListener(this);
      this.NotifyDataSetChanged();
    }

    public void NotifyDataSetChanged()
    {
      //TODO : change if requested
      //      this.mTabLayout.RemoveAllViews();
      //
      //      int count = this.mViewPager.Adapter.Count;
      //      for (int i = 0; i < count; i++)
      //      {
      //        AddTab((i + 1).ToString(), i);
      //      }
      //      if (mSelectedTabIndex > count)
      //      {
      //        mSelectedTabIndex = count - 1;
      //      }
      //      SetCurrentItem(mSelectedTabIndex);
      //      this.RequestLayout();
    }

    public void SetViewPager(ViewPager view, int initialPosition)
    {
      this.SetViewPager(view);
      this.SetCurrentItem(initialPosition); 
    }

    public void SetOnPageChangeListener(ViewPager.IOnPageChangeListener listener)
    {
      this.mListener = listener;
    }

    public void AddYesterdayTab(DaySummary yesterdaySummary)
    {
      this.AddTab(yesterdaySummary.LowestPrice.ToString(), yesterdaySummary.DepartureDate.ToShortDateString(), 0);
    }

    public void AddTodayTab(IFlightSearchUserInput input)
    {
      this.AddTab(null, input.ForwardFlightDepartureDate.ToShortDateString(), 1);
    }

    public void AddTommorowTab(DaySummary tomorrowSummary)
    {
      this.AddTab(tomorrowSummary.LowestPrice.ToString(), tomorrowSummary.DepartureDate.ToShortDateString(), 2);
    }

    public class TabView : LinearLayout
    {
      private TabPageIndicator mParent;
      private int mIndex;

      public TabView(Context context, IAttributeSet attrs)
        : base(context, attrs)
      {
      }

      public void Init(TabPageIndicator parent, string price, string date, int index)
      {
        this.mParent = parent;
        this.mIndex = index;

        var priceText = this.FindViewById<TextView>(Resource.Id.textview_day_price);
        var dateText = this.FindViewById<TextView>(Resource.Id.textview_day_date);
        
        dateText.Text = date;
        if (!string.IsNullOrEmpty(price))
        {
          priceText.Text = "$" + price;
        }
        else
        {
          priceText.Visibility = ViewStates.Gone;
          dateText.Gravity = GravityFlags.Center;
        }
      }

      protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
      {
        base.OnMeasure(widthMeasureSpec, heightMeasureSpec);

        // Re-measure if we went beyond our maximum size.
        if (this.mParent.mMaxTabWidth > 0 && this.MeasuredWidth > this.mParent.mMaxTabWidth)
        {
          base.OnMeasure(MeasureSpec.MakeMeasureSpec(this.mParent.mMaxTabWidth, MeasureSpecMode.Exactly), heightMeasureSpec);
        }

      }

      public int GetIndex()
      {
        return this.mIndex;
      }
    }
  }
}

