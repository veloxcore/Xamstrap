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
            btnWidthChange.Clicked += BtnWidthChange_Clicked;
            btnToggleMaster.Clicked += BtnToggleMaster_Clicked;
        }

        private void BtnToggleMaster_Clicked(object sender, EventArgs e)
        {
            if (masterDetail.IsMasterVisible)
                masterDetail.IsMasterVisible = false;
            else
                masterDetail.IsMasterVisible = true;
        }

        private void BtnWidthChange_Clicked(object sender, EventArgs e)
        {
            if (masterDetail.ShowDetailHeader)
                masterDetail.ShowDetailHeader = false;
            else
                masterDetail.ShowDetailHeader = true;
        }

        private void BtnPage_Clicked(object sender, EventArgs e)
        {
            masterDetail.IsMasterVisible = false;
            masterDetail.DetailContent = new GridPage();
        }

        private void BtnPress_Clicked(object sender, EventArgs e)
        {
            masterDetail.IsMasterVisible = false;
            StackLayout sl = new StackLayout();
            sl.Children.Add(new Label() { Text = "New layout added and old removed", HorizontalOptions = LayoutOptions.End, TextColor = Color.Purple });
            masterDetail.DetailContent = sl;
        }
    }
}
