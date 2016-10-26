using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamstrapSample
{
    public partial class Welcome : ContentPage
    {
        public Welcome()
        {
            InitializeComponent();
            btnViewSamples.Clicked += BtnViewSamples_Clicked;
        }

        private async void BtnViewSamples_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListOfSamples());
        }
    }
}
