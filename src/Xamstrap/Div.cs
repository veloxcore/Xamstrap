using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamstrap.ClassProcessor;
using System.Runtime.CompilerServices;
using Xamstrap.AttachedProperties;

namespace Xamstrap
{
    public class Div : Layout<View>
    {
        public double OriginalElementHeight { get; set; } = -1;

        #region "Protected Methods"
        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            List<string> classes = this.GetValue(ResponsiveProperty.ClassProperty)?.ToString().Split(" ".ToCharArray()).ToList();
            if ((width != 0 && height != 0) || !classes.Any(o => o.Equals($"hidden-{Common.GetCurrentDeviceSize().Tag()}")))
            {
                width = width - this.Padding.HorizontalThickness;
                x += this.Padding.Left;
                y += this.Padding.Top;

                this.ProcessCommonClass(x, y, width, height);

                if (classes != null)
                {
                    if (classes.Any(o => o.Equals(Constant.Row)))
                        this.ProcessRowClass(x, y, width, height);

                    if (classes.Any(o => o.Equals(Constant.FormBasic)))
                        this.ProcessFormBasicClass(x, y, width, height);

                    if (classes.Any(o => o.Equals(Constant.FormHorizontal)))
                        this.ProcessRowClass(x, y, width, height);

                    if (classes.Any(o => o.Equals(Constant.FormInLine)))
                        this.ProcessInlineClass(x, y, width, height);

                    if (classes.Any(o => o.Equals(Constant.ButtonGroup)))
                        this.ProcessButtonGroupClass(x, y, width, height);

                    if (classes.Any(o => o.Equals(Constant.ButtonGroupJustified)))
                        this.ProcessButtonJustifiedClass(x, y, width, height);

                    if (classes.Any(o => o.Equals(Constant.InputGroup)))
                        this.ProcessInputGroupClass(x, y, width, height);

                    if (classes.Any(o => o.Equals(Constant.Panel)))
                    {
                        //Not Yet Implemented 
                    }
                }
            }
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            List<string> classes = this.GetValue(ResponsiveProperty.ClassProperty)?.ToString().Split(" ".ToCharArray()).ToList();

            Enums.DeviceSize deviceSize = Common.GetCurrentDeviceSize();

            SizeRequest sizeRequest = new SizeRequest();

            if (!double.IsPositiveInfinity(widthConstraint))
                sizeRequest = this.ProcessCommonSizeRequest(widthConstraint, heightConstraint);

            if (classes != null && !double.IsPositiveInfinity(widthConstraint))
            {
                if (classes.Any(o => o.Equals(Constant.Row)) || classes.Any(o => o.Equals(Constant.FormHorizontal)))
                    sizeRequest = this.ProcessRowSizeRequest(widthConstraint, heightConstraint);

                if (classes.Any(o => o.Equals(Constant.FormBasic)))
                    sizeRequest = this.ProcessFormBasicSizeRequest(widthConstraint, heightConstraint);

                if (classes.Any(o => o.Equals(Constant.ButtonGroup)))
                    sizeRequest = this.ProcessButtonGroupSizeRequest(widthConstraint, heightConstraint);

                if (classes.Any(o => o.Equals(Constant.ButtonGroupJustified)))
                    sizeRequest = this.ProcessButtonJustifiedSizeRequest(widthConstraint, heightConstraint);

                if (classes.Any(o => o.Equals(Constant.FormInLine)))
                    sizeRequest = this.ProcessFormInlineSizeRequest(widthConstraint, heightConstraint);

                if (classes.Any(o => o.Equals(Constant.InputGroup)))
                    sizeRequest = this.ProcessInputGroupSizeRequest(widthConstraint, heightConstraint);

            }

            return sizeRequest;
        }

        #endregion
    }
}