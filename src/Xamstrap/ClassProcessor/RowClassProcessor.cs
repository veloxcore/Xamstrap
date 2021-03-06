﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamstrap.AttachedProperties;
using static Xamstrap.Enums;

namespace Xamstrap.ClassProcessor
{
    public static partial class Extension
    {
        public static void ProcessRowClass(this Layout<View> element, double x, double y, double width, double height)
        {
            DeviceSize device = Common.GetCurrentDeviceSize();
            double xPos = x, yPos = y, totalChildRowWidth = 0, lastChildHeight = 0;
            foreach (var child in element.Children.Where(o => o.IsVisible.Equals(true)))
            {
                int columnsGrid = -1;
                int columnsOffsetGrid = 0;
                Dictionary<DeviceSize, int> columnsData = GetColumns(child);
                Dictionary<DeviceSize, int> columnsOffsetData = GetColumnsOffset(child);

                int currentDeviceSize = (int)device;
                for (int i = currentDeviceSize; i >= 0; i--)
                {
                    columnsGrid = columnsData[(DeviceSize)Enum.Parse(typeof(DeviceSize), i.ToString())];
                    if (columnsGrid > 0)
                        break;
                }

                if (columnsGrid <= 0)
                    columnsGrid = 12;

                for (int i = currentDeviceSize; i >= 0; i--)
                {
                    columnsOffsetGrid = columnsOffsetData[(DeviceSize)Enum.Parse(typeof(DeviceSize), i.ToString())];
                    if (columnsOffsetGrid > -1)
                        break;
                }

                double columnWidthRequest = columnsGrid / 12d;

                //Calculate total height and width of current child considering all its child's height and width
                var request = child.Measure(width, height);

                double childWidth;
                if (child.WidthRequest > -1)
                {
                    childWidth = child.WidthRequest;
                }
                else
                {
                    childWidth = width * columnWidthRequest - child.Margin.HorizontalThickness;
                }               

                double childHeight = request.Request.Height;
                double childOffsetWidth;

                if (columnsOffsetGrid > -1)
                {
                    childOffsetWidth = (columnsOffsetGrid / 12d) * width;
                    totalChildRowWidth += childOffsetWidth;
                    xPos += childOffsetWidth;
                }
                totalChildRowWidth += childWidth + child.Margin.HorizontalThickness;

                // Hack: as totalChildRowWidth and Width both coming as same number but minor difference in decimal places, hence comparing it with 0.1
                if (totalChildRowWidth - width > 0.1)
                {
                    yPos += lastChildHeight;
                    lastChildHeight = childHeight + child.Margin.VerticalThickness;
                    xPos = x;
                    totalChildRowWidth = childWidth + child.Margin.HorizontalThickness;
                }

                var region = new Rectangle(xPos + child.Margin.Left, yPos + child.Margin.Top, childWidth, childHeight);
                child.Layout(region);

                if (totalChildRowWidth <= width)
                    lastChildHeight = Math.Max(childHeight + child.Margin.VerticalThickness, lastChildHeight);
                else
                    lastChildHeight = childHeight + child.Margin.VerticalThickness;

                xPos += region.Width + child.Margin.HorizontalThickness;
            }
        }

        public static SizeRequest ProcessRowSizeRequest(this Layout<View> element, double widthConstraint, double heightConstraint)
        {
            if (element.WidthRequest > 0)
                widthConstraint = Math.Min(element.WidthRequest, widthConstraint);
            if (element.HeightRequest > 0)
                heightConstraint = Math.Min(element.HeightRequest, heightConstraint);

            double internalHeight = double.IsPositiveInfinity(heightConstraint) ? double.PositiveInfinity : Math.Max(0, heightConstraint);
            double width = double.IsPositiveInfinity(widthConstraint) ? double.PositiveInfinity : Math.Max(0, widthConstraint);

            // Measure children height
            DeviceSize device = Common.GetCurrentDeviceSize();
            double totalChildRowWidth = 0;
            double lastChildHeight = 0;
            double height = 0;
            foreach (var child in element.Children.Where(o => o.IsVisible))
            {
                int columnsGrid = -1;
                int columnsOffsetGrid = 0;
                Dictionary<DeviceSize, int> columnsData = GetColumns(child);
                Dictionary<DeviceSize, int> columnsOffsetData = GetColumnsOffset(child);

                int currentDeviceSize = (int)device;
                for (int i = currentDeviceSize; i >= 0; i--)
                {
                    columnsGrid = columnsData[(DeviceSize)Enum.Parse(typeof(DeviceSize), i.ToString())];
                    if (columnsGrid > 0)
                        break;
                }

                if (columnsGrid <= 0)
                    columnsGrid = 12;

                for (int i = currentDeviceSize; i >= 0; i--)
                {
                    columnsOffsetGrid = columnsOffsetData[(DeviceSize)Enum.Parse(typeof(DeviceSize), i.ToString())];
                    if (columnsOffsetGrid > -1)
                        break;
                }

                double columnWidthRequest = columnsGrid / 12d;

                //Calculate total height and width of current child considering all its child's height and width
                var request = child.Measure(width, internalHeight);

                double childWidth;
                if (child.WidthRequest > -1)
                    childWidth = child.WidthRequest + child.Margin.HorizontalThickness;
                else
                    childWidth = width * columnWidthRequest - child.Margin.HorizontalThickness;

                double childHeight = request.Request.Height + child.Margin.VerticalThickness;
                double childOffsetWidth;

                if (columnsOffsetGrid > -1)
                {
                    childOffsetWidth = (columnsOffsetGrid / 12d) * width;
                    totalChildRowWidth += childOffsetWidth;
                }
                totalChildRowWidth += childWidth;

                // Hack: as totalChildRowWidth and Width both coming as same number but minor difference in decimal places, hence comparing it with 0.1
                if (totalChildRowWidth - width > 0.1)
                {
                    height += lastChildHeight;
                    lastChildHeight = childHeight;
                    totalChildRowWidth = childWidth;
                }
                else
                {
                    lastChildHeight = Math.Max(childHeight, lastChildHeight);
                }
            }
            height += lastChildHeight;
            height += element.Padding.VerticalThickness;

            width = double.IsInfinity(width) ? Common.GetCurrentScreenWidth() : width;
            return new SizeRequest(new Size(width, height), new Size(0, 0));
        }

        #region "Private Methods"

        private static Dictionary<Enums.DeviceSize, int> GetColumns(View element)
        {
            Dictionary<Enums.DeviceSize, int> columns = new Dictionary<Enums.DeviceSize, int>();
            foreach (Enums.DeviceSize size in Enum.GetValues(typeof(Enums.DeviceSize)))
            {
                int columnSize = GetColumnSizeByDeviceSize(element, size);
                //if (columnSize > 0 && columnSize <= 12)
                columns.Add(size, columnSize);
            }
            return columns;
        }

        private static Dictionary<DeviceSize, int> GetColumnsOffset(View element)
        {
            Dictionary<DeviceSize, int> columns = new Dictionary<DeviceSize, int>();

            foreach (DeviceSize size in Enum.GetValues(typeof(Enums.DeviceSize)))
            {
                int columnSize = GetColumnOffsetSizeByDeviceSize(element, size);
                //if (columnSize > 0 && columnSize <= 12)
                columns.Add(size, columnSize);
            }
            return columns;
        }

        private static int GetColumnOffsetSizeByDeviceSize(View element, DeviceSize deviceSize)
        {
            var property = element.GetValue(ResponsiveProperty.ClassProperty);
            int value = -1;
            if (property != null)
            {
                List<string> classes = property.ToString().Split(" ".ToCharArray()).ToList();
                string columnStartString = $"col-{deviceSize.Tag()}-offset-";

                if (classes.Any(o => o.StartsWith(columnStartString)))
                {
                    value = Convert.ToInt16(classes.Where(o => o.StartsWith(columnStartString)).First().TrimStart(columnStartString.ToCharArray()));
                }
            }

            return value;
        }

        private static int GetColumnSizeByDeviceSize(View element, Enums.DeviceSize deviceSize)
        {
            var property = element.GetValue(ResponsiveProperty.ClassProperty);
            int value = -1;
            if (property != null)
            {
                List<string> classes = property.ToString().Split(" ".ToCharArray()).ToList();
                string columnStartString = $"col-{deviceSize.Tag()}-";

                if (classes.Any(o => o.StartsWith(columnStartString) && !o.Contains("offset")))
                {
                    value = Convert.ToInt16(classes.Where(o => o.StartsWith(columnStartString) && !o.Contains("offset")).First().TrimStart(columnStartString.ToCharArray()));
                }
            }

            return value;
        }

        #endregion
    }
}
