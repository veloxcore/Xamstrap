using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamstrap.AttachedProperties
{
    public class ScrollViewPoperty
    {
        public static readonly BindableProperty HorizontalScrollBarVisibleProperty =
             BindableProperty.CreateAttached(
                 propertyName: "HorizontalScrollBarVisible",
                 returnType: typeof(Boolean),
                 declaringType: typeof(ScrollViewPoperty),
                 defaultValue: true,
                 defaultBindingMode: BindingMode.TwoWay,
                 validateValue: null);

        public static Boolean GetHorizontalScrollBarVisible(BindableObject bindable)
        {
            return (Boolean)bindable.GetValue(HorizontalScrollBarVisibleProperty);
        }

        public static void SetHorizontalScrollBarVisible(BindableObject bindable, Boolean value)
        {
            bindable.SetValue(HorizontalScrollBarVisibleProperty, value);
        }
    }
}
