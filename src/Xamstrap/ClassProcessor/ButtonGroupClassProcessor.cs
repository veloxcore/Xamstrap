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
        public static void ProcessButtonGroupClass(this Layout<View> element, double x, double y, double width, double height)
        {
            double xPos = x, yPos = y;
            double lastChildHeight = 0;
            double totalRowWidth = 0;
            foreach (var child in element.Children)
            {
                SizeRequest request = child.Measure(width, height);
                double childWidth = request.Request.Width;
                double childHeight = request.Request.Height;

                totalRowWidth += childWidth;
                if (totalRowWidth > width)
                {
                    xPos = x;
                    yPos += lastChildHeight;
                    lastChildHeight = childHeight;
                    totalRowWidth = childWidth;
                }
                else
                    lastChildHeight = Math.Max(childHeight, lastChildHeight);

                if (child is Button)
                {
                    (child as Button).BorderRadius = 1;
                }
                var region = new Rectangle(xPos, yPos, childWidth, childHeight);
                child.Layout(region);
                xPos += childWidth;
            }
        }

        public static SizeRequest ProcessButtonGroupSizeRequest(this Layout<View> element, double widthConstraint, double heightConstraint)
        {
            if (element.WidthRequest > 0)
                widthConstraint = Math.Min(element.WidthRequest, widthConstraint);
            if (element.HeightRequest > 0)
                heightConstraint = Math.Min(element.HeightRequest, heightConstraint);

            double internalHeight = double.IsPositiveInfinity(heightConstraint) ? double.PositiveInfinity : Math.Max(0, heightConstraint);
            double internalWidth = double.IsPositiveInfinity(widthConstraint) ? double.PositiveInfinity : Math.Max(0, widthConstraint);

            // Measure children height
            double height = 0d;
            double lastChildHeight = 0;
            double totalRowWidth = 0;
            foreach (var child in element.Children)
            {
                SizeRequest request = child.Measure(internalWidth, internalHeight);
                double childWidth = request.Request.Width;
                double childHeight = request.Request.Height;

                totalRowWidth += childWidth;
                if (totalRowWidth > internalWidth)
                {
                    height += lastChildHeight;
                    lastChildHeight = childHeight;
                    totalRowWidth = childWidth;
                }
                else
                    lastChildHeight = Math.Max(childHeight, lastChildHeight);

            }
            height += lastChildHeight;

            height += element.Padding.VerticalThickness;

            return new SizeRequest(new Size(internalWidth, height), new Size(0, 0));
        }
    }
}
