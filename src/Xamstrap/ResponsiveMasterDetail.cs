using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamstrap
{
    public class ResponsiveMasterDetail : AbsoluteLayout
    {
        #region Properties
        private Grid _masterGrid;
        private Grid _detailGrid;
        private Button _buttonBack;
        private Layout<View> _masterHeader;
        private BoxView _horizontalLineDetail;
        private BoxView _horizontalLine;
        #endregion

        #region Bindable Properties
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create("Title", typeof(string), typeof(ResponsiveMasterDetail),
                defaultValue: "Title",
                defaultBindingMode: BindingMode.TwoWay,
                validateValue: null);
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly BindableProperty IsDetailVisibleProperty =
            BindableProperty.Create("IsDetailVisible", typeof(bool), typeof(ResponsiveMasterDetail),
                defaultValue: default(bool),
                defaultBindingMode: BindingMode.TwoWay,
                validateValue: null,
                propertyChanged: OnIsDetailVisibleChanged);
        public bool IsDetailVisible
        {
            get { return (bool)GetValue(IsDetailVisibleProperty); }
            set { SetValue(IsDetailVisibleProperty, value); }
        }
        private static void OnIsDetailVisibleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var item = (ResponsiveMasterDetail)bindable;
            if (newValue == oldValue)
                return;
            bool newVal = Convert.ToBoolean(newValue);

            Enums.DeviceSize deviceSize = Common.GetCurrentDeviceSize();
            if (deviceSize.Equals(Enums.DeviceSize.Small))
            {
                if (!newVal)
                {
                    item._buttonBack.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(item._masterGrid.Width, 0, item._buttonBack.Width, item._buttonBack.Height));
                    item._masterGrid.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(0, 0, item._masterGrid.Width, 1));
                }
                else
                {
                    item._buttonBack.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(0, 0, item._buttonBack.Width, item._buttonBack.Height));
                    item._masterGrid.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(-item._masterGrid.Width, 0, item._masterGrid.Width, 1));
                }
            }
            else if (deviceSize.Equals(Enums.DeviceSize.ExtraSmall))
            {
                if (newVal)
                    item._masterGrid.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(-item._masterGrid.Width, 0, 1, 1));
                else
                    item._masterGrid.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(0, 0, 1, 1));
            }
        }

        public static readonly BindableProperty MasterWidthProperty =
            BindableProperty.Create("MasterWidth", typeof(double), typeof(ResponsiveMasterDetail),
                defaultValue: -1d,
                defaultBindingMode: BindingMode.TwoWay,
                validateValue: null);
        public double MasterWidth
        {
            get { return (double)GetValue(MasterWidthProperty); }
            set { SetValue(MasterWidthProperty, value); }
        }

        public static readonly BindableProperty MasterHeaderProperty =
            BindableProperty.Create("MasterHeader", typeof(VisualElement), typeof(ResponsiveMasterDetail),
                defaultValue: default(VisualElement),
                defaultBindingMode: BindingMode.TwoWay,
                validateValue: null);
        public VisualElement MasterHeader
        {
            get { return (VisualElement)GetValue(MasterHeaderProperty); }
            set { SetValue(MasterHeaderProperty, value); }
        }

        public static readonly BindableProperty DetailHeaderProperty =
            BindableProperty.Create("DetailHeader", typeof(VisualElement), typeof(ResponsiveMasterDetail),
                defaultValue: default(VisualElement),
                defaultBindingMode: BindingMode.TwoWay,
                validateValue: null);
        public VisualElement DetailHeader
        {
            get { return (VisualElement)GetValue(DetailHeaderProperty); }
            set { SetValue(DetailHeaderProperty, value); }
        }

        public static readonly BindableProperty DetailContentProperty =
            BindableProperty.Create("DetailContent", typeof(VisualElement), typeof(ResponsiveMasterDetail),
                defaultValue: default(VisualElement),
                defaultBindingMode: BindingMode.TwoWay,
                validateValue: null);
        public VisualElement DetailContent
        {
            get { return (VisualElement)GetValue(DetailContentProperty); }
            set { SetValue(DetailContentProperty, value); }
        }

        public static readonly BindableProperty MasterContentProperty =
            BindableProperty.Create("MasterContent", typeof(VisualElement), typeof(ResponsiveMasterDetail),
                defaultValue: default(VisualElement),
                defaultBindingMode: BindingMode.TwoWay,
                validateValue: null);
        public VisualElement MasterContent
        {
            get { return (VisualElement)GetValue(MasterContentProperty); }
            set { SetValue(MasterContentProperty, value); }
        }
        #endregion

        #region Constructor
        public ResponsiveMasterDetail()
        {
            this.IsClippedToBounds = true;

            _horizontalLine = new BoxView();
            _horizontalLine.HeightRequest = 1;
            _horizontalLine.HorizontalOptions = LayoutOptions.FillAndExpand;
            _horizontalLine.BackgroundColor = Color.Black;

            _horizontalLineDetail = new BoxView();
            _horizontalLineDetail.HeightRequest = 1;
            _horizontalLineDetail.HorizontalOptions = LayoutOptions.FillAndExpand;
            _horizontalLineDetail.BackgroundColor = Color.Black;

            SizeChanged += ResponsiveMasterDetail_SizeChanged;
        }

        #endregion


        #region Events
        private void ResponsiveMasterDetail_SizeChanged(object sender, EventArgs e)
        {
            Enums.DeviceSize deviceSize = Common.GetCurrentDeviceSize();
            _masterGrid = new Grid() { ColumnSpacing = 0, RowSpacing = 0 };
            _detailGrid = new Grid() { ColumnSpacing = 0, RowSpacing = 0 };
            _buttonBack = new Button();

            if (MasterHeader == null)
            {
                _masterHeader = new Grid();
                _masterHeader.Padding = new Thickness(8);
                _masterHeader.Children.Add(new Label { Text = Title, HorizontalOptions = LayoutOptions.Center, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), VerticalOptions = LayoutOptions.Center });
                _masterHeader.BackgroundColor = Color.Silver;
            }
            else
            {
                _masterHeader = MasterHeader as Layout<View>;
                if (_masterHeader.BackgroundColor.Equals(Color.Default))
                    _masterHeader.BackgroundColor = Color.Silver;
            }

            if (deviceSize.Equals(Enums.DeviceSize.Medium) || deviceSize.Equals(Enums.DeviceSize.Large) || deviceSize.Equals(Enums.DeviceSize.ExtraLarge))
            {
                LayoutMedium();
            }
            else if (deviceSize.Equals(Enums.DeviceSize.Small))
            {
                LayoutSmall();
            }
            else if (deviceSize.Equals(Enums.DeviceSize.ExtraSmall))
            {
                LayoutExtraSmall();
            }

            LayoutCommon();
        }

        private void _buttonSlide_Clicked(object sender, EventArgs e)
        {
            if (IsDetailVisible)
                IsDetailVisible = false;
            else
                IsDetailVisible = true;
        }
        #endregion

        #region Private Methods
        private void LayoutCommon()
        {
            _masterGrid.RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition {Height = 42 },
                new RowDefinition {Height = 1 },
                new RowDefinition {Height = GridLength.Star }
            };

            _detailGrid.RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition {Height = 42 },
                new RowDefinition {Height = 1 },
                new RowDefinition {Height = GridLength.Star }
            };

            Grid.SetRow(_masterHeader, 0);
            Grid.SetRow(_horizontalLine, 1);
            if (MasterContent != null)
            {
                Grid.SetRow(MasterContent as View, 2);
                _masterGrid.Children.Add(MasterContent as View);
            }

            _masterGrid.Children.Add(_masterHeader);
            _masterGrid.Children.Add(_horizontalLine);

            if (DetailHeader != null)
            {
                Grid.SetRow(DetailHeader as View, 0);
                _detailGrid.Children.Add(DetailHeader as View);
            }
            Grid.SetRow(_horizontalLineDetail, 1);
            if (DetailContent != null)
            {
                Grid.SetRow(DetailContent as View, 2);
                _detailGrid.Children.Add(DetailContent as View);
            }

            _detailGrid.Children.Add(_horizontalLineDetail);
        }

        private void LayoutExtraSmall()
        {
            _masterGrid.SetValue(AbsoluteLayout.LayoutFlagsProperty, AbsoluteLayoutFlags.SizeProportional);
            _masterGrid.BackgroundColor = _masterHeader.BackgroundColor;
            _masterGrid.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(0, 0, 1, 1));

            _detailGrid.SetValue(AbsoluteLayout.LayoutFlagsProperty, AbsoluteLayoutFlags.SizeProportional);
            _detailGrid.BackgroundColor = Color.Gray;
            _detailGrid.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(0, 0, 1, 1));

            if (MasterHeader == null)
                _buttonBack.Text = Title;
            else
                _buttonBack.Text = "Back";

            _buttonBack.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(0, 0, 80, 40));
            _buttonBack.BackgroundColor = Color.Transparent;
            _buttonBack.BorderRadius = 0;
            _buttonBack.VerticalOptions = LayoutOptions.Center;
            _buttonBack.Clicked += _buttonSlide_Clicked;
            this.Children.Add(_detailGrid);
            this.Children.Add(_buttonBack);
            this.Children.Add(_masterGrid);
        }

        private void LayoutSmall()
        {
            IsDetailVisible = true;
            _masterGrid.SetValue(AbsoluteLayout.LayoutFlagsProperty, AbsoluteLayoutFlags.HeightProportional);
            _masterGrid.BackgroundColor = _masterHeader.BackgroundColor;
            if (MasterWidth == -1)
                _masterGrid.WidthRequest = 300;
            else
                _masterGrid.WidthRequest = MasterWidth;

            _masterGrid.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(-_masterGrid.WidthRequest, 0, _masterGrid.WidthRequest, 1));

            _detailGrid.SetValue(AbsoluteLayout.LayoutFlagsProperty, AbsoluteLayoutFlags.All);
            _detailGrid.BackgroundColor = Color.Gray;
            _detailGrid.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(0, 0, 1, 1));

            _buttonBack.Text = "Menu";
            _buttonBack.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(0, 0, 80, 40));
            _buttonBack.BackgroundColor = _masterHeader.BackgroundColor;
            _buttonBack.BorderRadius = 1;
            _buttonBack.VerticalOptions = LayoutOptions.Center;
            _buttonBack.Clicked += _buttonSlide_Clicked;
            this.Children.Add(_detailGrid);
            this.Children.Add(_masterGrid);
            this.Children.Add(_buttonBack);
        }

        private void LayoutMedium()
        {
            _masterGrid.SetValue(AbsoluteLayout.LayoutFlagsProperty, AbsoluteLayoutFlags.All);
            _masterGrid.BackgroundColor = _masterHeader.BackgroundColor;
            if (MasterWidth == -1)
                _masterGrid.WidthRequest = 300;
            else
                _masterGrid.WidthRequest = MasterWidth;
            double widthProportionality = _masterGrid.WidthRequest / 1000;
            _masterGrid.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(0, 0, widthProportionality, 1));

            _detailGrid.SetValue(AbsoluteLayout.LayoutFlagsProperty, AbsoluteLayoutFlags.All);
            _detailGrid.BackgroundColor = Color.Gray;
            _detailGrid.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(1, 0, 1 - widthProportionality, 1));

            this.Children.Add(_masterGrid);
            this.Children.Add(_detailGrid);
        }
        #endregion
    }

}
