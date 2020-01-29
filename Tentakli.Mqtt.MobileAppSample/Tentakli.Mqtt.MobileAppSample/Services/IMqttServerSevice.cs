using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tentakli.Mqtt.MobileAppSample.Models;

namespace Tentakli.Mqtt.MobileAppSample.Services
{
    public interface IMqttServerSevice
    {
        bool IsRunning();
        Task StartAsync();
        Task StopAsync();

        ServerSettingsModel GetSettings();

        void SetSettings(ServerSettingsModel settings);
    }
}
