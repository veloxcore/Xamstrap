using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamstrapSample
{
    public partial class MasterDetailSample : ContentPage
    {
        public MasterDetailSample()
        {
            InitializeComponent();
            btnPress.Clicked += BtnPress_Clicked;
            btnPage.Clicked += BtnPage_Clicked;
        }

        private void BtnPage_Clicked(object sender, EventArgs e)
        {
            masterDetail.IsDetailVisible = true;
            masterDetail.DetailContent = new GridPage();
        }

        private void BtnPress_Clicked(object sender, EventArgs e)
        {
            masterDetail.IsDetailVisible = true;
            StackLayout sl = new StackLayout();
            sl.Children.Add(new Label() { Text = "New layout added and old removed", HorizontalOptions = LayoutOptions.End, TextColor = Color.Purple });
            masterDetail.DetailContent = sl;
        }
    }
}
