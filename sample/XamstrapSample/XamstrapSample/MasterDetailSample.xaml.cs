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
        #region Private Members
        private MasterDetailData _sampleData { get; set; }
        #endregion
        public MasterDetailSample()
        {
            InitializeComponent();
            this._sampleData = new MasterDetailData();
            this.Appearing += MasterDetailSample_Appearing;
            //btnPress.Clicked += BtnPress_Clicked;
            //btnPage.Clicked += BtnPage_Clicked;
            //btnWidthChange.Clicked += BtnWidthChange_Clicked;
            //btnToggleMaster.Clicked += BtnToggleMaster_Clicked;
        }

        private void MasterDetailSample_Appearing(object sender, EventArgs e)
        {
            this.BindingContext = _sampleData;            
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
            masterDetail.DetailContent = new GridSample();
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
