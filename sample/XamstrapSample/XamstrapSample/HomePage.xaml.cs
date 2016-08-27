using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamstrapSample
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            sampleMasterView.Clicked += SampleMasterView_Clicked;
            sampleGrid.Clicked += SampleGrid_Clicked;
            sampleFormGroup.Clicked += SampleFormGroup_Clicked;
            sampleHorizontal.Clicked += SampleHorizontal_Clicked;
            sampleInlineForm.Clicked += SampleInlineForm_Clicked;
            sampleResponsiveUtil.Clicked += SampleResponsiveUtil_Clicked;
            sampleInputGroup.Clicked += SampleInputGroup_Clicked;
            sampleButtonGroup.Clicked += SampleButtonGroup_Clicked;
            sampleTextHelper.Clicked += SampleTextHelper_Clicked;
            sampleButtonHelper.Clicked += SampleButtonHelper_Clicked;
            sampleBackgroundHelper.Clicked += SampleBackgroundHelper_Clicked;
            sampleTabPage.Clicked += SampleTabPage_Clicked;
        }

        private void SampleTabPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TabPageSample());
        }
        private void SampleMasterView_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MasterDetailSample());
        }
        private void SampleBackgroundHelper_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BackgroundHelperSample());
        }

        private void SampleButtonHelper_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ButtonHelperSample());
        }

        private void SampleInputGroup_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InputGroupSample());
        }       

        private void SampleTextHelper_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TextHelperSample());
        }

        private void SampleButtonGroup_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ButtonGroupSample());
        }

        private void SampleInlineForm_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FormInlineSample());
        }

        private void SampleResponsiveUtil_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ResponsiveUtils());
        }

        private void SampleHorizontal_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FormHorizontalSample());
        }

        private void SampleFormGroup_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FormBasicSample());
        }

        private void SampleGrid_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GridPage());
        }
    }
}
