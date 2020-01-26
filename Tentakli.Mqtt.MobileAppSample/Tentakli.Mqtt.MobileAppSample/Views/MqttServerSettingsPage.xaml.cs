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
    public partial class MqttServerSettingsPage : ContentPage
    {
        private MqttServerSettingsViewModel _viewModel;

        public MqttServerSettingsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new MqttServerSettingsViewModel();
        }
    }
}