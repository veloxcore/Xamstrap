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
                    ProcessBackgroundColorElement(classes);

                    if (classes.Any(o => o.Equals(Constant.Row)))
                    {
                        this.ProcessRowClass(x, y, width, height);
                    }

                    if (classes.Any(o => o.Equals(Constant.FormBasic)))
                    {
                        this.ProcessFormBasicClass(x, y, width, height);
                    }

                    if (classes.Any(o => o.Equals(Constant.FormHorizontal)))
                    {
                        this.ProcessRowClass(x, y, width, height);
                    }

                    if (classes.Any(o => o.Equals(Constant.FormInLine)))
                    {
                        this.ProcessInlineClass(x, y, width, height);
                    }

                    if (classes.Any(o => o.Equals(Constant.ButtonGroup)))
                    {
                        this.ProcessButtonGroupClass(x, y, width, height);
                    }

                    if (classes.Any(o => o.Equals(Constant.ButtonGroupJustified)))
                    {
                        this.ProcessButtonJustifiedClass(x, y, width, height);
                    }

                    if (classes.Any(o => o.Equals(Constant.Panel)))
                    {

                    }

                    if (classes.Any(o => o.Equals(Constant.InputGroup)))
                    {
                        this.ProcessInputGroupClass(x, y, width, height);
                    }
                }
            }
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            List<string> classes = this.GetValue(ResponsiveProperty.ClassProperty)?.ToString().Split(" ".ToCharArray()).ToList();

            Enums.DeviceSize deviceSize = Common.GetCurrentDeviceSize();

            SizeRequest sizeRequest = this.ProcessCommonSizeRequest(widthConstraint, heightConstraint);

            if (classes != null)
            {
                if (classes.Any(o => o.Equals($"hidden-{deviceSize.Tag()}")))
                {
                    if (this.HeightRequest > 0)
                        OriginalElementHeight = this.HeightRequest;
                    sizeRequest = this.ProcessVisibilitySizeRequest(widthConstraint, heightConstraint);
                }
                else if (OriginalElementHeight > 0)
                    this.HeightRequest = OriginalElementHeight;

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

        #region "Private Methods"

        private void ProcessBackgroundColorElement(List<string> classes)
        {
            object backgroundColor = null;
            if (classes.Any(o => o.Equals(Constant.BGPrimary)))
            {
                Application.Current.Resources?.TryGetValue(Constant.BGPrimary, out backgroundColor);
                if (backgroundColor == null)
                    backgroundColor = "#337ab7";
            }
            else if (classes.Any(o => o.Equals(Constant.BGSuccess)))
            {
                Application.Current.Resources?.TryGetValue(Constant.BGSuccess, out backgroundColor);
                if (backgroundColor == null)
                    backgroundColor = "#dff0d8";
            }
            else if (classes.Any(o => o.Equals(Constant.BGInfo)))
            {
                Application.Current.Resources?.TryGetValue(Constant.BGInfo, out backgroundColor);
                if (backgroundColor == null)
                    backgroundColor = "#d9edf7";
            }
            else if (classes.Any(o => o.Equals(Constant.BGWarning)))
            {
                Application.Current.Resources?.TryGetValue(Constant.BGWarning, out backgroundColor);
                if (backgroundColor == null)
                    backgroundColor = "#fcf8e3";
            }
            else if (classes.Any(o => o.Equals(Constant.BGDanger)))
            {
                Application.Current.Resources?.TryGetValue(Constant.BGDanger, out backgroundColor);
                if (backgroundColor == null)
                    backgroundColor = "#f2dede";
            }

            if (backgroundColor != null)
                this.BackgroundColor = Color.FromHex(backgroundColor.ToString());
        }

        #endregion
    }
}