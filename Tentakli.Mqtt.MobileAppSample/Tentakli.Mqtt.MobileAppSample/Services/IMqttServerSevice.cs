using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tentakli.Mqtt.MobileAppSample.Services
{
    public interface IMqttServerSevice
    {
        Task StartAsync();
        Task StopAsync();
    }
}
