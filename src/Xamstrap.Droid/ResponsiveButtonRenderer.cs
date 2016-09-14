using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.Text.TextUtils;
using Xamstrap;
using Xamstrap.AttachedProperties;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Button), typeof(Xamstrap.Droid.ResponsiveButtonRenderer))]
namespace Xamstrap.Droid
{
    public class ResponsiveButtonRenderer : ButtonRenderer
    {
        Android.Widget.Button thisButton;
        private Color originalBackgroundColor;
        private Color pressedBackgroundColor;
        private Color pressedTextColor;
        private Color originalTextColor;
        private bool isToggleButton;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            thisButton = Control as Android.Widget.Button;
            if (thisButton != null)
            {
                thisButton.SetMaxLines(1);
                thisButton.Ellipsize = TruncateAt.End;
            }
            List<string> classes = Element.GetValue(ResponsiveProperty.ClassProperty)?.ToString().Split(" ".ToCharArray()).ToList();
            if (classes != null)
            {
                ProcessButtonTheme(classes);

                ProcessButtonSize(classes);
            }

            //Handle Button Pading
            var padding = (Thickness)Element.GetValue(ButtonProperty.PaddingProperty);
            if (padding.Left != -100 && padding.Top != -100 && padding.Right != -100 && padding.Bottom != -100)
            {
                int top = int.Parse(padding.Top.ToString());
                int left = int.Parse(padding.Left.ToString());
                int bottom = int.Parse(padding.Bottom.ToString());
                int right = int.Parse(padding.Right.ToString());

                thisButton.SetPadding(left, top, right, bottom);
                Element.HeightRequest = Element.FontSize + padding.VerticalThickness + 10;
            }


            // Handle PressedBackgroundColor
            pressedBackgroundColor = (Xamarin.Forms.Color)Element.GetValue(ButtonProperty.PressedBackgroundColorProperty);
            if (!pressedBackgroundColor.Equals(Color.Default))
            {
                originalBackgroundColor = Element.BackgroundColor;

                //Handle TextColor
                pressedTextColor = (Xamarin.Forms.Color)Element.GetValue(ButtonProperty.PressedTextColorProperty);
                if (!pressedTextColor.Equals(Color.Default))
                    originalTextColor = Element.TextColor;

                thisButton.Touch += ButtonTouched;
                thisButton.SetSingleLine(true);
                isToggleButton = Convert.ToBoolean(Element.GetValue(ButtonProperty.IsToggleButtonProperty));
            }          

            // Handle Horizontal Content Alignment
            var horizontalContentAlignment = (ButtonProperty.HorizontalContentAlignmentType)Element.GetValue(ButtonProperty.HorizontalContentAlignmentProperty);
            if (horizontalContentAlignment != ButtonProperty.HorizontalContentAlignmentType.PlatformDefault)
            {
                switch (horizontalContentAlignment)
                {
                    case ButtonProperty.HorizontalContentAlignmentType.Fill:
                        thisButton.Gravity = thisButton.Gravity | GravityFlags.FillHorizontal;
                        break;
                    case ButtonProperty.HorizontalContentAlignmentType.Left:
                        thisButton.Gravity = thisButton.Gravity | GravityFlags.Left;
                        break;
                    case ButtonProperty.HorizontalContentAlignmentType.Right:
                        thisButton.Gravity = thisButton.Gravity | GravityFlags.Right;
                        break;
                    case ButtonProperty.HorizontalContentAlignmentType.Centre:
                        thisButton.Gravity = thisButton.Gravity | GravityFlags.CenterHorizontal;
                        break;
                }
            }

            // Handle Vertical Content Alignment
            var verticalContentAlignment = (ButtonProperty.VerticalContentAlignmentType)Element.GetValue(ButtonProperty.VerticalContentAlignmentProperty);
            if (verticalContentAlignment != ButtonProperty.VerticalContentAlignmentType.PlatformDefault)
            {
                switch (verticalContentAlignment)
                {
                    case ButtonProperty.VerticalContentAlignmentType.Fill:
                        thisButton.Gravity = thisButton.Gravity | GravityFlags.FillVertical;
                        break;
                    case ButtonProperty.VerticalContentAlignmentType.Top:
                        thisButton.Gravity = thisButton.Gravity | GravityFlags.Top;
                        break;
                    case ButtonProperty.VerticalContentAlignmentType.Bottom:
                        thisButton.Gravity = thisButton.Gravity | GravityFlags.Bottom;
                        break;
                    case ButtonProperty.VerticalContentAlignmentType.Centre:
                        thisButton.Gravity = thisButton.Gravity | GravityFlags.CenterVertical;
                        break;
                }
            }

        }

        private void ProcessButtonTheme(List<string> classes)
        {
            if (classes.Any(o => o.Equals(Constant.BtnDefault)))
            {
                object backgroundColor = null;
                object textColor = null;
                object borderColor = null;
                object borderWidth = null;
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnDefaultBackgroundColor, out backgroundColor);
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnDefaultTextColor, out textColor);
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnDefaultBorderColor, out borderColor);
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnDefaultBorderWidth, out borderWidth);

                if (backgroundColor != null)
                    Element.BackgroundColor = Color.FromHex(backgroundColor.ToString());
                else
                    Element.BackgroundColor = Color.FromHex("#fff");

                if (textColor != null)
                    Element.TextColor = Color.FromHex(textColor.ToString());
                else
                    Element.TextColor = Color.FromHex("#333");

                if (borderColor != null)
                    Element.BorderColor = Color.FromHex(borderColor.ToString());
                else
                    Element.BorderColor = Color.FromHex("#ccc");

                if (borderWidth != null)
                    Element.BorderWidth = Convert.ToInt16(borderWidth);
                else
                    Element.BorderWidth = 1;
            }
            else if (classes.Any(o => o.Equals(Constant.BtnPrimary)))
            {
                object backgroundColor = null;
                object textColor = null;
                object borderColor = null;
                object borderWidth = null;
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnPrimaryBackgroundColor, out backgroundColor);
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnPrimaryTextColor, out textColor);
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnPrimaryBorderColor, out borderColor);
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnPrimaryBorderWidth, out borderWidth);

                if (backgroundColor != null)
                    Element.BackgroundColor = Color.FromHex(backgroundColor.ToString());
                else
                    Element.BackgroundColor = Color.FromHex("#337ab7");

                if (textColor != null)
                    Element.TextColor = Color.FromHex(textColor.ToString());
                else
                    Element.TextColor = Color.FromHex("#fff");

                if (borderColor != null)
                    Element.BorderColor = Color.FromHex(borderColor.ToString());
                else
                    Element.BorderColor = Color.FromHex("#2e6da4");

                if (borderWidth != null)
                    Element.BorderWidth = Convert.ToInt16(borderWidth);
                else
                    Element.BorderWidth = 1;
            }
            else if (classes.Any(o => o.Equals(Constant.BtnSuccess)))
            {
                object backgroundColor = null;
                object textColor = null;
                object borderColor = null;
                object borderWidth = null;
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnSuccessBackgroundColor, out backgroundColor);
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnSuccessTextColor, out textColor);
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnSuccessBorderColor, out borderColor);
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnSuccessBorderWidth, out borderWidth);

                if (backgroundColor != null)
                    Element.BackgroundColor = Color.FromHex(backgroundColor.ToString());
                else
                    Element.BackgroundColor = Color.FromHex("#5cb85c");

                if (textColor != null)
                    Element.TextColor = Color.FromHex(textColor.ToString());
                else
                    Element.TextColor = Color.FromHex("#fff");

                if (borderColor != null)
                    Element.BorderColor = Color.FromHex(borderColor.ToString());
                else
                    Element.BorderColor = Color.FromHex("#4cae4c");

                if (borderWidth != null)
                    Element.BorderWidth = Convert.ToInt16(borderWidth);
                else
                    Element.BorderWidth = 1;
            }
            else if (classes.Any(o => o.Equals(Constant.BtnInfo)))
            {
                object backgroundColor = null;
                object textColor = null;
                object borderColor = null;
                object borderWidth = null;
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnInfoBackgroundColor, out backgroundColor);
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnInfoTextColor, out textColor);
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnInfoBorderColor, out borderColor);
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnInfoBorderWidth, out borderWidth);

                if (backgroundColor != null)
                    Element.BackgroundColor = Color.FromHex(backgroundColor.ToString());
                else
                    Element.BackgroundColor = Color.FromHex("#5bc0de");

                if (textColor != null)
                    Element.TextColor = Color.FromHex(textColor.ToString());
                else
                    Element.TextColor = Color.FromHex("#fff");

                if (borderColor != null)
                    Element.BorderColor = Color.FromHex(borderColor.ToString());
                else
                    Element.BorderColor = Color.FromHex("#46b8da");

                if (borderWidth != null)
                    Element.BorderWidth = Convert.ToInt16(borderWidth);
                else
                    Element.BorderWidth = 1;
            }
            else if (classes.Any(o => o.Equals(Constant.BtnWarning)))
            {
                object backgroundColor = null;
                object textColor = null;
                object borderColor = null;
                object borderWidth = null;
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnWarningBackgroundColor, out backgroundColor);
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnWarningTextColor, out textColor);
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnWarningBorderColor, out borderColor);
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnWarningBorderWidth, out borderWidth);

                if (backgroundColor != null)
                    Element.BackgroundColor = Color.FromHex(backgroundColor.ToString());
                else
                    Element.BackgroundColor = Color.FromHex("#f0ad4e");

                if (textColor != null)
                    Element.TextColor = Color.FromHex(textColor.ToString());
                else
                    Element.TextColor = Color.FromHex("#fff");

                if (borderColor != null)
                    Element.BorderColor = Color.FromHex(borderColor.ToString());
                else
                    Element.BorderColor = Color.FromHex("#eea236");

                if (borderWidth != null)
                    Element.BorderWidth = Convert.ToInt16(borderWidth);
                else
                    Element.BorderWidth = 1;
            }
            else if (classes.Any(o => o.Equals(Constant.BtnDanger)))
            {
                object backgroundColor = null;
                object textColor = null;
                object borderColor = null;
                object borderWidth = null;
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnDangerBackgroundColor, out backgroundColor);
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnDangerTextColor, out textColor);
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnDangerBorderColor, out borderColor);
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnDangerBorderWidth, out borderWidth);

                if (backgroundColor != null)
                    Element.BackgroundColor = Color.FromHex(backgroundColor.ToString());
                else
                    Element.BackgroundColor = Color.FromHex("#d9534f");

                if (textColor != null)
                    Element.TextColor = Color.FromHex(textColor.ToString());
                else
                    Element.TextColor = Color.FromHex("#fff");

                if (borderColor != null)
                    Element.BorderColor = Color.FromHex(borderColor.ToString());
                else
                    Element.BorderColor = Color.FromHex("#d43f3a");

                if (borderWidth != null)
                    Element.BorderWidth = Convert.ToInt16(borderWidth);
                else
                    Element.BorderWidth = 1;
            }
            else if (classes.Any(o => o.Equals(Constant.BtnLink)))
            {
                object backgroundColor = null;
                object textColor = null;
                object borderColor = null;
                object borderWidth = null;
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnLinkBackgroundColor, out backgroundColor);
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnLinkTextColor, out textColor);
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnLinkBorderColor, out borderColor);
                Xamarin.Forms.Application.Current.Resources?.TryGetValue(Constant.BtnLinkBorderWidth, out borderWidth);

                if (backgroundColor != null)
                    Element.BackgroundColor = Color.FromHex(backgroundColor.ToString());
                else
                    Element.BackgroundColor = Color.Transparent;

                if (textColor != null)
                    Element.TextColor = Color.FromHex(textColor.ToString());
                else
                    Element.TextColor = Color.FromHex("#337ab7");

                if (borderColor != null)
                    Element.BorderColor = Color.FromHex(borderColor.ToString());

                if (borderWidth != null)
                    Element.BorderWidth = Convert.ToInt16(borderWidth);
            }
        }

        private void ProcessButtonSize(List<string> classes)
        {
            if (classes.Any(o => o.Equals(Constant.BtnXS)))
            {
                Element.FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Xamarin.Forms.Button));
                thisButton.SetPadding(8, 7, 8, 7);
                Element.HeightRequest = Element.FontSize + 14 + 10;
            }
            else if (classes.Any(o => o.Equals(Constant.BtnSM)))
            {
                Element.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Xamarin.Forms.Button));
            }
            else if (classes.Any(o => o.Equals(Constant.BtnMD)))
            {
                Element.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Xamarin.Forms.Button));
                thisButton.SetPadding(18, 15, 18, 15);
                Element.HeightRequest = Element.FontSize + 30 + 10;
            }
            else if (classes.Any(o => o.Equals(Constant.BtnLG)))
            {
                Element.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Xamarin.Forms.Button));
                thisButton.SetPadding(21, 17, 21, 17);
                Element.HeightRequest = Element.FontSize + 34 + 10;
            }
        }

        private void ButtonTouched(object sender, TouchEventArgs e)
        {
            if (e.Event.Action == MotionEventActions.Down)
            {
                Element.BackgroundColor = pressedBackgroundColor;
                Element.TextColor = pressedTextColor;
            }
            else if (e.Event.Action == MotionEventActions.Up && !isToggleButton)
            {
                Element.BackgroundColor = originalBackgroundColor;
                Element.TextColor = originalTextColor;
            }
            e.Handled = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                thisButton.Touch -= ButtonTouched;
            }
            base.Dispose(disposing);
        }
    }
}