using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Tentakli.Mqtt.MobileAppSample.Services;
using Xamarin.Forms;

namespace Tentakli.Mqtt.MobileAppSample.ViewModels
{
    public class BrockerSettingsViewModel : BaseViewModel
    {


        public BrockerSettingsViewModel()
        {
            Title = "Brocker settings";

        }



        public void BrockerStart()
        {
            MqttBrocker.StartAsync();
        }

        public void BrockerStop()
        {
            MqttBrocker.StopAsync();
        }
    }
}
