using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MQTTnet.Protocol;

namespace Tentakli.Mqtt.MobileAppSample.Services
{
    public interface IMqttClientService
    {
        Task Connect(string host);
        Task Disconnect();
        Task Subscribe(string topic, MqttQualityOfServiceLevel qos);

        Task Unsubscribe(params string[] topics);
    }
}
