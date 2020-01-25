using System;
using System.Collections.Generic;
using System.Text;

namespace Tentakli.Mqtt.MobileAppSample.Services
{
    public interface IMqttBrocker
    {
        void Start();
        void Stop();
    }
}
