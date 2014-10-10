namespace JetstreamAndroid.Tabs
{
  using System;
  using Android.Content;
  using Android.Util;
  using Android.Views;
  using Android.Widget;
  using JetstreamAndroid.Utils;

  public class TabView : LinearLayout
  {
    private TabPageIndicator mParent;
    private int mIndex;

    private TextView priceTextView;
    private TextView dateTextView;

    private DateTime tabDate;
    private decimal? price;

    private bool isPriceUpdated;
    private bool toShowPrice = true;

    public TabView(Context context, IAttributeSet attrs)
      : base(context, attrs)
    {
    }

    public void Init(TabPageIndicator parent, DateTime date, int index)
    {
      this.mParent = parent;
      this.mIndex = index;
      this.tabDate = date;

      this.priceTextView = this.FindViewById<TextView>(Resource.Id.textview_day_price);
      this.dateTextView = this.FindViewById<TextView>(Resource.Id.textview_day_date);

      this.dateTextView.Text = date.ToShortDateString();
      this.UpdatePrice();
    }

    protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
    {
      base.OnMeasure(widthMeasureSpec, heightMeasureSpec);

      // Re-measure if we went beyond our maximum size.
      if(this.mParent.MaxTabWidth > 0 && this.MeasuredWidth > this.mParent.MaxTabWidth)
      {
        base.OnMeasure(MeasureSpec.MakeMeasureSpec(this.mParent.MaxTabWidth, MeasureSpecMode.Exactly), heightMeasureSpec);
      }

    }

    public int GetIndex()
    {
      return this.mIndex;
    }

    private void HidePrice()
    {
      this.priceTextView.Visibility = ViewStates.Gone;
      this.dateTextView.Gravity = GravityFlags.Center;
    }

    private void ShowPrice()
    {
      this.priceTextView.Visibility = ViewStates.Visible;
      this.priceTextView.Text = CurrencyUtil.ConvertPriceToLocalString(this.price);
    }

    private async void UpdatePrice()
    {
      if(price == null && this.isPriceUpdated == false)
      {
        try
        {
          var loader = JetstreamApp.From(this.mParent.Context).FlightSearchLoaderForDate(this.tabDate);

          var summary = await loader.GetCurrentDayAsync();
          if(summary != null)
          {
            this.isPriceUpdated = true;
            this.price = summary.LowestPrice;
          }
        }
        catch (System.Exception exception)
        {
          LogUtils.Info(typeof(TabView), "Updateting lowests price for date\n" + exception);
        }
      }
      this.ShowOrHidePrice(this.toShowPrice);
    }

    public void ShowOrHidePrice(bool toShow)
    {
      this.toShowPrice = toShow;
      if(this.toShowPrice)
      {
        this.ShowPrice();
      }
      else
      {
        this.HidePrice();
      }
    }
  }
}
