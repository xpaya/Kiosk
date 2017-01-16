using System;
using Android.Content;
using Android.Util;

namespace Kiosk.Common
{
    public static class ScreenUtilities
    {
        public static int CalculateNumberOfAppsForRows(Context context, float heigthAsigned, float withAsigned, float elementHeigth, float elementWidth)
        {
            DisplayMetrics displayMetrics = context.Resources.DisplayMetrics;
            float dpHeight = (displayMetrics.HeightPixels / displayMetrics.Density) - (heigthAsigned / displayMetrics.Density);
            float dpWidth = (displayMetrics.WidthPixels / displayMetrics.Density) - (withAsigned / displayMetrics.Density);

            var rows = (int)(dpHeight / (elementHeigth / displayMetrics.Density));
            rows = rows > 0 ? rows : 1;
            var col = (int)(dpWidth / (elementWidth / displayMetrics.Density));
            col = col > 0 ? col : 1;
            var result = rows * col;
            return result > 0 ? result : 1;
        }

        public static int CalculateNumberOfRowsOnScreen(Context context, float elementWidth, int elements)
        {
            DisplayMetrics displayMetrics = context.Resources.DisplayMetrics;
            float dpWidth = (displayMetrics.WidthPixels / displayMetrics.Density);

            var cols = (dpWidth / (elementWidth / displayMetrics.Density));
            cols = cols > 0 ? cols : 1;
            return (int)Math.Ceiling(elements / cols);
        }
    }
}