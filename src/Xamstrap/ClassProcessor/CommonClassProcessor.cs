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
        public static Layout<View> ProcessCommonClass(this Layout<View> element, double x, double y, double width, double height)
        {
            foreach (var child in element.Children.Where(o => o.IsVisible))
            {
                var request = child.Measure(width, height);
                double childWidth = request.Request.Width;
                double childHeight = request.Request.Height;

                var region = new Rectangle(x, y, childWidth, childHeight);
                child.Layout(region);
                y += childHeight;
            }

            return element;
        }

        public static SizeRequest ProcessCommonSizeRequest(this Layout<View> element, double widthConstraint, double heightConstraint)
        {
            if (element.WidthRequest > 0)
                widthConstraint = Math.Min(element.WidthRequest, widthConstraint);
            if (element.HeightRequest > 0)
                heightConstraint = Math.Min(element.HeightRequest, heightConstraint);

            double internalHeight = double.IsPositiveInfinity(heightConstraint) ? double.PositiveInfinity : Math.Max(0, heightConstraint);
            double internalWidth = double.IsPositiveInfinity(widthConstraint) ? double.PositiveInfinity : Math.Max(0, widthConstraint);
            double height = 0d;

            if (element.Children.Count == 0)
            {
                internalWidth = Math.Max(0, Math.Min(element.WidthRequest, widthConstraint));
                height = internalHeight = Math.Max(0, Math.Min(element.HeightRequest, heightConstraint));
            }
            // Measure children height
            foreach (var child in element.Children)
            {
                var size = child.Measure(internalWidth, internalHeight);
                height += size.Request.Height;
            }

            height += element.Padding.VerticalThickness;

            return new SizeRequest(new Size(internalWidth, height), new Size(0, 0));
        }
    }
}
