using UnityEngine.UI;

namespace Chocolate4.Helpers
{
    public static class ImageExtensions
    {
        public static void FadeIn(this Image img, float seconds)
        {
            LeanTween.alpha(img.rectTransform, 1f, seconds);
        }
        public static void FadeOut(this Image img, float seconds)
        {
            LeanTween.alpha(img.rectTransform, 0f, seconds);
        }
    }
}