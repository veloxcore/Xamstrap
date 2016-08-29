using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static Xamstrap.Enums;

namespace Xamstrap
{
    public class Constant
    {
        public const string Row = "row";

        public const string FormBasic = "form-basic";
        public const string FormInLine = "form-inline";
        public const string FormHorizontal = "form-horizontal";
        public const string InputGroup = "input-group";
        public const string ButtonGroup = "btn-group";
        public const string ButtonGroupJustified = "btn-group-justified";

        public const string BtnDefault = "btn-default";
        public const string BtnDefaultBackgroundColor = "btn-default-background-color";
        public const string BtnDefaultTextColor = "btn-default-text-color";
        public const string BtnDefaultBorderColor = "btn-default-border-color";
        public const string BtnDefaultBorderWidth = "btn-default-border-width";

        public const string BtnPrimary = "btn-primary";
        public const string BtnPrimaryBackgroundColor = "btn-primary-background-color";
        public const string BtnPrimaryTextColor = "btn-primary-text-color";
        public const string BtnPrimaryBorderColor = "btn-primary-border-color";
        public const string BtnPrimaryBorderWidth = "btn-primary-border-width";

        public const string BtnSuccess = "btn-success";
        public const string BtnSuccessBackgroundColor = "btn-success-background-color";
        public const string BtnSuccessTextColor = "btn-success-text-color";
        public const string BtnSuccessBorderColor = "btn-success-border-color";
        public const string BtnSuccessBorderWidth = "btn-success-border-width";

        public const string BtnInfo = "btn-info";
        public const string BtnInfoBackgroundColor = "btn-info-background-color";
        public const string BtnInfoTextColor = "btn-info-text-color";
        public const string BtnInfoBorderColor = "btn-info-border-color";
        public const string BtnInfoBorderWidth = "btn-info-border-width";

        public const string BtnWarning = "btn-warning";
        public const string BtnWarningBackgroundColor = "btn-warning-background-color";
        public const string BtnWarningTextColor = "btn-warning-text-color";
        public const string BtnWarningBorderColor = "btn-warning-border-color";
        public const string BtnWarningBorderWidth = "btn-warning-border-width";

        public const string BtnDanger = "btn-danger";
        public const string BtnDangerBackgroundColor = "btn-danger-background-color";
        public const string BtnDangerTextColor = "btn-danger-text-color";
        public const string BtnDangerBorderColor = "btn-danger-border-color";
        public const string BtnDangerBorderWidth = "btn-danger-border-width";

        public const string BtnLink = "btn-link";
        public const string BtnLinkBackgroundColor = "btn-link-background-color";
        public const string BtnLinkTextColor = "btn-link-text-color";
        public const string BtnLinkBorderColor = "btn-link-border-color";
        public const string BtnLinkBorderWidth = "btn-link-border-width";

        public const string BtnXS ="btn-xs";
        public const string BtnSM ="btn-sm";
        public const string BtnMD ="btn-md";
        public const string BtnLG ="btn-lg";

        public const string TextMuted = "text-muted";
        public const string TextPrimary = "text-primary";
        public const string TextSuccess = "text-success";
        public const string TextInfo = "text-info";
        public const string TextWarning = "text-warning";
        public const string TextDanger = "text-danger";

        public const string BGPrimary = "bg-primary";
        public const string BGSuccess = "bg-success";
        public const string BGInfo = "bg-info";
        public const string BGWarning = "bg-warning";
        public const string BGDanger = "bg-danger";

        public const string HiddenXs = "hidden-xs";
        public const string HiddenSm = "hidden-sm";
        public const string HiddenMd = "hidden-md";
        public const string HiddenLg = "hidden-lg";
        public const string HiddenXl = "hidden-xl";

        public const string Panel = "panel";

        public const string ColMd1 = "col-md-1";
        public const string ColMd2 = "col-md-2";
        public const string ColMd3 = "col-md-3";
        public const string ColMd4 = "col-md-4";
        public const string ColMd5 = "col-md-5";
        public const string ColMd6 = "col-md-6";
        public const string ColMd7 = "col-md-7";
        public const string ColMd8 = "col-md-8";
        public const string ColMd9 = "col-md-9";
        public const string ColMd10 = "col-md-10";
        public const string ColMd11 = "col-md-11";
        public const string ColMd12 = "col-md-12";

        public const string ColSm1 = "col-sm-1";
        public const string ColSm2 = "col-sm-2";
        public const string ColSm3 = "col-sm-3";
        public const string ColSm4 = "col-sm-4";
        public const string ColSm5 = "col-sm-5";
        public const string ColSm6 = "col-sm-6";
        public const string ColSm7 = "col-sm-7";
        public const string ColSm8 = "col-sm-8";
        public const string ColSm9 = "col-sm-9";
        public const string ColSm10 = "col-sm-10";
        public const string ColSm11 = "col-sm-11";
        public const string ColSm12 = "col-sm-12";

        public const string ColXs1 = "col-xs-1";
        public const string ColXs2 = "col-xs-2";
        public const string ColXs3 = "col-xs-3";
        public const string ColXs4 = "col-xs-4";
        public const string ColXs5 = "col-xs-5";
        public const string ColXs6 = "col-xs-6";
        public const string ColXs7 = "col-xs-7";
        public const string ColXs8 = "col-xs-8";
        public const string ColXs9 = "col-xs-9";
        public const string ColXs10 = "col-xs-10";
        public const string ColXs11 = "col-xs-11";
        public const string ColXs12 = "col-xs-12";

        public const string ColLg1 = "col-lg-1";
        public const string ColLg2 = "col-lg-2";
        public const string ColLg3 = "col-lg-3";
        public const string ColLg4 = "col-lg-4";
        public const string ColLg5 = "col-lg-5";
        public const string ColLg6 = "col-lg-6";
        public const string ColLg7 = "col-lg-7";
        public const string ColLg8 = "col-lg-8";
        public const string ColLg9 = "col-lg-9";
        public const string ColLg10 = "col-lg-10";
        public const string ColLg11 = "col-lg-11";
        public const string ColLg12 = "col-lg-12";

        public const string ColXL1 = "col-xl-1";
        public const string ColXL2 = "col-xl-2";
        public const string ColXL3 = "col-xl-3";
        public const string ColXL4 = "col-xl-4";
        public const string ColXL5 = "col-xl-5";
        public const string ColXL6 = "col-xl-6";
        public const string ColXL7 = "col-xl-7";
        public const string ColXL8 = "col-xl-8";
        public const string ColXL9 = "col-xl-9";
        public const string ColXL10 = "col-xl-10";
        public const string ColXL11 = "col-xl-11";
        public const string ColXL12 = "col-xl-12";
        
    }

    public class Enums
    {
        public enum DeviceSize
        {
            [Tag("xs")]
            ExtraSmall = 0,
            [Tag("sm")]
            Small,
            [Tag("md")]
            Medium,
            [Tag("lg")]
            Large,
            [Tag("xl")]
            ExtraLarge
        }
    }

    public class TagAttribute : Attribute
    {
        public string Value { get; set; }
        public TagAttribute(String tagValue)
        {
            this.Value = tagValue;
        }
    }

    public static class Extensions
    {
        /// <summary>
        /// Gets the page to which an element belongs
        /// </summary>
        /// <returns>The page.</returns>
        /// <param name="element">Element.</param>
        public static Page GetParentPage(this VisualElement element)
        {
            if (element != null)
            {
                var parent = element.Parent;
                while (parent != null)
                {
                    if (parent is Page)
                    {
                        return parent as Page;
                    }
                    parent = parent.Parent;
                }
            }
            return null;
        }

        public static string Tag(this Enum source)
        {
            FieldInfo fi = source.GetType().GetRuntimeField(source.ToString());

            TagAttribute[] attributes = (TagAttribute[])fi.GetCustomAttributes(
                typeof(TagAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Value;
            else return source.ToString();
        }
    }
}
