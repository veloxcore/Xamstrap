using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static Xamstrap.Enums;

namespace Xamstrap.ClassProcessor
{
    public static partial class Extension
    {
        public static void ProcessFormBasicClass(this Layout<View> element, double x, double y, double width, double height)
        {
            DeviceSize device = Common.GetCurrentDeviceSize();
            double yPos = y;

            foreach (var child in element.Children)
            {
                var request = child.Measure(width, height);
                var childWidth = width - child.Margin.HorizontalThickness;
                var childHeight = request.Request.Height;

                var region = new Rectangle(x + child.Margin.Left, yPos + child.Margin.Top, childWidth, childHeight);
                child.Layout(region);

                yPos += childHeight + child.Margin.VerticalThickness;
            }
        }

        public static SizeRequest ProcessFormBasicSizeRequest(this Layout<View> element, double widthConstraint, double heightConstraint)
        {
            if (element.WidthRequest > 0)
                widthConstraint = Math.Min(element.WidthRequest, widthConstraint);
            if (element.HeightRequest > 0)
                heightConstraint = Math.Min(element.HeightRequest, heightConstraint);

            double internalHeight = double.IsPositiveInfinity(heightConstraint) ? double.PositiveInfinity : Math.Max(0, heightConstraint);
            double internalWidth = double.IsPositiveInfinity(widthConstraint) ? double.PositiveInfinity : Math.Max(0, widthConstraint);

            // Measure children height
            double height = 0d;
            foreach (var child in element.Children)
            {
                var size = child.Measure(internalWidth, internalHeight);
                height += size.Request.Height + child.Margin.VerticalThickness;
            }

            height += element.Padding.VerticalThickness;
            return new SizeRequest(new Size(internalWidth, height), new Size(0, 0));
        }
    }
}
