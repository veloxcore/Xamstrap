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
using Xamarin.Forms;
using Xamstrap;
using Xamarin.Forms.Platform.Android;
using Xamstrap.AttachedProperties;
using Android.Content.Res;

[assembly: ExportRenderer(typeof(Div), typeof(Xamstrap.Droid.DivRenderer))]
namespace Xamstrap.Droid
{
    public class DivRenderer : ViewRenderer<Div, Android.Views.View>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Div> e)
        {
            base.OnElementChanged(e);
            List<string> classes = Element?.GetValue(ResponsiveProperty.ClassProperty)?.ToString().Split(" ".ToCharArray()).ToList();

            if (classes != null)
            {
                ProcessBackgroundColorElement(classes);

                ProcessVisibility(classes);
            }
        }

        protected override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            List<string> classes = Element?.GetValue(ResponsiveProperty.ClassProperty)?.ToString().Split(" ".ToCharArray()).ToList();

            if (classes != null)
            {
                ProcessVisibility(classes);
            }
        }
        private void ProcessVisibility(List<string> classes)
        {
            if (classes.Any(o => o.StartsWith("hidden-")))
            {
                double width = (Resources.DisplayMetrics.WidthPixels) / (Resources.DisplayMetrics.Density);
                Enums.DeviceSize deviceSize = Common.GetDeviceSize(width);

                Element.IsVisible = true;
                if (classes.Any(o => o.Equals($"hidden-{deviceSize.Tag()}")))
                {
                    Element.IsVisible = false;
                }
            }
        }

        private void ProcessBackgroundColorElement(List<string> classes)
        {
            object backgroundColor = null;
            if (classes.Any(o => o.Equals(Constant.BGPrimary)))
            {
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BGPrimary, out backgroundColor);
                if (backgroundColor == null)
                    backgroundColor = "#337ab7";
            }
            else if (classes.Any(o => o.Equals(Constant.BGSuccess)))
            {
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BGSuccess, out backgroundColor);
                if (backgroundColor == null)
                    backgroundColor = "#dff0d8";
            }
            else if (classes.Any(o => o.Equals(Constant.BGInfo)))
            {
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BGInfo, out backgroundColor);
                if (backgroundColor == null)
                    backgroundColor = "#d9edf7";
            }
            else if (classes.Any(o => o.Equals(Constant.BGWarning)))
            {
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BGWarning, out backgroundColor);
                if (backgroundColor == null)
                    backgroundColor = "#fcf8e3";
            }
            else if (classes.Any(o => o.Equals(Constant.BGDanger)))
            {
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BGDanger, out backgroundColor);
                if (backgroundColor == null)
                    backgroundColor = "#f2dede";
            }

            if (backgroundColor != null)
                Element.BackgroundColor = Color.FromHex(backgroundColor.ToString());
        }
    }
}