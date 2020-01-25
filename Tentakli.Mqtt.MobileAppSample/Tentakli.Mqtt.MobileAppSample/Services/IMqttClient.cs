using System;
using System.Collections.Generic;
using System.Net.Mqtt;
using System.Text;
using System.Threading.Tasks;

namespace Tentakli.Mqtt.MobileAppSample.Services
{
    public interface IMqttClient
    {
        Task Connect(string host);
        Task Disconnect();
        Task Subscribe(string topic, Action<MqttApplicationMessage> onNext);

        Task Unsubscribe(params string[] topics);
    }
}
