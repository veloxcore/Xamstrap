using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamstrap.AttachedProperties
{
    public class ResponsiveProperty
    {
        public static readonly BindableProperty ClassProperty =
          BindableProperty.CreateAttached(
              propertyName: "Class",
              returnType: typeof(string),
              declaringType: typeof(ResponsiveProperty),
              defaultValue: default(string),
              defaultBindingMode: BindingMode.TwoWay,
              validateValue: null);

        public static string GetClass(BindableObject bindable)
        {
            return (string)bindable.GetValue(ClassProperty);
        }

        public static void SetClass(BindableObject bindable, string value)
        {
            bindable.SetValue(ClassProperty, value);
        }
    }
}
