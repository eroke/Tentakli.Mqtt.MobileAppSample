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
using Tentakli.Mqtt.MobileAppSample.Models;

namespace Tentakli.Mqtt.MobileAppSample.Services
{
    public class MqttClientService : IMqttClientService
    {
        private Dictionary<string, Action<string>> _topicCallbacks = new Dictionary<string, Action<string>>();
        private IMqttClient _mqttClient = null;
        private IMqttClientOptions _mqttClientOptions = null;
        private MqttClientOptionsBuilder _mqttClientOptionsBuilder = null;
        private MqttClientSubscribeOptions _mqttClientSubscribeOptions = null;
        private MqttClientUnsubscribeOptions _mqttClientUnsubscribeOptions = null;

        public bool IsConnected { 
            get { 
                return _mqttClient != null && _mqttClient.IsConnected; 
            } 
        }

        public MqttClientService() {
            _mqttClientOptionsBuilder = new MqttClientOptionsBuilder()
                // Add client default options
                .WithClientId(Guid.NewGuid().ToString())
                .WithTcpServer("127.0.0.1", 1883)
                .WithKeepAlivePeriod(TimeSpan.FromDays(1))
                .WithKeepAliveSendInterval(TimeSpan.FromSeconds(15))

                ;
            _mqttClientOptions = _mqttClientOptionsBuilder.Build();
            _mqttClientSubscribeOptions = new MqttClientSubscribeOptionsBuilder()
                // Add client subscribe default options
                .Build();

            _mqttClientUnsubscribeOptions = new MqttClientUnsubscribeOptions();
        }

        public async Task Connect() {
            if (_mqttClient != null)
                return;

            _mqttClient = new MqttFactory().CreateMqttClient();

            _mqttClient.ApplicationMessageReceivedHandler = new ApplicationMessageReceiverHandler(_topicCallbacks);
            _mqttClient.ConnectedHandler = new ConnectedHandler();
            _mqttClient.DisconnectedHandler = new DisconnectedHandler();
            
            _mqttClientOptions = _mqttClientOptionsBuilder.Build();

            await _mqttClient.ConnectAsync(_mqttClientOptions);
        }

        public async Task Disconnect() {
            if (_mqttClient == null)
                return;

            await _mqttClient.DisconnectAsync();
            _mqttClient.ApplicationMessageReceivedHandler = null;
            _mqttClient.ConnectedHandler = null;
            _mqttClient.DisconnectedHandler = null;
            _mqttClient = null;
        }

        public async Task Subscribe(string topic, MqttQualityOfServiceLevel qos, Action<string> callback) {
            if (_mqttClient == null || !_mqttClient.IsConnected || _topicCallbacks.ContainsKey(topic))
                return;

            _topicCallbacks.Add(topic, callback);

            await _mqttClient.SubscribeAsync(topic, qos);
        }

        public async Task Unsubscribe(params string[] topics) {
            if (_mqttClient == null || !_mqttClient.IsConnected)
                return;

            foreach (string topic in topics)
                _topicCallbacks.Remove(topic);

            await _mqttClient.UnsubscribeAsync(topics);
        }

        public ClientSettingsModel GetSettings()
        {
            return new ClientSettingsModel() {
                ClientId = _mqttClientOptions.ClientId,
                Host = ((MqttClientTcpOptions)_mqttClientOptions.ChannelOptions).Server,
                Port = (ushort)((MqttClientTcpOptions)_mqttClientOptions.ChannelOptions).Port,
                Timeout = (ushort)_mqttClientOptions.CommunicationTimeout.TotalSeconds
            };
        }

        public void SetSettings(ClientSettingsModel settings)
        {
            _mqttClientOptionsBuilder
                .WithClientId(settings.ClientId)
                .WithCommunicationTimeout(TimeSpan.FromSeconds(settings.Timeout))
                .WithTcpServer(settings.Host, settings.Port)
                ;
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
                    $"exception - { eventArgs.Exception?.Message };");
                return Task.CompletedTask;
            }
        }

        internal class ApplicationMessageReceiverHandler : IMqttApplicationMessageReceivedHandler
        {
            private Dictionary<string, Action<string>> _topicCallbacks;

            internal ApplicationMessageReceiverHandler(Dictionary<string, Action<string>> topicCallbacks)
            {
                if (topicCallbacks == null)
                    throw new ArgumentNullException(nameof(topicCallbacks), "Argument can not be null");
                _topicCallbacks = topicCallbacks;
            }
            public Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
            {
                string topic = eventArgs.ApplicationMessage.Topic;
                if (_topicCallbacks.ContainsKey(topic))
                    _topicCallbacks[topic].Invoke(Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload));
                
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
