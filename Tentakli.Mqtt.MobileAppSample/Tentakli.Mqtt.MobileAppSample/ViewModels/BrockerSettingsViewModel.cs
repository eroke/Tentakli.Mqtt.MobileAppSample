using System;
using System.Collections.Generic;
using System.Text;

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
            MqttBrocker.Start();
        }

        public void BrockerStop()
        {
            MqttBrocker.Stop();
        }
    }
}
