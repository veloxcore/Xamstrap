using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamstrap;
using Xamstrap.AttachedProperties;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Label), typeof(Xamstrap.iOS.ResponsiveLabelRenderer))]
namespace Xamstrap.iOS
{
    public class ResponsiveLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            List<string> classes = Element?.GetValue(ResponsiveProperty.ClassProperty)?.ToString().Split(" ".ToCharArray()).ToList();

            if (classes != null)
            {
                ProcessLabelTextColor(classes);
            }
        }

        private void ProcessLabelTextColor(List<string> classes)
        {
            object textColor = null;

            if (classes.Any(o => o.Equals(Constant.TextMuted)))
            {
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.TextMuted, out textColor);

                if (textColor == null)
                    textColor = "#777";
            }
            else if (classes.Any(o => o.Equals(Constant.TextPrimary)))
            {
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.TextPrimary, out textColor);

                if (textColor == null)
                    textColor = "#337ab7";
            }
            else if (classes.Any(o => o.Equals(Constant.TextSuccess)))
            {
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.TextSuccess, out textColor);

                if (textColor == null)
                    textColor = "#3c763d";
            }
            else if (classes.Any(o => o.Equals(Constant.TextInfo)))
            {
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.TextInfo, out textColor);

                if (textColor == null)
                    textColor = "#31708f";
            }
            else if (classes.Any(o => o.Equals(Constant.TextWarning)))
            {
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.TextWarning, out textColor);

                if (textColor == null)
                    textColor = "#8a6d3b";
            }
            else if (classes.Any(o => o.Equals(Constant.TextDanger)))
            {
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.TextDanger, out textColor);

                if (textColor == null)
                    textColor = "#a94442";
            }

            if (textColor != null)
                Element.TextColor = Color.FromHex(textColor.ToString());
        }

    }
}
