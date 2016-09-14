using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamstrap;
using Xamstrap.AttachedProperties;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Button), typeof(Xamstrap.iOS.ResponsiveButtonRenderer))]
namespace Xamstrap.iOS
{
    public class ResponsiveButtonRenderer : ButtonRenderer
    {
        private UIButton thisButton;
        private Color originalBackgroundColor;
        private Color pressedBackgroundColor;
        private Color pressedTextColor;
        private Color originalTextColor;

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            thisButton = Control as UIButton;
            if (thisButton != null)
            {
                thisButton.TitleLabel.LineBreakMode = UILineBreakMode.TailTruncation;
                thisButton.TitleLabel.Lines = 1;
            }
            if (Element != null)
            {
                List<string> classes = Element.GetValue(ResponsiveProperty.ClassProperty)?.ToString().Split(" ".ToCharArray()).ToList();

                if (classes != null)
                {
                    ProcessButtonTheme(classes);

                    ProcessButtonSize(classes);
                }

                var padding = (Xamarin.Forms.Thickness)Element.GetValue(ButtonProperty.PaddingProperty);
                if (padding.Top != -100 && padding.Left != -100 && padding.Right != -100 && padding.Left != -100)
                {
                    nfloat top = nfloat.Parse(padding.Top.ToString());
                    nfloat left = nfloat.Parse(padding.Left.ToString());
                    nfloat bottom = nfloat.Parse(padding.Bottom.ToString());
                    nfloat right = nfloat.Parse(padding.Right.ToString());
                    thisButton.ContentEdgeInsets = new UIEdgeInsets(top, left, bottom, right);
                    Element.HeightRequest = Element.FontSize + padding.VerticalThickness + 14;
                }

                // Handle PressedBackgroundColor
                object pressedBGColor;
                pressedBGColor = Element.GetValue(ButtonProperty.PressedBackgroundColorProperty);
                if (pressedBGColor != null)
                {
                    pressedBackgroundColor = (Xamarin.Forms.Color)pressedBGColor;
                    if (!pressedBackgroundColor.Equals(Color.Default))
                    {
                        // No Default value here, means set the button click background events
                        originalBackgroundColor = Element.BackgroundColor;

                        //Handle Foregroundcolor
                        pressedTextColor = (Color)Element.GetValue(ButtonProperty.PressedTextColorProperty);
                        if (!pressedTextColor.Equals(Color.Default))
                        {
                            originalTextColor = Element.TextColor;
                        }

                        thisButton.TouchDown += ButtonTouchDown;
                        thisButton.TouchUpInside += ButtonTouchUp;
                        thisButton.TouchUpOutside += ButtonTouchUp;

                        thisButton.ImageEdgeInsets = new UIEdgeInsets(thisButton.ImageEdgeInsets.Top, 0, thisButton.ImageEdgeInsets.Bottom, 0);
                    }
                }

                // Handle Horizontal Content Alignment
                var horizontalContentAlignment = (ButtonProperty.HorizontalContentAlignmentType)Element.GetValue(ButtonProperty.HorizontalContentAlignmentProperty);
                if (horizontalContentAlignment != ButtonProperty.HorizontalContentAlignmentType.PlatformDefault)
                {
                    switch (horizontalContentAlignment)
                    {
                        case ButtonProperty.HorizontalContentAlignmentType.Fill:
                            thisButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Fill;
                            break;
                        case ButtonProperty.HorizontalContentAlignmentType.Left:
                            thisButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
                            break;
                        case ButtonProperty.HorizontalContentAlignmentType.Right:
                            thisButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Right;
                            break;
                        case ButtonProperty.HorizontalContentAlignmentType.Centre:
                            thisButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
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
                            thisButton.VerticalAlignment = UIControlContentVerticalAlignment.Fill;
                            break;
                        case ButtonProperty.VerticalContentAlignmentType.Top:
                            thisButton.VerticalAlignment = UIControlContentVerticalAlignment.Top;
                            break;
                        case ButtonProperty.VerticalContentAlignmentType.Bottom:
                            thisButton.VerticalAlignment = UIControlContentVerticalAlignment.Bottom;
                            break;
                        case ButtonProperty.VerticalContentAlignmentType.Centre:
                            thisButton.VerticalAlignment = UIControlContentVerticalAlignment.Center;
                            break;
                    }
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

                ProcessSMButtonSize();
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
                    Element.TextColor = Color.FromHex("#ffffff");

                if (borderColor != null)
                    Element.BorderColor = Color.FromHex(borderColor.ToString());
                else
                    Element.BorderColor = Color.FromHex("#2e6da4");

                if (borderWidth != null)
                    Element.BorderWidth = Convert.ToInt16(borderWidth);
                else
                    Element.BorderWidth = 1;

                ProcessSMButtonSize();
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

                ProcessSMButtonSize();
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

                ProcessSMButtonSize();
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

                ProcessSMButtonSize();
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

                ProcessSMButtonSize();
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

                ProcessSMButtonSize();
            }
        }

        private void ProcessButtonSize(List<string> classes)
        {
            if (classes.Any(o => o.Equals(Constant.BtnXS)))
            {
                ProcessXSButtonSize();
            }
            else if (classes.Any(o => o.Equals(Constant.BtnSM)))
            {
                ProcessSMButtonSize();
            }
            else if (classes.Any(o => o.Equals(Constant.BtnMD)))
            {
                ProcessMDButtonSize();
            }
            else if (classes.Any(o => o.Equals(Constant.BtnLG)))
            {
                ProcessLGButtonSize();
            }
        }

        private void ProcessXSButtonSize()
        {
            Element.FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Xamarin.Forms.Button));
            nfloat top = nfloat.Parse(7.ToString());
            nfloat left = nfloat.Parse(8.ToString());
            nfloat bottom = nfloat.Parse(7.ToString());
            nfloat right = nfloat.Parse(8.ToString());
            thisButton.ContentEdgeInsets = new UIEdgeInsets(top, left, bottom, right);
            Element.HeightRequest = Element.FontSize + 16;
        }

        private void ProcessMDButtonSize()
        {
            Element.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Xamarin.Forms.Button));
            nfloat top = nfloat.Parse(15.ToString());
            nfloat left = nfloat.Parse(18.ToString());
            nfloat bottom = nfloat.Parse(15.ToString());
            nfloat right = nfloat.Parse(18.ToString());
            thisButton.ContentEdgeInsets = new UIEdgeInsets(top, left, bottom, right);
            Element.HeightRequest = Element.FontSize + 36;
        }

        private void ProcessLGButtonSize()
        {
            Element.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Xamarin.Forms.Button));
            nfloat top = nfloat.Parse(17.ToString());
            nfloat left = nfloat.Parse(21.ToString());
            nfloat bottom = nfloat.Parse(17.ToString());
            nfloat right = nfloat.Parse(21.ToString());
            thisButton.ContentEdgeInsets = new UIEdgeInsets(top, left, bottom, right);
            Element.HeightRequest = Element.FontSize + 42;
        }

        private void ProcessSMButtonSize()
        {
            Element.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Xamarin.Forms.Button));
            nfloat top = nfloat.Parse(12.ToString());
            nfloat left = nfloat.Parse(15.ToString());
            nfloat bottom = nfloat.Parse(12.ToString());
            nfloat right = nfloat.Parse(15.ToString());
            thisButton.ContentEdgeInsets = new UIEdgeInsets(top, left, bottom, right);
            Element.HeightRequest = Element.FontSize + 30;
        }

        private void ButtonTouchUp(object sender, EventArgs e)
        {
            try
            {
                thisButton.Highlighted = false;
                Element.BackgroundColor = originalBackgroundColor;
                Element.TextColor = originalTextColor;
            }
            catch (Exception)
            {
                // Do nothing, just swallow it
            }
        }

        private void ButtonTouchDown(object sender, EventArgs e)
        {
            try
            {
                thisButton.Highlighted = false;
                Element.BackgroundColor = pressedBackgroundColor;
                Element.TextColor = pressedTextColor;
            }
            catch (Exception)
            {
                // Do nothing, just swallow it
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                thisButton.TouchDown -= ButtonTouchDown;
                thisButton.TouchUpInside -= ButtonTouchDown;
                thisButton.TouchUpOutside -= ButtonTouchDown;
            }
            base.Dispose(disposing);
        }
    }
}
