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
        public static void ProcessButtonJustifiedClass(this Layout<View> element, double x, double y, double width, double height)
        {
            int buttonCounts = element.Children.Count;
            double xPos = x;
            foreach (var child in element.Children)
            {
                var request = child.Measure(width, height);
                double childWidth = width / buttonCounts;
                double childHeight = request.Request.Height;
                if (child is Button)
                {
                    (child as Button).BorderRadius = 1;
                }
                var region = new Rectangle(xPos, y, childWidth, childHeight);
                child.Layout(region);
                xPos += childWidth;
            }
        }

        public static SizeRequest ProcessButtonJustifiedSizeRequest(this Layout<View> element, double widthConstraint, double heightConstraint)
        {
            if (element.WidthRequest > 0)
                widthConstraint = Math.Min(element.WidthRequest, widthConstraint);
            if (element.HeightRequest > 0)
                heightConstraint = Math.Min(element.HeightRequest, heightConstraint);

            double internalHeight = double.IsPositiveInfinity(heightConstraint) ? double.PositiveInfinity : Math.Max(0, heightConstraint);
            double internalWidth = double.IsPositiveInfinity(widthConstraint) ? double.PositiveInfinity : Math.Max(0, widthConstraint);

            double height = 0;

            foreach (var child in element.Children)
            {
                SizeRequest request = child.Measure(internalWidth, internalHeight);
                double childHeight = request.Request.Height;
                height = Math.Max(childHeight, height);
            }

            height += element.Padding.VerticalThickness;

            return new SizeRequest(new Size(internalWidth, height), new Size(0, 0));
        }
    }
}
