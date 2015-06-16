namespace Jetstream.UI.Anim
{
  using System;
  using Android.Animation;
  using Android.Views;

  public static class DestinationsCardsAnimHelper
  {
    private const long AnimationDuration = 500;

    public static void AnimateAppearance(View layout)
    {
      PrepareAppearanceAnimation(layout).Start();
    }

    public static void AnimateDisappearance(View layout)
    {
      PrepareDisappearanceAnimation(layout).Start();
    }

    private static AnimatorSet PrepareDisappearanceAnimation(View layout)
    {
      ViewGroup parent = (ViewGroup)layout.Parent;
      int distance = parent.Height - layout.Top;

      var set = new AnimatorSet();
      set.PlayTogether(
        ObjectAnimator.OfFloat(layout, "alpha", 1, 0),
        ObjectAnimator.OfFloat(layout, "translationY", 0, distance));

      set.SetDuration(AnimationDuration);
      set.SetTarget(layout);

      var listener = new AnimatorListenerImpl
        {
          AnimationEnd = animator => layout.Visibility = ViewStates.Gone
        };

      set.AddListener(listener);
      return set;
    }

    private static AnimatorSet PrepareAppearanceAnimation(View layout)
    {
      ViewGroup parent = (ViewGroup)layout.Parent;
      int distance = parent.MeasuredHeight - layout.Top;

      var set = new AnimatorSet();
      set.PlayTogether(
        ObjectAnimator.OfFloat(layout, "alpha", 0, 1),
        ObjectAnimator.OfFloat(layout, "translationY", distance, 0));

      set.SetDuration(AnimationDuration);
      set.SetTarget(layout);

      var listener = new AnimatorListenerImpl
        {
          AnimationStart = animator => layout.Visibility = ViewStates.Visible
        };

      set.AddListener(listener);
      return set;
    }

    private class AnimatorListenerImpl : AnimatorListenerAdapter
    {
      public Action<Animator> AnimationStart { get; set; }

      public Action<Animator> AnimationEnd { get; set; }

      public override void OnAnimationStart(Animator animation)
      {
        if(this.AnimationStart != null)
        {
          this.AnimationStart(animation);
        }
      }

      public override void OnAnimationEnd(Animator animation)
      {
        if(this.AnimationEnd != null)
        {
          this.AnimationEnd(animation);
        }
      }
    }
  }
}

