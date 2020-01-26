using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tentakli.Mqtt.MobileAppSample.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tentakli.Mqtt.MobileAppSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrockerSettingsPage : ContentPage
    {
        private BrockerSettingsViewModel _viewModel;

        public BrockerSettingsPage()
        {
            InitializeComponent();
            
            BindingContext = _viewModel = new BrockerSettingsViewModel();
        }

        private void BrockerStartSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
                _viewModel.BrockerStart();
            else
                _viewModel.BrockerStop();
        }
    }
}