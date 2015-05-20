
namespace JetStreamIOS
{
  using System;
  using System.Drawing;

  using MonoTouch.Foundation;
  using MonoTouch.UIKit;

  using JetStreamCommons;
  using JetStreamIOS.Helpers;


  public partial class OrderSummaryViewController : UIViewController
  {
 
    public JetStreamOrder Order;

    public OrderSummaryViewController (IntPtr handle) : base (handle)
    {
    }

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();
        
      IOSOrderSummaryHtmlBuilder htmlBuilder = new IOSOrderSummaryHtmlBuilder();
      string htmlString = htmlBuilder.GetHtmlStringWithOrder(this.Order);
      if (null != htmlString)
      {
        this.SummaryInfoWebView.LoadHtmlString (htmlString, null);
      }
    }

    partial void OnPurchaseButtonTouched (MonoTouch.UIKit.UIButton sender)
    {
      AlertHelper.ShowLocalizedAlertWithOkOption(null, "Tickets purchased");
    }
  }
}

