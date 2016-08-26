using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamstrap.ClassProcessor
{
    public static partial class Extension
    {
        public static SizeRequest ProcessVisibilitySizeRequest(this Layout<View> element, double widthConstraint, double heightConstraint)
        {
            if (element.HeightRequest > 0)
                element.HeightRequest = 0;
            return new SizeRequest(new Size(0, 0));
        }
    }
}
