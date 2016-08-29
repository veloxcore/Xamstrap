using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamstrap
{
    public class ViewContainer : View
    {
        public static readonly BindableProperty ContentProperty =
            BindableProperty.Create("Content", typeof(Page), typeof(ViewContainer),
                defaultValue: default(Page),
                defaultBindingMode: BindingMode.TwoWay);
        public Page Content
        {
            get { return (Page)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
    }
}
