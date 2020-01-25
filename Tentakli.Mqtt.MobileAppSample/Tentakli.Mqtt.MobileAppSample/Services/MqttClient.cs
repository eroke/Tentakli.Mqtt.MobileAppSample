using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mqtt;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace Tentakli.Mqtt.MobileAppSample.Services
{
    public class MqttClient : IMqttClient
    {
        private System.Net.Mqtt.IMqttClient _mqttClient = null;
        private MqttConfiguration _mqttConfiguration = new MqttConfiguration();

        public bool IsConnected { 
            get { 
                return _mqttClient != null && _mqttClient.IsConnected; 
            } 
        }

        public MqttClient() {
            _mqttConfiguration.Port = 1883;
        }

        public async Task Connect(string host) {
            _mqttClient = await System.Net.Mqtt.MqttClient.CreateAsync(host, _mqttConfiguration);
            await _mqttClient.ConnectAsync();
        }

        public async Task Disconnect() {
            await _mqttClient.DisconnectAsync();
        }

        public async Task Subscribe(string topic, Action<MqttApplicationMessage> onNext) {
            if (_mqttClient == null || !_mqttClient.IsConnected)
                return;

            await _mqttClient.SubscribeAsync(topic, MqttQualityOfService.AtMostOnce);
            _mqttClient.MessageStream
                .Where(m => m.Topic == topic)
                .Subscribe(onNext);
        }

        public async Task Unsubscribe(params string[] topics) {
            if (_mqttClient == null || !_mqttClient.IsConnected)
                return;

            await _mqttClient.UnsubscribeAsync(topics);
        }


    }
}
