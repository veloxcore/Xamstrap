using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamstrap;
using Xamstrap.AttachedProperties;

[assembly: ExportRenderer(typeof(Xamarin.Forms.ScrollView), typeof(Xamstrap.iOS.ScrollViewEXRenderer))]
namespace Xamstrap.iOS
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
                    e.OldElement.PropertyChanged -= OnElementPropertyChanged;
                }

                e.NewElement.PropertyChanged += OnElementPropertyChanged;
            }
        }
        private void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            this.ShowsHorizontalScrollIndicator = false;
        }
    }
}