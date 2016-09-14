using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamstrap.AttachedProperties
{
    public class ButtonProperty
    {
        #region Padding
        public static readonly BindableProperty PaddingProperty =
            BindableProperty.CreateAttached(
                propertyName: "Padding",
                returnType: typeof(Thickness),
                declaringType: typeof(ButtonProperty),
                defaultValue: new Thickness(-100),
                defaultBindingMode: BindingMode.TwoWay,
                validateValue: null);

        public static Thickness GetPadding(BindableObject bindable)
        {
            return (Thickness)bindable.GetValue(PaddingProperty);
        }

        public static void SetPadding(BindableObject bindable, Thickness value)
        {
            bindable.SetValue(PaddingProperty, value);
        }
        #endregion
      
        #region PressedBackgroundColor

        public static readonly BindableProperty PressedBackgroundColorProperty =
            BindableProperty.CreateAttached(
                propertyName: "PressedBackgroundColor",
                returnType: typeof(Color),
                declaringType: typeof(ButtonProperty),
                defaultValue: Color.Default,
                defaultBindingMode: BindingMode.TwoWay,
                validateValue: null);

        public static Color GetPressedBackgroundColor(BindableObject bindable)
        {
            return (Color)bindable.GetValue(PressedBackgroundColorProperty);
        }

        public static void SetPressedBackgroundColor(BindableObject bindable, Color value)
        {
            bindable.SetValue(PressedBackgroundColorProperty, value);
        }


        public static readonly BindableProperty IsToggleButtonProperty =
            BindableProperty.CreateAttached(
                propertyName: "IsToggleButton",
                returnType: typeof(bool),
                declaringType: typeof(ButtonProperty),
                defaultValue: false,
                defaultBindingMode: BindingMode.TwoWay,
                validateValue: null);

        public static bool GetIsToggleButton(BindableObject bindable)
        {
            return (bool)bindable.GetValue(IsToggleButtonProperty);
        }

        public static void SetIsToggleButton(BindableObject bindable, bool value)
        {
            bindable.SetValue(IsToggleButtonProperty, value);
        }
        #endregion

        #region HorizontalContentAlignment

        public static readonly BindableProperty HorizontalContentAlignmentProperty =
            BindableProperty.CreateAttached(
                propertyName: "HorizontalContentAlignment",
                returnType: typeof(HorizontalContentAlignmentType),
                declaringType: typeof(ButtonProperty),
                defaultValue: HorizontalContentAlignmentType.PlatformDefault,
                defaultBindingMode: BindingMode.TwoWay,
                validateValue: null);

        public static HorizontalContentAlignmentType GetHorizontalContentAlignment(BindableObject bindable)
        {
            return (HorizontalContentAlignmentType)bindable.GetValue(HorizontalContentAlignmentProperty);
        }

        public static void SetHorizontalContentAlignment(BindableObject bindable, HorizontalContentAlignmentType value)
        {
            bindable.SetValue(HorizontalContentAlignmentProperty, value);
        }

        #endregion

        #region VerticalContentAlignment

        public static readonly BindableProperty VerticalContentAlignmentProperty =
            BindableProperty.CreateAttached(
                propertyName: "VerticalContentAlignment",
                returnType: typeof(VerticalContentAlignmentType),
                declaringType: typeof(ButtonProperty),
                defaultValue: VerticalContentAlignmentType.PlatformDefault,
                defaultBindingMode: BindingMode.TwoWay,
                validateValue: null);

        public static VerticalContentAlignmentType GetVerticalContentAlignment(BindableObject bindable)
        {
            return (VerticalContentAlignmentType)bindable.GetValue(VerticalContentAlignmentProperty);
        }

        public static void SetVerticalContentAlignment(BindableObject bindable, VerticalContentAlignmentType value)
        {
            bindable.SetValue(VerticalContentAlignmentProperty, value);
        }

        #endregion

        #region Enums

        public enum HorizontalContentAlignmentType { PlatformDefault, Fill, Left, Right, Centre };
        public enum VerticalContentAlignmentType { PlatformDefault, Fill, Top, Bottom, Centre };

        #endregion

        #region PressedTextColor
        public static readonly BindableProperty PressedTextColorProperty =
            BindableProperty.CreateAttached(
                propertyName: "PressedTextColor",
                returnType: typeof(Color),
                declaringType: typeof(ButtonProperty),
                defaultValue: default(Color),
                defaultBindingMode: BindingMode.TwoWay,
                validateValue: null);

        public static Color GetPressedTextColor(BindableObject bindable)
        {
            return (Color)bindable.GetValue(PressedTextColorProperty);
        }

        public static void SetPressedTextColor(BindableObject bindable, Color value)
        {
            bindable.SetValue(PressedTextColorProperty, value);
        }
        #endregion
    }

}
