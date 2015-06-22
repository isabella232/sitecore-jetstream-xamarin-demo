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

    private static AnimatorSet PrepareAppearanceAnimation(View layout)
    {
      ViewGroup parent = (ViewGroup)layout.Parent;
      int distance = parent.MeasuredHeight - layout.Top;

      var set = new AnimatorSet();
      set.Play(ObjectAnimator.OfFloat(layout, "translationY", distance, 0));

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

