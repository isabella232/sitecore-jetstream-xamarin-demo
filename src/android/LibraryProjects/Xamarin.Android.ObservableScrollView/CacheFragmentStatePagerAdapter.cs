using Bundle = Android.OS.Bundle;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;
using FragmentStatePagerAdapter = Android.Support.V4.App.FragmentStatePagerAdapter;
using IParcelable = Android.OS.IParcelable;
using ViewGroup = Android.Views.ViewGroup;
using Android.Util;

namespace Xamarin.Android.ObservableScrollView
{
  public abstract class CacheFragmentStatePagerAdapter
    : FragmentStatePagerAdapter
  {
    const string StateSuperState = "superState";
    const string StatePages = "pages";
    const string StatePageKeyPrefix = "page:";

    private readonly FragmentManager mFm;
    private readonly SparseArray<Fragment> mPages;

    public CacheFragmentStatePagerAdapter(FragmentManager fm)
      : base(fm)
    {
      this.mPages = new SparseArray<Fragment>();
      this.mFm = fm;
    }

    public override IParcelable SaveState()
    {
      var p = base.SaveState();
      Bundle bundle = new Bundle();
      bundle.PutParcelable(StateSuperState, p);

      bundle.PutInt(StatePages, this.mPages.Size());
      if (0 < this.mPages.Size())
      {
        for (int i = 0; i < this.mPages.Size(); i++)
        {
          Fragment f = this.mPages.Get(i);
          this.mFm.PutFragment(bundle, this.CreateCacheKey(i), f);
        }
      }
      return bundle;
    }

    public override void RestoreState(IParcelable state, Java.Lang.ClassLoader loader)
    {
      Bundle bundle = (Bundle)state;
      int pages = bundle.GetInt(StatePages);
      if (0 < pages)
      {
        for (int i = 0; i < pages; i++)
        {
          Fragment f = this.mFm.GetFragment(bundle, this.CreateCacheKey(i));
          this.mPages.Put(i, f);
        }
      }

      var p = bundle.GetParcelable(StateSuperState) as IParcelable;
      base.RestoreState(p, loader);
    }


    /**
       * Get a new Fragment instance.
       * Each fragments are automatically cached in this method,
       * so you don't have to do it by yourself.
       * If you want to implement instantiation of Fragments,
       * you should override {@link #createItem(int)} instead.
       */
    public override Fragment GetItem(int position)
    {
      var f = this.CreateItem(position);
      // We should cache fragments manually to access to them later
      this.mPages.Put(position, f);
      return f;
    }

    public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object @object)
    {
      if (0 <= this.mPages.IndexOfKey(position))
      {
        this.mPages.Remove(position);
      }
      base.DestroyItem(container, position, @object);
    }


    /**
       * Get the item at the specified position in the adapter.
       *
       * @param position position of the item in the adapter
       * @return fragment instance
       */
    public Fragment GetItemAt(int position)
    {
      return this.mPages.Get(position);
    }

    /**
       * Create a new Fragment instance.
       * This is called inside {@link #getItem(int)}.
       *
       * @param position position of the item in the adapter
       * @return fragment instance
      */
    protected abstract Fragment CreateItem(int position);

    /**
       * Create a key string for caching Fragment pages.
       *
       * @param position position of the item in the adapter
       * @return key string for caching Fragment pages
       */
    protected string CreateCacheKey(int position)
    {
      return StatePageKeyPrefix + position;
    }
  }
}

