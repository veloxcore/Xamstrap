using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using System.ComponentModel;
using Xamstrap;
using Xamstrap.AttachedProperties;

[assembly: ExportRenderer(typeof(Xamarin.Forms.ScrollView), typeof(Xamstrap.Droid.ScrollViewEXRenderer))]
namespace Xamstrap.Droid
{
    public class ScrollViewEXRenderer : ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            var result = this.Element.GetValue(ScrollViewPoperty.HorizontalScrollBarVisibleProperty);
            bool IsHorizontalScrollBarVisible = Convert.ToBoolean(result);
            if (IsHorizontalScrollBarVisible == false)
            {
                if (e.OldElement != null || this.Element == null)
                {
                    return;
                }

                if (e.OldElement != null)
                {
                    e.OldElement.PropertyChanged -= OnElementPropetyChanged;
                }

                e.NewElement.PropertyChanged += OnElementPropetyChanged;

            }
        }

        private void OnElementPropetyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (ChildCount > 0)
            {
                GetChildAt(0).HorizontalScrollBarEnabled = false;
            }
        }
    }
}