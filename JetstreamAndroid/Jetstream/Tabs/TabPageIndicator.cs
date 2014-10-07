namespace JetstreamAndroid.Tabs
{
  using System;
  using Android.Content;
  using Android.Support.V4.View;
  using Android.Util;
  using Android.Views;
  using Android.Widget;
  using Java.Lang;

  public class TabPageIndicator : HorizontalScrollView, PageIndicator
  {
    private readonly LinearLayout mTabLayout;
    private ViewPager mViewPager;
    private ViewPager.IOnPageChangeListener mListener;
    private readonly LayoutInflater mInflater;

    public int MaxTabWidth { get; private set; }

    private int mSelectedTabIndex;

    public int TabsCount
    {
      get
      {
        return mTabLayout.ChildCount;
      }
    }

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
          this.MaxTabWidth = (int)(MeasureSpec.GetSize(widthMeasureSpec) * 0.4f);
        }
        else
        {
          this.MaxTabWidth = MeasureSpec.GetSize(widthMeasureSpec) / 2;
        }
      }
      else
      {
        this.MaxTabWidth = -1;
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
      var tabView = (TabView)this.mTabLayout.GetChildAt(position);

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
      for (var i = 0; i < tabCount; i++)
      {
        var child = (TabView)this.mTabLayout.GetChildAt(i);
        var isSelected = (i == item);
        child.Selected = isSelected;
        if (isSelected)
        {
          child.ShowOrHidePrice(false);
          this.AnimateToTab(item);
        }
        else
        {
          child.ShowOrHidePrice(true);
        }
      }
    }

    public void RefreshTabs()
    {
      var tabCount = this.mTabLayout.ChildCount;
      for (var i = 0; i < tabCount; i++)
      {
        var child = (TabView)this.mTabLayout.GetChildAt(i);
        var isSelected = (i == this.mSelectedTabIndex);
        child.Selected = isSelected;
        child.ShowOrHidePrice(!isSelected);
      }
    }

    public void AddTab(DateTime date, int index, bool isCurrentDay)
    {
      var tabView = (TabView)this.mInflater.Inflate(Resource.Layout.tab_item, null);

      tabView.Init(this, date, index, isCurrentDay);

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

    public void SetViewPager(ViewPager pager)
    {
      var adapter = pager.Adapter;
      if (adapter == null)
      {
        throw new IllegalStateException("ViewPager does not have adapter instance.");
      }
      this.mViewPager = pager;
      pager.SetOnPageChangeListener(this);
    }

    public void SetViewPager(ViewPager pager, int initialPosition)
    {
      this.SetViewPager(pager);
      this.SetCurrentItem(initialPosition);
    }

    public void SetOnPageChangeListener(ViewPager.IOnPageChangeListener listener)
    {
      this.mListener = listener;
    }
  }
}

