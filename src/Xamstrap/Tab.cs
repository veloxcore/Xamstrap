using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamstrap.AttachedProperties;

namespace Xamstrap
{
    [ContentProperty("TabItems")]
    public class Tab : Grid
    {
        #region Private Members
        private StackLayout _tabHeaderStack;
        private Frame _tabContent;
        private TabItem _lastSelectedItem = null;
        private Grid _tabHeaderHolder;
        private RelativeLayout _selectedTabIndicatorHolder;
        private BoxView _selectedTabIndicator;
        private Xamarin.Forms.ScrollView _tabScrolling;
        private BoxView _seperator;
        #endregion

        #region Bindable Properties

        #region TabItems
        public static readonly BindableProperty TabItemsProperty =
            BindableProperty.Create("TabItems", typeof(ObservableCollection<TabItem>), typeof(Tab),
                defaultValue: new ObservableCollection<TabItem>(),
                defaultBindingMode: BindingMode.TwoWay,
                validateValue: null
                );
        public ObservableCollection<TabItem> TabItems
        {
            get { return (ObservableCollection<TabItem>)GetValue(TabItemsProperty); }
            set { SetValue(TabItemsProperty, value); }
        }
        #endregion

        #region HeaderColor
        public static readonly BindableProperty HeaderColorProperty =
             BindableProperty.Create("HeaderColor", typeof(Color), typeof(Tab),
                 defaultValue: Color.FromHex("#337ab7"),
                 defaultBindingMode: BindingMode.TwoWay,
                 validateValue: null,
                 propertyChanged: OnHeaderColorChanged);
        public Color TabHeaderTextColor
        {
            get { return (Color)GetValue(HeaderColorProperty); }
            set { SetValue(HeaderColorProperty, value); }
        }
        private static void OnHeaderColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var item = (Tab)bindable;
            if (newValue == oldValue)
                return;
            item._selectedTabIndicator.BackgroundColor = (Color)newValue;
            item._seperator.BackgroundColor = (Color)newValue;
        }
        #endregion

        #region TabHeaderBackgroundColor
        public static readonly BindableProperty TabHeaderBackgroundColorProperty =
            BindableProperty.Create("TabHeaderBackgroundColor", typeof(Color), typeof(Tab),
                defaultValue: default(Color),
                defaultBindingMode: BindingMode.TwoWay,
                validateValue: null,
                propertyChanged: OnTabHeaderBackgroundColorChanged);
        public Color TabHeaderBackgroundColor
        {
            get { return (Color)GetValue(TabHeaderBackgroundColorProperty); }
            set { SetValue(TabHeaderBackgroundColorProperty, value); }
        }
        private static void OnTabHeaderBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var item = (Tab)bindable;
            if (newValue == oldValue)
                return;

            item._tabHeaderHolder.BackgroundColor = (Color)newValue;
            item._tabScrolling.BackgroundColor = (Color)newValue;
        }
        #endregion

        #region IsFixedTab
        public static readonly BindableProperty IsFixedTabProperty =
            BindableProperty.Create("IsFixedTab", typeof(Boolean), typeof(Tab),
                defaultValue: false,
                defaultBindingMode: BindingMode.TwoWay,
                validateValue: null);
        public Boolean IsFixedTab
        {
            get { return (Boolean)GetValue(IsFixedTabProperty); }
            set { SetValue(IsFixedTabProperty, value); }
        }
        #endregion

        #region MinTabHeaderWidth
        public static readonly BindableProperty MinTabHeaderWidthProperty =
            BindableProperty.Create("MinTabHeaderWidth", typeof(double), typeof(Tab),
                defaultValue: Convert.ToDouble(120),
                defaultBindingMode: BindingMode.TwoWay,
                validateValue: null);
        public double MinTabHeaderWidth
        {
            get { return (double)GetValue(MinTabHeaderWidthProperty); }
            set { SetValue(MinTabHeaderWidthProperty, value); }
        }
       
        #endregion

        #region MaxTabHeaderWidth
        public static readonly BindableProperty MaxTabHeaderWidthProperty =
            BindableProperty.Create("MaxTabHeaderWidth", typeof(double), typeof(Tab),
                defaultValue: Convert.ToDouble(176),
                defaultBindingMode: BindingMode.TwoWay,
                validateValue: null);
        public double MaxTabHeaderWidth
        {
            get { return (double)GetValue(MaxTabHeaderWidthProperty); }
            set { SetValue(MaxTabHeaderWidthProperty, value); }
        }
        #endregion

        #endregion
        //................Structure..................................//
        //   Grid                                                          - Main grid containing 2 rows
        //       ScrollView(_tabScrolling)                                 - Contains grid with 1 row.
        //           Grid(_tabHeaderHolder)                                - Used to hold tabHeader and selectedTabhighlighter
        //               Stacklayout(_tabHeaderStack)                      - Contains _tabHeader
        //               RelativeLayout(_selectedTabIndicatorHolder)       - Contains _selectedTabIndicator, will be drawn according to selected tab
        //       Frame(_tabContent)                                        - Hold tab content and change the content according to selected tab
        //................Structure..................................//

        public Tab()
        {
            //Declaring the main grid 
            this.RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition { Height = GridLength.Auto},
                new RowDefinition { Height = GridLength.Star}
            };

            this.RowSpacing = 0;
            this.Padding = 0;

            _tabScrolling = new Xamarin.Forms.ScrollView();
            _tabScrolling.Orientation = ScrollOrientation.Horizontal;
            _tabScrolling.SetValue(Grid.RowProperty, 0);
            _tabScrolling.SetValue(ScrollViewPoperty.HorizontalScrollBarVisibleProperty, false);

            _tabHeaderHolder = new Grid();
            _tabHeaderHolder.RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition { Height = GridLength.Auto},
                new RowDefinition {Height= 3 }
            };
            _tabHeaderHolder.RowSpacing = 0;

            _tabHeaderStack = new StackLayout();
            _tabHeaderStack.Orientation = StackOrientation.Horizontal;
            _tabHeaderStack.Spacing = 0;
            _tabHeaderHolder.Children.Add(_tabHeaderStack);


            _selectedTabIndicator = new BoxView();
            _selectedTabIndicator.HeightRequest = Device.OnPlatform<double>(3, 3, 0);

            _seperator = new BoxView();
            _seperator.HeightRequest = 1;
            _seperator.VerticalOptions = LayoutOptions.End;
            _seperator.SetValue(Grid.RowProperty, 1);
            _tabHeaderHolder.Children.Add(_seperator);

            _selectedTabIndicatorHolder = new RelativeLayout();
            _selectedTabIndicatorHolder.BackgroundColor = Color.Transparent;
            _selectedTabIndicatorHolder.SetValue(Grid.RowProperty, 1);
            _selectedTabIndicatorHolder.VerticalOptions = LayoutOptions.End;
            _selectedTabIndicatorHolder.Children.Add(_selectedTabIndicator, xConstraint: null);
            //Adding _selectedTabHolder(RelativeLayout) as a child to _tabHeaderHolder(Grid)
            _tabHeaderHolder.Children.Add(_selectedTabIndicatorHolder);

            //Adding _tabHeaderHolder(Grid) as a content to _tabScrolling(ScrollView)
            _tabScrolling.Content = _tabHeaderHolder;
            this.Children.Add(_tabScrolling);



            //Generating frame to hold tab item content
            _tabContent = new Frame();
            _tabContent.Padding = 0;
            _tabContent.OutlineColor = Color.Transparent;
            _tabContent.HasShadow = false;
            _tabContent.SetValue(Grid.RowProperty, 1);
            _tabContent.HorizontalOptions = LayoutOptions.FillAndExpand;
            _tabContent.VerticalOptions = LayoutOptions.FillAndExpand;
            //Adding tabContent to main grid row 1
            this.Children.Add(_tabContent);

            this.TabItems.CollectionChanged += (s, e) =>
            {
                // TODO: Processing assumes first element in NewItem is always new, no code to process removal of child.

                var tabItem = e.NewItems[0] as TabItem;
                if (tabItem == null)
                    return;

                var tabItemButton = new Button();
                tabItemButton.Margin = 0;
                tabItemButton.BackgroundColor = Color.Transparent;
                tabItemButton.BorderRadius = 1;
                tabItemButton.SetValue(ButtonProperty.PaddingProperty,new Thickness(6,5));
                //tabItemButton.SetValue(ResponsiveProperty.ClassProperty, "btn-sm");
                tabItemButton.Text = (e.NewItems[0] as TabItem).Title.ToUpper();
                tabItemButton.TextColor = TabHeaderTextColor;
                tabItemButton.Clicked += (sender, args) =>
                {
                    //Handling Clicked event for the tab item
                    if (_lastSelectedItem != tabItem)
                    {
                        foreach (var item in TabItems)
                        {
                            item.IsSelected = false;
                        }
                        tabItem.IsSelected = true;
                        this._lastSelectedItem = tabItem;

                        //Draw seleted tab highlighter
                        DrawSelectedTab(sender, tabItem);
                    }
                };
                _tabHeaderStack.Children.Add(tabItemButton);
            };

            this.SizeChanged += (sender, args) =>
            {
                if (!this.TabItems.Any(o => o.IsSelected))
                {
                    if (_lastSelectedItem != TabItems[0])
                        TabItems[0].IsSelected = true;
                }

                for (int i = 0; i < TabItems.Count; i++)
                {
                    if (TabItems[i].IsSelected)
                    {
                        DrawSelectedTab(_tabHeaderStack.Children[i], TabItems[i]);
                        _lastSelectedItem = TabItems[i];
                        break;
                    }
                }
            };
        }

        protected override void OnSizeAllocated(double w, double h)
        {
            if (this.Height <= 0)
                return;

            // Process FixedTab
            double width = MinTabHeaderWidth;
            if (this.IsFixedTab)
            {
                var totalWidth = _tabHeaderStack.Children.Sum(o => o.Width);
                width = this.Width / _tabHeaderStack.Children.Count;

                foreach (Button btn in _tabHeaderStack.Children)
                {
                    btn.WidthRequest = width;
                }
            }
            else
            {
                var largestPossibleBtn = _tabHeaderStack.Children.OrderBy(o => o.WidthRequest);
                double maxAllowedBtnWidth = 0;
                if (largestPossibleBtn.Any())
                    maxAllowedBtnWidth = largestPossibleBtn.First().Width;

                width = maxAllowedBtnWidth > MaxTabHeaderWidth ?
                                        MaxTabHeaderWidth : maxAllowedBtnWidth < MinTabHeaderWidth ?
                                                    MinTabHeaderWidth : maxAllowedBtnWidth;

                foreach (Button btn in _tabHeaderStack.Children)
                {
                    btn.WidthRequest = width;
                }
            }

            base.OnSizeAllocated(w, h);
        }

        /// <summary>
        /// Draws the selected tab.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="tabItem">The tab item.</param>      
        private async void DrawSelectedTab(object sender, TabItem tabItem)
        {
            var selectElement = sender as VisualElement;
            await _selectedTabIndicator.LayoutTo(new Rectangle(selectElement.X, _selectedTabIndicator.Y, selectElement.WidthRequest, _selectedTabIndicator.HeightRequest));
            _tabContent.Content = tabItem.Content;
        }
    }
}
