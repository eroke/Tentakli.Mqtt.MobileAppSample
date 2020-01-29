using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MQTTnet.Protocol;
using Tentakli.Mqtt.MobileAppSample.Models;

namespace Tentakli.Mqtt.MobileAppSample.Services
{
    public interface IMqttClientService
    {
        Task Connect();
        Task Disconnect();
        Task Subscribe(string topic, MqttQualityOfServiceLevel qos, Action<string> action);

        Task Unsubscribe(params string[] topics);

        ClientSettingsModel GetSettings();
        void SetSettings(ClientSettingsModel settings);
    }
}
