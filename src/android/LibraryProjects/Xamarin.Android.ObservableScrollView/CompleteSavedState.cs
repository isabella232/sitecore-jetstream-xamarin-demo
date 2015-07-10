using IParcelable = Android.OS.IParcelable;
using Parcel = Android.OS.Parcel;
using ParcelableWriteFlags = Android.OS.ParcelableWriteFlags;
using SparseIntArray = Android.Util.SparseIntArray;
using View = Android.Views.View;

namespace Xamarin.Android.ObservableScrollView
{
  public class CompleteSavedState: View.BaseSavedState
	{

		internal int PrevFirstVisiblePosition;
		internal int PrevFirstVisibleChildHeight = -1;
		internal int PrevScrolledChildrenHeight;
		internal int PrevScrollY;
		internal int ScrollY;
		internal SparseIntArray ChildrenHeights;

		// The creator creates an instance of the specified object
		private static readonly GenericParcelableCreator<CompleteSavedState> _creator
		= new GenericParcelableCreator<CompleteSavedState>((parcel) => new CompleteSavedState(parcel));

		[Java.Interop.ExportField("CREATOR")]
		public static GenericParcelableCreator<CompleteSavedState> GetCreator()
		{
			return _creator;
		}

		/**
         * Called by onSaveInstanceState.
         */
		internal CompleteSavedState (IParcelable superState) : base (superState)
		{
		}

		/**
         * Called by CREATOR.
         */
		internal CompleteSavedState(Parcel @in) :base(@in)
		{
			this.PrevFirstVisiblePosition = @in.ReadInt();
			this.PrevFirstVisibleChildHeight = @in.ReadInt();
			this.PrevScrollY = @in.ReadInt();
			this.ScrollY = @in.ReadInt();

			this.ChildrenHeights = new SparseIntArray();

			int numOfChildren = @in.ReadInt();
			if (0 < numOfChildren) {
				for (int i = 0; i < numOfChildren; i++) {
					int key = @in.ReadInt();
					int value = @in.ReadInt();
					this.ChildrenHeights.Put(key, value);
				}
			}
		}

		public override void WriteToParcel (Parcel dest, ParcelableWriteFlags flags)
		{
			base.WriteToParcel(dest, flags);
			dest.WriteInt(this.PrevFirstVisiblePosition);
			dest.WriteInt(this.PrevFirstVisibleChildHeight);
			dest.WriteInt(this.PrevScrolledChildrenHeight);
			dest.WriteInt(this.PrevScrollY);
			dest.WriteInt(this.ScrollY);

			int numOfChildren = this.ChildrenHeights == null ? 0 : this.ChildrenHeights.Size();
			dest.WriteInt(numOfChildren);
			if (0 < numOfChildren) {
				for (int i = 0; i < numOfChildren; i++) {
					dest.WriteInt(this.ChildrenHeights.KeyAt(i));
					dest.WriteInt(this.ChildrenHeights.ValueAt(i));
				}
			}
		}


	}
}

