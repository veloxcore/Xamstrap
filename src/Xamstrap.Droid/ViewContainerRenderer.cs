using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamstrap;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ViewContainer), typeof(Xamstrap.Droid.ViewContainerRenderer))]
namespace Xamstrap.Droid
{
    public class ViewContainerRenderer : ViewRenderer<ViewContainer, Android.Views.View>
    {
        Page _currentPage;

        protected override void OnElementChanged(ElementChangedEventArgs<ViewContainer> e)
        {
            base.OnElementChanged(e);
            var pageViewContainer = e.NewElement as ViewContainer;
            if (e.NewElement != null)
            {
                ChangePage(e.NewElement.Content);
            }
            else
            {
                ChangePage(null);
            }

        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "Content")
            {
                ChangePage(Element.Content);
            }
        }

        bool _contentNeedsLayout;

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {

            base.OnLayout(changed, l, t, r, b);

            if ((changed || _contentNeedsLayout) && this.Control != null)
            {
                if (_currentPage != null)
                {
                    _currentPage.Layout(new Rectangle(0, 0, Element.Width, Element.Height));
                }
                var msw = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
                var msh = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);
                this.Control.Measure(msw, msh);
                this.Control.Layout(0, 0, r, b);
                _contentNeedsLayout = false;
            }
        }

        private int ConvertPixelsToDp(float pixelValue)
        {
            var dp = (int)((pixelValue) / Resources.DisplayMetrics.Density);
            return dp;
        }


        void ChangePage(Page page)
        {

            //TODO handle current page
            if (page != null)
            {
                var parentPage = Element.GetParentPage();
                page.Parent = parentPage;

                var existingRenderer = page.GetRenderer();
                if (existingRenderer == null)
                {

                    var renderer = Platform.CreateRenderer(page);// RendererFactory.GetRenderer (page);
                    page.SetRenderer(renderer);
                    existingRenderer = page.GetRenderer();

                }
                _contentNeedsLayout = true;
                SetNativeControl(existingRenderer.ViewGroup);
                Invalidate();
                //TODO update the page
                _currentPage = page;
            }
            else
            {
                //TODO - update the page
                _currentPage = null;
            }

            if (_currentPage == null)
            {
                //have to set somethign for android not to get pissy
                var view = new Android.Views.View(this.Context);
                view.SetBackgroundColor(Element.BackgroundColor.ToAndroid());
                SetNativeControl(view);
            }
        }

    }
}