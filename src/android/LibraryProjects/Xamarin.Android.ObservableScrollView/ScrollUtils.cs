using Color = Android.Graphics.Color;
using View = Android.Views.View;

namespace Xamarin.Android.ObservableScrollView
{
  using Java.Lang;

  /**
	 * Utilities for creating scrolling effects.
	 */
	public class ScrollUtils
	{
		/**
	     * Return a float value within the range.
	     * This is just a wrapper for Math.min() and Math.max().
	     * This may be useful if you feel it confusing ("Which is min and which is max?").
	     *
	     */
		public static float GetFloat (float value, float minValue, float maxValue)
		{
			return Math.Min (maxValue, Math.Max (minValue, value));
		}

		/**
	     * Create a color integer value with specified alpha.
	     * This may be useful to change alpha value of background color.
	     *
	     * @param alpha     alpha value from 0.0f to 1.0f.
	     * @param baseColor base color. alpha value will be ignored.
	     * @return a color with alpha made from base color
	     */
		public static Color GetColorWithAlpha (float alpha, int baseColor)
		{
			int a = System.Math.Min (255, System.Math.Max (0, (int)(alpha * 255))) << 24;
			int rgb = 0x00ffffff & baseColor;
      return new Color(a + rgb);
		}

		/**
     * Add an OnGlobalLayoutListener for the view.
     * This is just a convenience method for using {@code ViewTreeObserver.OnGlobalLayoutListener()}.
     * This also handles removing listener when onGlobalLayout is called.
     *
     * @param view     the target view to add global layout listener
     * @param runnable runnable to be executed after the view is laid out
     */
		public static void AddOnGlobalLayoutListener (View view, Runnable runnable)
		{					
			view.ViewTreeObserver.AddOnGlobalLayoutListener (new GlobalLayoutListener (runnable, view));
		}

		/**
     * Mix two colors.
     * {@code toColor} will be {@code toAlpha/1} percent,
     * and {@code fromColor} will be {@code (1-toAlpha)/1} percent.
     *
     * @param fromColor first color to be mixed
     * @param toColor   second color to be mixed
     * @param toAlpha   alpha value of toColor, 0.0f to 1.0f.
     * @return mixed color value in ARGB. Alpha is fixed value (255).
     */
		public static int MixColors (int fromColor, int toColor, float toAlpha)
		{
			float[] fromCmyk = CmykFromRgb (fromColor);
			float[] toCmyk = CmykFromRgb (toColor);
			float[] result = new float[4];
			for (int i = 0; i < 4; i++) {
				result [i] = System.Math.Min (1, fromCmyk [i] * (1 - toAlpha) + toCmyk [i] * toAlpha);
			}
			int tmp = 0x00ffffff & RgbFromCmyk (result);
			return (int)(0xff000000 + tmp);
		}

		/**
     * Convert RGB color to CMYK color.
     *
     * @param rgbColor target color
     * @return CMYK array
     */
		public static float[] CmykFromRgb (int rgbColor)
		{
			int red = (0xff0000 & rgbColor) >> 16;
			int green = (0xff00 & rgbColor) >> 8;
			int blue = (0xff & rgbColor);
			float black = System.Math.Min (1.0f - red / 255.0f, System.Math.Min (1.0f - green / 255.0f, 1.0f - blue / 255.0f));
			float cyan = 1.0f;
			float magenta = 1.0f;
			float yellow = 1.0f;
			if (black != 1.0f) {
				// black 1.0 causes zero divide
				cyan = (1.0f - (red / 255.0f) - black) / (1.0f - black);
				magenta = (1.0f - (green / 255.0f) - black) / (1.0f - black);
				yellow = (1.0f - (blue / 255.0f) - black) / (1.0f - black);
			}
			return new float[]{ cyan, magenta, yellow, black };
		}

		/**
     * Convert CYMK color to RGB color.
     * This method doesn't check f cmyk is not null or have 4 elements in array.
     *
     * @param cmyk target CYMK color. Each value should be between 0.0f to 1.0f,
     *             and should be set in this order: cyan, magenta, yellow, black.
     * @return ARGB color. Alpha is fixed value (255).
     */
		public static int RgbFromCmyk (float[] cmyk)
		{
			float cyan = cmyk [0];
			float magenta = cmyk [1];
			float yellow = cmyk [2];
			float black = cmyk [3];
			int red = (int)((1.0f - System.Math.Min (1.0f, cyan * (1.0f - black) + black)) * 255);
			int green = (int)((1.0f - System.Math.Min (1.0f, magenta * (1.0f - black) + black)) * 255);
			int blue = (int)((1.0f - System.Math.Min (1.0f, yellow * (1.0f - black) + black)) * 255);
			return ((0xff & red) << 16) + ((0xff & green) << 8) + (0xff & blue);
		}
	}
}

