using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tentakli.Mqtt.MobileAppSample.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tentakli.Mqtt.MobileAppSample.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(true)]
    public partial class MqttClientSettingsPage : ContentPage
    {
        private MqttClientSettingsViewModel _viewModel;

        public MqttClientSettingsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new MqttClientSettingsViewModel();
        }
    }
}