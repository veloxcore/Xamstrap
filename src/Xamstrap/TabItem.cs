using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamstrap
{
    public class TabItem : Frame
    {
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create("Title", typeof(string), typeof(TabItem),
                defaultValue: string.Empty,
                defaultBindingMode: BindingMode.TwoWay,
                validateValue: null);

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create("IsSelected", typeof(Boolean), typeof(TabItem),
                defaultValue: default(Boolean),
                defaultBindingMode: BindingMode.TwoWay,
                validateValue: null,
                propertyChanged: OnIsSelectedChanged);
        public Boolean IsSelected
        {
            get { return (Boolean)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
        private static void OnIsSelectedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var item = (TabItem)bindable;
            if (newValue == oldValue)
                return;
        }	
    }
}
