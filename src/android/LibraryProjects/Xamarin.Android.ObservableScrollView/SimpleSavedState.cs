using IParcelable = Android.OS.IParcelable;
using Parcel = Android.OS.Parcel;
using ParcelableWriteFlags = Android.OS.ParcelableWriteFlags;
using View = Android.Views.View;

namespace Xamarin.Android.ObservableScrollView
{
  public class SimpleSavedState: View.BaseSavedState
	{
		internal int PrevScrollY;
		internal int ScrollY;

		// The creator creates an instance of the specified object
		private static readonly GenericParcelableCreator<SimpleSavedState> _creator
		= new GenericParcelableCreator<SimpleSavedState>((parcel) => new SimpleSavedState(parcel));

		[Java.Interop.ExportField("CREATOR")]
		public static GenericParcelableCreator<SimpleSavedState> GetCreator()
		{
			return _creator;
		}

		/**
         * Called by onSaveInstanceState.
         */
		internal SimpleSavedState (IParcelable superState) : base (superState)
		{
		}

		/**
         * Called by CREATOR.
         */
		internal SimpleSavedState(Parcel @in) :base(@in)
		{
			this.PrevScrollY = @in.ReadInt();
			this.ScrollY = @in.ReadInt();
		}

		public override void WriteToParcel (Parcel dest, ParcelableWriteFlags flags)
		{
			base.WriteToParcel(dest, flags);
			dest.WriteInt(this.PrevScrollY);
			dest.WriteInt(this.ScrollY);

		}
	}
}

