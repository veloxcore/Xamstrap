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
        VisualElement _masterGridHeader;
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

        public static readonly BindableProperty IsMasterVisibleProperty =
            BindableProperty.Create("IsMasterVisible", typeof(bool), typeof(ResponsiveMasterDetail),
                defaultValue: default(bool),
                defaultBindingMode: BindingMode.TwoWay,
                validateValue: null,
                propertyChanged: OnIsMasterVisibleChanged);
        public bool IsMasterVisible
        {
            get { return (bool)GetValue(IsMasterVisibleProperty); }
            set { SetValue(IsMasterVisibleProperty, value); }
        }
        private static void OnIsMasterVisibleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var item = (ResponsiveMasterDetail)bindable;
            if (newValue == oldValue)
                return;

            bool newVal = Convert.ToBoolean(newValue);

            Enums.DeviceSize deviceSize = Common.GetCurrentDeviceSize();
            if (item.CollapsableInMedium && (deviceSize.Equals(Enums.DeviceSize.Medium) || deviceSize.Equals(Enums.DeviceSize.Large) || deviceSize.Equals(Enums.DeviceSize.ExtraLarge)))
            {
                if (newVal)
                {
                    item._masterGrid.WidthRequest = item.MasterWidth;
                }
                else
                {
                    item._masterGrid.WidthRequest = item.CollapsedMediumMasterWidth;
                }
                item.ArrangeMediumContent();
            }
            else if (deviceSize.Equals(Enums.DeviceSize.Small))
            {
                if (newVal)
                {
                    if (item._buttonBack != null)
                        item._buttonBack.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(item._masterGrid.Width, 0, item._buttonBack.Width, item._buttonBack.Height));
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
                if (!newVal)
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
                defaultValue: 300d,
                defaultBindingMode: BindingMode.TwoWay);
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
            if (newValue != null && item.ShowDetailHeader)
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
                    item._container.Content = null;
                else
                    item._detailGrid.Children.Remove(oldValue as View);
            }
            if (newValue != null)
            {
                if (newValue is Page)
                {
                    item._container.IsVisible = true;
                    item._container.Content = newValue as Page;
                    if (item.ShowDetailHeader)
                        item._container.SetValue(Grid.RowProperty, 2);
                    item._detailGrid.Children.Add(item._container);
                }
                else
                {
                    item._container.IsVisible = false;
                    item._container.Content = null;
                    View view = newValue as View;
                    if (item.ShowDetailHeader)
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

        public static readonly BindableProperty ShowDetailHeaderProperty =
            BindableProperty.Create("ShowDetailHeader", typeof(bool), typeof(ResponsiveMasterDetail),
                defaultValue: true,
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: OnShowDetailHeaderChanged);
        public bool ShowDetailHeader
        {
            get { return (bool)GetValue(ShowDetailHeaderProperty); }
            set { SetValue(ShowDetailHeaderProperty, value); }
        }
        public static void OnShowDetailHeaderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var item = (ResponsiveMasterDetail)bindable;
            if (newValue == oldValue)
                return;

            if (Convert.ToBoolean(newValue))
            {
                if (item._detailGrid.RowDefinitions.Count != 3)
                {
                    item._detailGrid.RowDefinitions.Clear();
                    item._detailGrid.RowDefinitions.Add(new RowDefinition { Height = 42 });
                    item._detailGrid.RowDefinitions.Add(new RowDefinition { Height = 1 });
                    item._detailGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
                }

                if (item.DetailHeader != null)
                {
                    View view = item.DetailHeader as View;
                    view.SetValue(Grid.RowProperty, 0);
                    item.DetailHeader.IsVisible = true;
                }

                item._horizontalLineDetail.SetValue(Grid.RowProperty, 1);
                item._horizontalLineDetail.IsVisible = true;
                if (!item._detailGrid.Children.Any(o => o.Equals(item._horizontalLineDetail)))
                    item._detailGrid.Children.Add(item._horizontalLineDetail);

                if (item.DetailContent != null)
                {
                    View view = item.DetailContent as View;
                    view.SetValue(Grid.RowProperty, 2);
                }
            }
            else
            {
                item._detailGrid.RowDefinitions.Clear();

                item._horizontalLineDetail.IsVisible = false;
                item._horizontalLineDetail.SetValue(Grid.RowProperty, 0);

                if (item.DetailHeader != null)
                    item.DetailHeader.IsVisible = false;

                if (item.DetailContent != null)
                {
                    item.DetailContent.SetValue(Grid.RowProperty, 0);
                }
            }
        }

        public static readonly BindableProperty CollapsableInMediumProperty =
             BindableProperty.Create("CollapsableInMedium", typeof(bool), typeof(ResponsiveMasterDetail),
                 defaultValue: default(bool),
                 defaultBindingMode: BindingMode.TwoWay,
                 validateValue: null,
                 propertyChanged: OnCollapsableInMediumChanged);
        public bool CollapsableInMedium
        {
            get { return (bool)GetValue(CollapsableInMediumProperty); }
            set { SetValue(CollapsableInMediumProperty, value); }
        }
        private static void OnCollapsableInMediumChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var item = (ResponsiveMasterDetail)bindable;
            if (newValue == oldValue)
                return;
        }

        public static readonly BindableProperty CollapsedMediumMasterWidthProperty =
            BindableProperty.Create("CollapsedMediumMasterWidth", typeof(double), typeof(ResponsiveMasterDetail),
                defaultValue: 48d,
                defaultBindingMode: BindingMode.TwoWay,
                validateValue: null,
                propertyChanged: OnCollapsedMediumMasterWidthChanged);
        public double CollapsedMediumMasterWidth
        {
            get { return (double)GetValue(CollapsedMediumMasterWidthProperty); }
            set { SetValue(CollapsedMediumMasterWidthProperty, value); }
        }
        private static void OnCollapsedMediumMasterWidthChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var item = (ResponsiveMasterDetail)bindable;
            if (newValue == oldValue)
                return;
        }
        #endregion

        #region Constructor
        public ResponsiveMasterDetail()
        {
            this.IsClippedToBounds = true;
            _horizontalLineDetail = GetHorizontalLine();

            _masterGrid = new Grid() { ColumnSpacing = 0, RowSpacing = 0 };
            _detailGrid = new Grid() { ColumnSpacing = 0, RowSpacing = 0 };

            _container = new ViewContainer();
            _container.IsVisible = false;
            _detailGrid.Children.Add(_container);

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
            _masterGrid.ColumnDefinitions = new ColumnDefinitionCollection
            {
                new ColumnDefinition() {Width = GridLength.Star },
                new ColumnDefinition() { Width = 1 }
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

            _horizontalLine = GetHorizontalLine();
            Grid.SetRow(_horizontalLine, 1);
            Grid.SetColumnSpan(_horizontalLine, 2);
            _masterGrid.Children.Add(_horizontalLine);

            //Grid.SetRow(_horizontalLineDetail, 1);
            //_detailGrid.Children.Add(_horizontalLineDetail);

            _verticalLine = new BoxView();
            _verticalLine.WidthRequest = 1;
            _verticalLine.HorizontalOptions = LayoutOptions.End;
            _verticalLine.VerticalOptions = LayoutOptions.FillAndExpand;
            _verticalLine.BackgroundColor = Color.Black;
            _verticalLine.SetValue(Grid.ColumnProperty, 1);
            _verticalLine.SetValue(Grid.RowSpanProperty, 3);
            _masterGrid.Children.Add(_verticalLine);

            _buttonBack = new Button();
            _buttonBack.Clicked += _buttonSlide_Clicked;
            this.Children.Add(_buttonBack);

            SizeChanged += ResponsiveMasterDetail_SizeChanged;
        }

        private void Tap_Tapped(object sender, EventArgs e)
        {
            IsMasterVisible = false;
        }

        #endregion


        #region Events

        private void ResponsiveMasterDetail_SizeChanged(object sender, EventArgs e)
        {
            this.SizeChanged -= ResponsiveMasterDetail_SizeChanged;
            try
            {
                if (MasterHeader == null)
                {
                    // _masterGridHeader = new Grid() { ColumnSpacing = 0, RowSpacing = 0, BackgroundColor = Color.White, Padding = new Thickness(8), Margin = new Thickness(1) };
                    _masterGridHeader = new Label { Text = Title, TextColor = Color.Black, HorizontalOptions = LayoutOptions.Center, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), VerticalOptions = LayoutOptions.Center };
                }
                else
                {
                    _masterGridHeader = MasterHeader as VisualElement;
                    if (_masterGridHeader.BackgroundColor.Equals(Color.Default))
                        _masterGridHeader.BackgroundColor = Color.White;
                    //if (_masterGridHeader.Margin == new Thickness(0))
                    //    _masterGridHeader.Margin = new Thickness(1);
                }

                //_masterGrid.Children.Remove(_masterGridHeader as View);
                //if (MasterContent != null)
                //    _masterGrid.Children.Remove(MasterContent as View);
                //if (DetailHeader != null)
                //    _detailGrid.Children.Remove(DetailHeader as View);
                //if (DetailContent != null)
                //{
                //    if (DetailContent is Page)
                //        _detailGrid.Children.Remove(_container);
                //    else
                //        _detailGrid.Children.Remove(DetailContent as View);
                //}

                if (ShowDetailHeader)
                {
                    try
                    {
                        if (_detailGrid.RowDefinitions.Count != 3)
                        {
                            _detailGrid.RowDefinitions.Clear();
                            _detailGrid.RowDefinitions.Add(new RowDefinition { Height = 42 });
                            _detailGrid.RowDefinitions.Add(new RowDefinition { Height = 1 });
                            _detailGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
                        }

                        Grid.SetRow(_horizontalLineDetail, 1);
                        _horizontalLineDetail.IsVisible = true;
                        if (!_detailGrid.Children.Any(o => o.Equals(_horizontalLineDetail)))
                            _detailGrid.Children.Add(_horizontalLineDetail);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }

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
            finally
            {
                this.SizeChanged += ResponsiveMasterDetail_SizeChanged;
            }
        }

        private void _buttonSlide_Clicked(object sender, EventArgs e)
        {
            IsMasterVisible = !IsMasterVisible;
        }
        #endregion

        #region Private Methods
        private void LayoutCommon()
        {
            if (MasterContent != null)
            {
                var view = MasterContent as View;
                Grid.SetRow(view, 2);
                if (!_masterGrid.Children.Any(o => o.Equals(MasterContent)))
                    _masterGrid.Children.Add(view);
            }

            if (DetailHeader != null && ShowDetailHeader)
            {
                var view = DetailHeader as View;
                Grid.SetRow(view, 0);
                if (!_detailGrid.Children.Any(o => o.Equals(view)))
                    _detailGrid.Children.Add(view);
            }

            Grid.SetRow(_masterGridHeader, 0);
            if (!_masterGrid.Children.Any(o => o.Equals(_masterGridHeader)))
                _masterGrid.Children.Add(_masterGridHeader as View);

            if (DetailContent != null)
            {
                if (DetailContent is View)
                {
                    var view = DetailContent as View;
                    _container.IsVisible = false;
                    _container.Content = null;
                    if (ShowDetailHeader)
                        Grid.SetRow(view, 2);
                    if (!_detailGrid.Children.Any(o => o.Equals(view)))
                        _detailGrid.Children.Add(view);
                }
                else if (DetailContent is Page)
                {
                    _container.Content = DetailContent as Page;
                    if (ShowDetailHeader)
                        _container.SetValue(Grid.RowProperty, 2);
                }
            }
        }

        private void LayoutExtraSmall()
        {
            IsMasterVisible = true;
            _overLay.IsVisible = false;
            _masterGrid.SetValue(AbsoluteLayout.LayoutFlagsProperty, AbsoluteLayoutFlags.SizeProportional);
            _masterGrid.BackgroundColor = _masterGridHeader.BackgroundColor;
            _masterGrid.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(0, 0, 1, 1));

            _detailGrid.SetValue(AbsoluteLayout.LayoutFlagsProperty, AbsoluteLayoutFlags.SizeProportional);
            _detailGrid.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(0, 0, 1, 1));

            if (MasterHeader == null)
                _buttonBack.Text = Title;
            else
                _buttonBack.Text = "Back";
            _buttonBack.IsVisible = false;
            _buttonBack.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(0, 0, 80, 40));
            _buttonBack.BackgroundColor = Color.Transparent;
            _buttonBack.BorderRadius = 0;
            _buttonBack.VerticalOptions = LayoutOptions.Center;
            _verticalLine.IsVisible = false;
        }

        private void LayoutSmall()
        {
            IsMasterVisible = false;
            _masterGrid.SetValue(AbsoluteLayout.LayoutFlagsProperty, AbsoluteLayoutFlags.HeightProportional);
            _masterGrid.BackgroundColor = _masterGridHeader.BackgroundColor;
            _masterGrid.WidthRequest = MasterWidth;

            _masterGrid.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(-_masterGrid.WidthRequest, 0, _masterGrid.WidthRequest, 1));

            _detailGrid.SetValue(AbsoluteLayout.LayoutFlagsProperty, AbsoluteLayoutFlags.All);
            _detailGrid.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(0, 0, 1, 1));

            _buttonBack.Image = "menuPrimary.png";
            _buttonBack.IsVisible = true;
            _buttonBack.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(0, 0, 40, 42));
            _buttonBack.BackgroundColor = _masterGridHeader.BackgroundColor;
            _buttonBack.BorderRadius = 0;
            _buttonBack.VerticalOptions = LayoutOptions.Center;
            _verticalLine.IsVisible = true;
        }

        private void LayoutMedium()
        {
            IsMasterVisible = true;
            _overLay.IsVisible = false;
            _buttonBack.IsVisible = false;
            _masterGrid.SetValue(AbsoluteLayout.LayoutFlagsProperty, AbsoluteLayoutFlags.All);
            _detailGrid.SetValue(AbsoluteLayout.LayoutFlagsProperty, AbsoluteLayoutFlags.All);
            _masterGrid.BackgroundColor = _masterGridHeader.BackgroundColor;
            if (CollapsableInMedium && !IsMasterVisible)
                _masterGrid.WidthRequest = CollapsedMediumMasterWidth;
            else
                _masterGrid.WidthRequest = MasterWidth;

            ArrangeMediumContent();
            _verticalLine.IsVisible = true;
        }

        private void ArrangeMediumContent()
        {
            double widthProportionality = _masterGrid.WidthRequest / this.Width;
            _masterGrid.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(0, 0, widthProportionality, 1));

            _detailGrid.SetValue(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(1, 0, 1 - widthProportionality, 1));
        }

        private BoxView GetHorizontalLine()
        {
            return new BoxView() { HeightRequest = 1, BackgroundColor = Color.Black, HorizontalOptions = LayoutOptions.FillAndExpand };
        }
        #endregion
    }

}
