namespace Jetstream.UI.Anim
{
  using Android.Animation;
  using Android.Views;

  public static class DestinationsCardsAnimHelper
  {
    private const long AnimationDuration = 500;

    public static void AnimateAppearance(View layout)
    {
      PrepareAppearanceAnimation(layout).Start();
    }

    private static AnimatorSet PrepareAppearanceAnimation(View layout)
    {
      ViewGroup parent = (ViewGroup)layout.Parent;
      int distance = parent.MeasuredHeight - layout.Top;

      var set = new AnimatorSet();
      set.Play(ObjectAnimator.OfFloat(layout, "translationY", distance, 0));

      set.SetDuration(AnimationDuration);
      set.SetTarget(layout);
      set.AnimationStart += (sender, args) => layout.Visibility = ViewStates.Visible;

      return set;
    }
  }
}

