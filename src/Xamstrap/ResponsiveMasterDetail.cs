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
        Grid _masterGrid;
        Grid _detailGrid;
        Button _buttonBack;
        Grid _overLay;
        Layout<View> _masterGridHeader;
        BoxView _horizontalLineDetail;
        BoxView _horizontalLine;
        BoxView _verticalLine;
        ViewContainer _container;
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
                    if (item._buttonBack != null)
                        item._buttonBack.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(item._masterGrid.Width - 1, 0, item._buttonBack.Width, item._buttonBack.Height));
                    item._masterGrid.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(0, 0, item._masterGrid.Width, 1));
                    item._overLay.IsVisible = true;
                }
                else
                {
                    if (item._buttonBack != null)
                        item._buttonBack.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(0, 0, item._buttonBack.Width, item._buttonBack.Height));
                    item._masterGrid.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(-item._masterGrid.Width, 0, item._masterGrid.Width, 1));
                    item._overLay.IsVisible = false;
                }
            }
            else if (deviceSize.Equals(Enums.DeviceSize.ExtraSmall))
            {
                if (newVal)
                {
                    if (item._buttonBack != null)
                        item._buttonBack.IsVisible = true;
                    item._masterGrid.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(-item._masterGrid.Width, 0, 1, 1));
                }
                else
                {
                    if (item._buttonBack != null)
                        item._buttonBack.IsVisible = false;
                    item._masterGrid.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(0, 0, 1, 1));
                }
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
                validateValue: null, propertyChanged: OnMasterHeaderChanged);
        public VisualElement MasterHeader
        {
            get { return (VisualElement)GetValue(MasterHeaderProperty); }
            set { SetValue(MasterHeaderProperty, value); }
        }
        public static void OnMasterHeaderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var item = (ResponsiveMasterDetail)bindable;
            if (newValue == oldValue)
                return;

            if (oldValue != null)
                item._detailGrid.Children.Remove(oldValue as View);
            if (newValue != null)
            {
                View view = newValue as View;
                view.SetValue(Grid.RowProperty, 0);
                item._detailGrid.Children.Add(view);
            }
        }

        public static readonly BindableProperty DetailHeaderProperty =
            BindableProperty.Create("DetailHeader", typeof(VisualElement), typeof(ResponsiveMasterDetail),
                defaultValue: default(VisualElement),
                defaultBindingMode: BindingMode.TwoWay,
                validateValue: null, propertyChanged: OnDetailHeaderChanged);
        public VisualElement DetailHeader
        {
            get { return (VisualElement)GetValue(DetailHeaderProperty); }
            set { SetValue(DetailHeaderProperty, value); }
        }
        public static void OnDetailHeaderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var item = (ResponsiveMasterDetail)bindable;
            if (newValue == oldValue)
                return;

            if (oldValue != null)
                item._detailGrid.Children.Remove(oldValue as View);
            if (newValue != null)
            {
                View view = newValue as View;
                view.SetValue(Grid.RowProperty, 0);
                item._detailGrid.Children.Add(view);
            }
        }

        public static readonly BindableProperty DetailContentProperty =
            BindableProperty.Create("DetailContent", typeof(VisualElement), typeof(ResponsiveMasterDetail),
                defaultValue: default(VisualElement),
                defaultBindingMode: BindingMode.TwoWay,
                validateValue: null,
                propertyChanged: OnDetailContentChanged);
        public VisualElement DetailContent
        {
            get { return (VisualElement)GetValue(DetailContentProperty); }
            set { SetValue(DetailContentProperty, value); }
        }
        public static void OnDetailContentChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var item = (ResponsiveMasterDetail)bindable;
            if (newValue == oldValue)
                return;

            if (oldValue != null)
            {
                if (oldValue is Page)
                {
                    item._detailGrid.Children.Remove(item._container);
                }
                else
                    item._detailGrid.Children.Remove(oldValue as View);
            }
            if (newValue != null)
            {
                if (newValue is Page)
                {
                    Page page = newValue as Page;
                    item._container = new ViewContainer();
                    item._container.Content = page;
                    if (item.DetailHeader != null)
                        item._container.SetValue(Grid.RowProperty, 2);
                    item._detailGrid.Children.Add(item._container);
                }
                else
                {
                    View view = newValue as View;
                    if (item.DetailHeader != null)
                        view.SetValue(Grid.RowProperty, 2);
                    item._detailGrid.Children.Add(view);
                }
            }
        }

        public static readonly BindableProperty MasterContentProperty =
            BindableProperty.Create("MasterContent", typeof(VisualElement), typeof(ResponsiveMasterDetail),
                defaultValue: default(VisualElement),
                defaultBindingMode: BindingMode.TwoWay,
                validateValue: null,
                propertyChanged: OnMasterContentChanged);
        public VisualElement MasterContent
        {
            get { return (VisualElement)GetValue(MasterContentProperty); }
            set { SetValue(MasterContentProperty, value); }
        }
        public static void OnMasterContentChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var item = (ResponsiveMasterDetail)bindable;
            if (newValue == oldValue)
                return;

            if (oldValue != null)
                item._detailGrid.Children.Remove(oldValue as View);
            if (newValue != null)
            {
                View view = newValue as View;
                view.SetValue(Grid.RowProperty, 2);
                item._detailGrid.Children.Add(view);
            }
        }
        #endregion

        #region Constructor
        public ResponsiveMasterDetail()
        {
            this.IsClippedToBounds = true;

            _masterGrid = new Grid() { ColumnSpacing = 0, RowSpacing = 0 };
            _detailGrid = new Grid() { ColumnSpacing = 0, RowSpacing = 0 };
            _container = new ViewContainer();
            _overLay = new Grid() { Opacity = 0.25, IsVisible = false, BackgroundColor = Color.Gray };
            _overLay.SetValue(AbsoluteLayout.LayoutFlagsProperty, AbsoluteLayoutFlags.All);
            _overLay.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(0, 0, 1, 1));
            var tap = new TapGestureRecognizer();
            tap.Tapped += Tap_Tapped;
            _overLay.GestureRecognizers.Add(tap);

            _masterGrid.RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition {Height = 42 },
                new RowDefinition {Height = 1 },
                new RowDefinition {Height = GridLength.Star }
            };

            _detailGrid.RowDefinitions = new RowDefinitionCollection();
            //{
            //    new RowDefinition {Height = 42 },
            //    new RowDefinition {Height = 1 },
            //    new RowDefinition {Height = GridLength.Star }
            //};
            this.Children.Add(_detailGrid);
            this.Children.Add(_overLay);
            this.Children.Add(_masterGrid);

            _horizontalLine = new BoxView();
            _horizontalLine.HeightRequest = 1;
            _horizontalLine.HorizontalOptions = LayoutOptions.FillAndExpand;
            _horizontalLine.BackgroundColor = Color.Black;
            Grid.SetRow(_horizontalLine, 1);
            _masterGrid.Children.Add(_horizontalLine);

            _horizontalLineDetail = new BoxView();
            _horizontalLineDetail.HeightRequest = 1;
            _horizontalLineDetail.HorizontalOptions = LayoutOptions.FillAndExpand;
            _horizontalLineDetail.BackgroundColor = Color.Black;
            //Grid.SetRow(_horizontalLineDetail, 1);
            //_detailGrid.Children.Add(_horizontalLineDetail);

            _verticalLine = new BoxView();
            _verticalLine.WidthRequest = 1;
            _verticalLine.HorizontalOptions = LayoutOptions.End;
            _verticalLine.VerticalOptions = LayoutOptions.FillAndExpand;
            _verticalLine.BackgroundColor = Color.Black;
            _verticalLine.SetValue(Grid.RowSpanProperty, 3);
            _masterGrid.Children.Add(_verticalLine);

            SizeChanged += ResponsiveMasterDetail_SizeChanged;
        }

        private void Tap_Tapped(object sender, EventArgs e)
        {
            IsDetailVisible = true;
        }

        #endregion


        #region Events
        private void ResponsiveMasterDetail_SizeChanged(object sender, EventArgs e)
        {
            if (MasterHeader == null)
            {
                _masterGridHeader = new Grid() { ColumnSpacing = 0, RowSpacing = 0, BackgroundColor = Color.White, Padding = new Thickness(8), Margin = new Thickness(1) };
                _masterGridHeader.Children.Add(new Label { Text = Title, TextColor = Color.Black, HorizontalOptions = LayoutOptions.Center, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), VerticalOptions = LayoutOptions.Center });
            }
            else
            {
                _masterGridHeader = MasterHeader as Layout<View>;
                if (_masterGridHeader.BackgroundColor.Equals(Color.Default))
                    _masterGridHeader.BackgroundColor = Color.White;
                if (_masterGridHeader.Margin == new Thickness(0))
                    _masterGridHeader.Margin = new Thickness(1);
            }

            _masterGrid.Children.Remove(_masterGridHeader as View);
            if (_buttonBack != null)
                this.Children.Remove(_buttonBack);
            if (MasterContent != null)
                _masterGrid.Children.Remove(MasterContent as View);
            if (DetailHeader != null)
            {
                _detailGrid.Children.Remove(DetailHeader as View);
                _detailGrid.Children.Remove(_horizontalLineDetail);
            }

            if (DetailContent != null)
            {
                if (DetailContent is Page)
                    _detailGrid.Children.Remove(_container);
                else
                    _detailGrid.Children.Remove(DetailContent as View);
            }

            _detailGrid.RowDefinitions.Clear();
            if (DetailHeader != null)
            {
                _detailGrid.RowDefinitions.Add(new RowDefinition { Height = 42 });
                _detailGrid.RowDefinitions.Add(new RowDefinition { Height = 1 });
                _detailGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });

                Grid.SetRow(_horizontalLineDetail, 1);
                _detailGrid.Children.Add(_horizontalLineDetail);
            }
            //else
            //{
            //    _detailGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            //}

            Enums.DeviceSize deviceSize = Common.GetCurrentDeviceSize();

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
            if (MasterContent != null)
            {
                var view = MasterContent as View;
                Grid.SetRow(view, 2);
                _masterGrid.Children.Add(view);
            }

            if (DetailHeader != null)
            {
                var view = DetailHeader as View;
                Grid.SetRow(view, 0);
                _detailGrid.Children.Add(view);
            }

            Grid.SetRow(_masterGridHeader, 0);
            _masterGrid.Children.Add(_masterGridHeader as View);

            if (DetailContent != null)
            {
                if (DetailContent is View)
                {
                    var view = DetailContent as View;
                    if (DetailHeader != null)
                        Grid.SetRow(view, 2);
                    //else
                    //    Grid.SetRow(view, 0);
                    _detailGrid.Children.Add(view);
                }
                else if (DetailContent is Page)
                {
                    _container.Content = DetailContent as Page;
                    if (DetailHeader != null)
                        _container.SetValue(Grid.RowProperty, 2);
                    //else
                    //    _container.SetValue(Grid.RowProperty, 0);

                    _detailGrid.Children.Add(_container);
                }
            }
        }

        private void LayoutExtraSmall()
        {
            IsDetailVisible = false;
            _overLay.IsVisible = false;
            _masterGrid.SetValue(AbsoluteLayout.LayoutFlagsProperty, AbsoluteLayoutFlags.SizeProportional);
            _masterGrid.BackgroundColor = _masterGridHeader.BackgroundColor;
            _masterGrid.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(0, 0, 1, 1));

            _detailGrid.SetValue(AbsoluteLayout.LayoutFlagsProperty, AbsoluteLayoutFlags.SizeProportional);
            _detailGrid.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(0, 0, 1, 1));

            _buttonBack = new Button();
            if (MasterHeader == null)
                _buttonBack.Text = Title;
            else
                _buttonBack.Text = "Back";
            _buttonBack.IsVisible = false;
            _buttonBack.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(0, 0, 80, 40));
            _buttonBack.BackgroundColor = Color.Transparent;
            _buttonBack.BorderRadius = 0;
            _buttonBack.VerticalOptions = LayoutOptions.Center;
            _buttonBack.Clicked += _buttonSlide_Clicked;
            this.Children.Add(_buttonBack);
        }

        private void LayoutSmall()
        {
            IsDetailVisible = true;
            _masterGrid.SetValue(AbsoluteLayout.LayoutFlagsProperty, AbsoluteLayoutFlags.HeightProportional);
            _masterGrid.BackgroundColor = _masterGridHeader.BackgroundColor;
            if (MasterWidth == -1)
                _masterGrid.WidthRequest = 300;
            else
                _masterGrid.WidthRequest = MasterWidth;

            _masterGrid.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(-_masterGrid.WidthRequest, 0, _masterGrid.WidthRequest, 1));

            _detailGrid.SetValue(AbsoluteLayout.LayoutFlagsProperty, AbsoluteLayoutFlags.All);
            _detailGrid.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(0, 0, 1, 1));

            _buttonBack = new Button();
            _buttonBack.Text = "Menu";
            _buttonBack.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(0, 0, 80, 40));
            _buttonBack.BackgroundColor = _masterGridHeader.BackgroundColor;
            _buttonBack.BorderRadius = 1;
            _buttonBack.VerticalOptions = LayoutOptions.Center;
            _buttonBack.Clicked += _buttonSlide_Clicked;

            this.Children.Add(_buttonBack);
        }

        private void LayoutMedium()
        {
            _overLay.IsVisible = false;
            _masterGrid.SetValue(AbsoluteLayout.LayoutFlagsProperty, AbsoluteLayoutFlags.All);
            _masterGrid.BackgroundColor = _masterGridHeader.BackgroundColor;
            if (MasterWidth == -1)
                _masterGrid.WidthRequest = 300;
            else
                _masterGrid.WidthRequest = MasterWidth;
            double widthProportionality = _masterGrid.WidthRequest / this.Width;
            _masterGrid.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(0, 0, widthProportionality, 1));

            _detailGrid.SetValue(AbsoluteLayout.LayoutFlagsProperty, AbsoluteLayoutFlags.All);
            _detailGrid.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(1, 0, 1 - widthProportionality, 1));
        }
        #endregion
    }

}
