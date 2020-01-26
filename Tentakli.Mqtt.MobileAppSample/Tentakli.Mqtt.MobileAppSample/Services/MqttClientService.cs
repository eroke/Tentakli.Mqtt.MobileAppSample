using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;
using MQTTnet.Client.Subscribing;
using MQTTnet.Client.Unsubscribing;
using MQTTnet.Protocol;

namespace Tentakli.Mqtt.MobileAppSample.Services
{
    public class MqttClientService : IMqttClientService
    {
        private IMqttClient _mqttClient = null;
        private IMqttClientOptions _mqttClientOptions = null;
        private MqttClientSubscribeOptions _mqttClientSubscribeOptions = null;
        private MqttClientUnsubscribeOptions _mqttClientUnsubscribeOptions = null;

        public bool IsConnected { 
            get { 
                return _mqttClient != null && _mqttClient.IsConnected; 
            } 
        }

        public MqttClientService() {
            _mqttClientOptions = new MqttClientOptionsBuilder()
                // Add client default options
                .Build();
            _mqttClientSubscribeOptions = new MqttClientSubscribeOptionsBuilder()
                // Add client subscribe default options
                .Build();

            _mqttClientUnsubscribeOptions = new MqttClientUnsubscribeOptions();
        }

        public async Task Connect(string host) {
            if (_mqttClient != null)
                return;

            _mqttClient = new MqttFactory().CreateMqttClient();
            _mqttClient.ApplicationMessageReceivedHandler = new ApplicationMessageReceiverHandler();
            _mqttClient.ConnectedHandler = new ConnectedHandler();
            _mqttClient.DisconnectedHandler = new DisconnectedHandler();

            await _mqttClient.ConnectAsync(_mqttClientOptions);
        }

        public async Task Disconnect() {
            if (_mqttClient == null)
                return;

            await _mqttClient.DisconnectAsync();
            _mqttClient = null;
        }

        public async Task Subscribe(string topic, MqttQualityOfServiceLevel qos) {
            if (_mqttClient == null || !_mqttClient.IsConnected)
                return;

            await _mqttClient.SubscribeAsync(topic, qos);
        }

        public async Task Unsubscribe(params string[] topics) {
            if (_mqttClient == null || !_mqttClient.IsConnected)
                return;

            await _mqttClient.UnsubscribeAsync(topics);
        }

        internal class ConnectedHandler : IMqttClientConnectedHandler
        {
            public Task HandleConnectedAsync(MqttClientConnectedEventArgs eventArgs)
            {
                Console.WriteLine($"Client connected: authResult - { eventArgs.AuthenticateResult };");
                return Task.CompletedTask;
            }
        }

        internal class DisconnectedHandler : IMqttClientDisconnectedHandler
        {
            public Task HandleDisconnectedAsync(MqttClientDisconnectedEventArgs eventArgs)
            {
                Console.WriteLine(
                    $"Client disconnected: " +
                    $"authResult - { eventArgs.AuthenticateResult }; " +
                    $"clientWasConnected - { eventArgs.ClientWasConnected }; " +
                    $"exception - { eventArgs.Exception.Message };");
                return Task.CompletedTask;
            }
        }

        internal class ApplicationMessageReceiverHandler : IMqttApplicationMessageReceivedHandler
        {
            public Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
            {
                Console.WriteLine(
                    $"Message received: " +
                    $"processingFailed - { eventArgs.ProcessingFailed }; " +
                    $"clientId - { eventArgs.ClientId }; " +
                    $"topic - { eventArgs.ApplicationMessage.Topic }; " +
                    $"message - { Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload) }; " +
                    $"qos - { eventArgs.ApplicationMessage.QualityOfServiceLevel }; " +
                    $"retain - { eventArgs.ApplicationMessage.Retain };"
                    );

                return Task.CompletedTask;
            }
        }
    }
}
