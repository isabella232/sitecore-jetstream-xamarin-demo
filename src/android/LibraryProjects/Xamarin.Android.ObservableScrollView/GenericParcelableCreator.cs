
using IParcelableCreator = Android.OS.IParcelableCreator;
using Parcel = Android.OS.Parcel;

namespace Xamarin.Android.ObservableScrollView
{
  public sealed class GenericParcelableCreator<T> : Java.Lang.Object, IParcelableCreator
		where T : Java.Lang.Object
	{
		private readonly System.Func<Parcel, T> createFunc;

		/// <summary>
		/// Initializes a new instance of the <see cref="ParcelableDemo.GenericParcelableCreator`1"/> class.
		/// </summary>
		/// <param name='createFromParcelFunc'>
		/// Func that creates an instance of T, populated with the values from the parcel parameter
		/// </param>
		public GenericParcelableCreator(System.Func<Parcel, T> createFromParcelFunc)
		{
			this.createFunc = createFromParcelFunc;
		}

		#region IParcelableCreator Implementation

		public Java.Lang.Object CreateFromParcel(Parcel source)
		{
			return this.createFunc(source);
		}

		public Java.Lang.Object[] NewArray(int size)
		{
			return new T[size];
		}

		#endregion
	}
}

