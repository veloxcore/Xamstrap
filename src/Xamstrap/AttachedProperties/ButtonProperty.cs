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
    }
}
