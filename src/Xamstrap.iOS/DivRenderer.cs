using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamstrap.AttachedProperties;

[assembly: ExportRenderer(typeof(Xamstrap.Div), typeof(Xamstrap.iOS.DivRenderer))]
namespace Xamstrap.iOS
{
    public class DivRenderer : ViewRenderer<Div, UIView>
    {
        public DivRenderer()
        {
            var notificationCenter = NSNotificationCenter.DefaultCenter;
            notificationCenter.AddObserver(UIApplication.DidChangeStatusBarOrientationNotification, DeviceOrientationDidChange);
            UIDevice.CurrentDevice.BeginGeneratingDeviceOrientationNotifications();

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Div> e)
        {
            if(Control == null)
            {
                SetNativeControl(new UIView());
            }

            base.OnElementChanged(e);
            List<string> classes = Element?.GetValue(ResponsiveProperty.ClassProperty)?.ToString().Split(" ".ToCharArray()).ToList();

            if (classes != null)
            {
                ProcessBackgroundColorElement(classes);

                ProcessVisibility(classes);
            }
        }
        public void DeviceOrientationDidChange(NSNotification notification)
        {
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
                double width = UIScreen.MainScreen.Bounds.Width;
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
                Application.Current.Resources?.TryGetValue(Constant.BGPrimary, out backgroundColor);
                if (backgroundColor == null)
                    backgroundColor = "#337ab7";
            }
            else if (classes.Any(o => o.Equals(Constant.BGSuccess)))
            {
                Application.Current.Resources?.TryGetValue(Constant.BGSuccess, out backgroundColor);
                if (backgroundColor == null)
                    backgroundColor = "#dff0d8";
            }
            else if (classes.Any(o => o.Equals(Constant.BGInfo)))
            {
                Application.Current.Resources?.TryGetValue(Constant.BGInfo, out backgroundColor);
                if (backgroundColor == null)
                    backgroundColor = "#d9edf7";
            }
            else if (classes.Any(o => o.Equals(Constant.BGWarning)))
            {
                Application.Current.Resources?.TryGetValue(Constant.BGWarning, out backgroundColor);
                if (backgroundColor == null)
                    backgroundColor = "#fcf8e3";
            }
            else if (classes.Any(o => o.Equals(Constant.BGDanger)))
            {
                Application.Current.Resources?.TryGetValue(Constant.BGDanger, out backgroundColor);
                if (backgroundColor == null)
                    backgroundColor = "#f2dede";
            }

            if (backgroundColor != null)
                Control.Layer.BackgroundColor = Color.FromHex(backgroundColor.ToString()).ToCGColor();
        }
    }
}
