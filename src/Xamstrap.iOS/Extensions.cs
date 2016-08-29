using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Xamstrap.iOS
{
    public static class Extensions
    {
        private delegate IVisualElementRenderer GetRendererDelegate(VisualElement bindable);

        private static GetRendererDelegate _getRendererDelegate;

        public static IVisualElementRenderer GetRenderer(this VisualElement bindable)
        {
            if (bindable == null)
            {
                return null;
            }

            if (_getRendererDelegate == null)
            {
                var assembly = typeof(Xamarin.Forms.Platform.iOS.EntryRenderer).Assembly;
                var platformType = assembly.GetType("Xamarin.Forms.Platform.iOS.Platform");
                var method = platformType.GetMethod("GetRenderer");
                _getRendererDelegate = (GetRendererDelegate)method.CreateDelegate(typeof(GetRendererDelegate));
            }

            var value = _getRendererDelegate(bindable);

            return value;
        }
    }
}
