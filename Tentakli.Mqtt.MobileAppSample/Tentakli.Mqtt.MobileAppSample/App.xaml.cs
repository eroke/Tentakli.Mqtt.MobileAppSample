using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tentakli.Mqtt.MobileAppSample.Services;
using Tentakli.Mqtt.MobileAppSample.Views;

namespace Tentakli.Mqtt.MobileAppSample
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MqttServerService>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
