using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ActionSheetDatePicker {
	public class ActionSheetDatePickerView {

		#region -= declarations =-
    UIView backgroundView;
    UIView dataView;
    UIDatePicker datePicker; 
		UIButton doneButton = UIButton.FromType (UIButtonType.RoundedRect);
		UIView owner;
		UILabel titleLabel = new UILabel ();
    float titleBarHeight = 40;
		#endregion
		
		#region -= properties =-
		public UIDatePicker DatePicker
		{
      get 
      { 
        return this.datePicker; 
      }
			set { datePicker = value; }
		}

		public string Title
		{
			get { return titleLabel.Text; }
			set { titleLabel.Text = value; }
		}
		
    public string DoneButtonTitle
    {
      get { return doneButton.TitleLabel.Text; }
      set { doneButton.SetTitle (value, UIControlState.Normal); }
    }
		#endregion
		
		#region -= constructor =-
    public ActionSheetDatePickerView (UIView owner)
		{
			this.owner = owner;
	
			titleLabel.BackgroundColor = UIColor.Clear;
      titleLabel.TextColor = UIColor.Black;
			titleLabel.Font = UIFont.BoldSystemFontOfSize(18);
			
			doneButton.SetTitle ("Done", UIControlState.Normal);
      doneButton.TouchUpInside += (s, e) => { backgroundView.RemoveFromSuperview(); };
			
      this.backgroundView = new UIView(owner.Bounds);
      this.backgroundView.BackgroundColor = new UIColor (0.3f, 0.3f, 0.3f, 0.6f);

      this.dataView = new UIView(owner.Bounds);
      this.dataView.BackgroundColor = UIColor.White;

      this.datePicker = new UIDatePicker(RectangleF.Empty);

      float tabBarOffset = 50.0f;
      RectangleF dataViewFrame = new RectangleF (
        0,
        this.owner.Bounds.Height - this.datePicker.Frame.Height - this.titleBarHeight - tabBarOffset,
        this.owner.Bounds.Width,
        this.datePicker.Frame.Height + this.titleBarHeight + tabBarOffset
      );
      this.dataView.Frame = dataViewFrame;

      RectangleF datePickerFrame = new RectangleF (
        0,
        this.titleBarHeight,
        this.owner.Bounds.Width,
        this.datePicker.Frame.Height
      );
      this.datePicker.Frame = datePickerFrame;

      this.titleLabel.Frame = new RectangleF (10, 10, owner.Frame.Width - 100, 35);
      SizeF doneButtonSize = new SizeF (71, 30);
      this.doneButton.Frame = new RectangleF (datePickerFrame.Width - doneButtonSize.Width - 10, 10, doneButtonSize.Width, doneButtonSize.Height);


			// add our controls to the action sheet
      this.backgroundView.AddSubview (this.dataView);
      this.dataView.AddSubview (this.DatePicker);
      this.dataView.AddSubview (this.titleLabel);
      this.dataView.AddSubview (this.doneButton);
		}
		#endregion
		
		#region -= public methods =-
		public void Show ()
		{
      this.owner.AddSubview(this.backgroundView);
      RectangleF realFrame = this.dataView.Frame;
      RectangleF fakeFrame = realFrame;
      fakeFrame.Y += fakeFrame.Height;
      this.dataView.Frame = fakeFrame;

      UIView.Animate (
        duration: 0.3f, 
        delay: 0, 
        options: UIViewAnimationOptions.CurveEaseInOut,
        animation: () => {
        this.dataView.Frame = realFrame;
      },
        completion: () => {}
      );
		}

    public void ShowWithDate(DateTime date)
    {
      this.datePicker.SetDate(date, false);
      this.Show();
    }

		public void Hide (bool animated)
		{
      this.backgroundView.RemoveFromSuperview();
		}
		#endregion		
	}
}

