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
        public static void ProcessInputGroupClass(this Layout<View> element, double x, double y, double width, double height)
        {
            double xPos = x, yPos = y;
            double entryHeight = 0;
            double totalLabelWidth = 0;
            double totalButtonWidth = 0;

            if (element.Children.Any(o => o is Entry))
            {
                var entry = (element.Children.First(o => o is Entry) as Entry);
                var request = entry.Measure(width, height);
                entryHeight = request.Request.Height;
            }

            //processing total label width
            foreach (var child in element.Children)
            {
                if (child is Label)
                {
                    var label = (element.Children.First(o => o is Label) as Label);
                    var request = label.Measure(width, height);
                    totalLabelWidth += request.Request.Width;
                }
            }

            //processing total button width
            foreach (var child in element.Children)
            {
                if (child is Button)
                {
                    var button = (element.Children.First(o => o is Button) as Button);
                    var request = button.Measure(width, height);
                    totalButtonWidth += request.Request.Width;
                }
            }
            foreach (var child in element.Children)
            {
                var request = child.Measure(width, height);
                var childWidth = request.Request.Width;
                var childHeight = request.Request.Height;

                if (child is Label)
                {
                    yPos = yPos + (entryHeight - childHeight) / 2;
                }
                if (child is Entry)
                {
                    childWidth = width  - totalLabelWidth - totalButtonWidth;
                }
                var region = new Rectangle(xPos, yPos, childWidth, childHeight);
                child.Layout(region);
                if (child is Label)
                {
                    yPos = y;
                }
                xPos += child.Width;
            }
        }

        public static SizeRequest ProcessInputGroupSizeRequest(this Layout<View> element,double widthConstraint,double heightConstraint)
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
                height += size.Request.Height;
            }

            height += element.Padding.VerticalThickness;

            return new SizeRequest(new Size(internalWidth, height), new Size(0, 0));
        }
    }
}
