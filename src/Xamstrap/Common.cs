using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static Xamstrap.Enums;

namespace Xamstrap
{
    public class Common
    {
        private static Application OriginalApp;

        static Common()
        {
            OriginalApp = Application.Current;
        }

        public static DeviceSize GetCurrentDeviceSize()
        {
            //TODO: revisit here as it sometime not works
            return GetDeviceSize(OriginalApp.MainPage.Width);
        }

        public static double GetCurrentScreenWidth()
        {
            //TODO: revisit here as it sometime not works
            return OriginalApp.MainPage.Width;
        }

        public static DeviceSize GetDeviceSize(double width)
        {
            DeviceSize device = DeviceSize.Medium;

            if (width <= 544)
                device = DeviceSize.ExtraSmall;
            else if (width > 544 && width <= 768)
                device = DeviceSize.Small;
            else if (width > 768 && width <= 992)
                device = DeviceSize.Medium;
            else if (width > 992 && width <= 1200)
                device = DeviceSize.Large;
            else if (width > 1200)
                device = DeviceSize.ExtraLarge;

            return device;
        }

    }
}
