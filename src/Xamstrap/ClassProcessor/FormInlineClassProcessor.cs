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
        public static void ProcessInlineClass(this Layout<View> element, double x, double y, double width, double height)
        {
            double xPos = x, yPos = y;
            double lastChildHeight = 0;
            double totalWidth = 0;

            foreach (var child in element.Children)
            {
                var request = child.Measure(width, height);
                var childWidth = request.Request.Width;
                var childHeight = request.Request.Height;

                lastChildHeight = Math.Max(childHeight + child.Margin.VerticalThickness, lastChildHeight);

                totalWidth += childWidth + child.Margin.HorizontalThickness;
                if (totalWidth > width)
                {
                    yPos += lastChildHeight;
                    xPos = x;
                    totalWidth = 0;
                }

                var region = new Rectangle(xPos + child.Margin.Left, yPos + child.Margin.Top, childWidth, childHeight);
                child.Layout(region);               
                xPos += childWidth + child.Margin.HorizontalThickness;
            }
        }

        public static SizeRequest ProcessFormInlineSizeRequest(this Layout<View> element, double widthConstraint, double heightConstraint)
        {
            if (element.WidthRequest > 0)
                widthConstraint = Math.Min(element.WidthRequest, widthConstraint);
            if (element.HeightRequest > 0)
                heightConstraint = Math.Min(element.HeightRequest, heightConstraint);

            double internalHeight = double.IsPositiveInfinity(heightConstraint) ? double.PositiveInfinity : Math.Max(0, heightConstraint);
            double internalWidth = double.IsPositiveInfinity(widthConstraint) ? double.PositiveInfinity : Math.Max(0, widthConstraint);

            // Measure children height
            double height = 0d;
            double lastChildHeight = 0d;
            double totalWidth = 0;
            double totalElementHeight = 0d;
            foreach (var child in element.Children)
            {
                var size = child.Measure(internalWidth, internalHeight);

                lastChildHeight = Math.Max(size.Request.Height + child.Margin.VerticalThickness, lastChildHeight);
                totalWidth += size.Request.Width + child.Margin.HorizontalThickness;
                totalElementHeight = lastChildHeight;
            }

            if (totalWidth > internalWidth)
            {
                totalElementHeight = lastChildHeight * 2;
            }
            height += element.Padding.VerticalThickness + totalElementHeight;

            return new SizeRequest(new Size(internalWidth, height), new Size(0, 0));
        }

    }
}
