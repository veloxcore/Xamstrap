using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamstrapSample
{
    public partial class ListOfSamples : ContentPage
    {
        public ListOfSamples()
        {
            InitializeComponent();
            sampleGrid.Clicked += SampleGrid_Clicked;
            sampleButton.Clicked += SampleButton_Clicked;
            sampleText.Clicked += SampleText_Clicked;
            sampleForm.Clicked += SampleForm_Clicked;
            sampleTab.Clicked += SampleTabPage_Clicked;
            sampleVisiblity.Clicked += SampleVisiblity_Clicked;
            sampleMasterDetail.Clicked += SampleMasterView_Clicked;

        }
        private void SampleMasterView_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MasterDetailSample());
        }

        private void SampleVisiblity_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VisiblitySample());
        }

        private void SampleTabPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TabSample());
        }

        private void SampleForm_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FormsSample());
        }

        private void SampleText_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TextSample());
        }

        private void SampleButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ButtonSample());
        }

        private void SampleGrid_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GridSample());
        }
    }
}
