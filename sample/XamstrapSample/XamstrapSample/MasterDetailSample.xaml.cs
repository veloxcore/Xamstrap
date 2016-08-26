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
        }

        private void BtnPress_Clicked(object sender, EventArgs e)
        {
            masterDetail.IsDetailVisible = true;
        }
    }
}
