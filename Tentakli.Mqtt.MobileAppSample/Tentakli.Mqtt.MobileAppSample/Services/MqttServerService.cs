using System;
using System.Collections.Generic;
using System.Text;
//using System.Net.Mqtt;
using System.Net;

using MQTTnet;
using MQTTnet.Server;
using System.Threading.Tasks;
using Tentakli.Mqtt.MobileAppSample.Models;

namespace Tentakli.Mqtt.MobileAppSample.Services
{
    public class MqttServerService : IMqttServerSevice
    {
        private IMqttServer _mqttServer = null;
        private IMqttServerOptions _mqttServerOptions = null;
        private MqttServerOptionsBuilder _mqttServerOptionsBuilder = null;

        public int Port { 
            get { 
                return _mqttServerOptions.DefaultEndpointOptions.Port; 
            } 
            set {
                _mqttServerOptions.DefaultEndpointOptions.Port = value;
            }
        }

        public MqttServerService()
        {
            _mqttServerOptionsBuilder = new MqttServerOptionsBuilder()
                // Add some default options
                ;
            _mqttServerOptions = _mqttServerOptionsBuilder.Build();
        }

        public async Task StartAsync()
        {
            if (_mqttServer != null) 
                return;

            _mqttServerOptions = _mqttServerOptionsBuilder.Build();

            _mqttServer = new MqttFactory().CreateMqttServer();
            _mqttServer.ClientConnectedHandler = new MqttServerClientConnectedHandler();
            _mqttServer.ClientDisconnectedHandler = new MqttServerClientDisconnectedHandler();

            await _mqttServer.StartAsync(_mqttServerOptions);
        }

        public async Task StopAsync()
        {
            if (_mqttServer == null)
                return;

            await _mqttServer.StopAsync();
            _mqttServer.ClientConnectedHandler = null;
            _mqttServer.ClientDisconnectedHandler = null;
            _mqttServer = null;
        }

        public ServerSettingsModel GetSettings()
        {
            return new ServerSettingsModel() {
                Port = (ushort) _mqttServerOptions.DefaultEndpointOptions.Port,
                Timeout = (ushort) _mqttServerOptions.DefaultCommunicationTimeout.TotalSeconds
            };
        }

        public void SetSettings(ServerSettingsModel settings)
        {
            _mqttServerOptionsBuilder
                .WithDefaultCommunicationTimeout(TimeSpan.FromSeconds(settings.Timeout))
                .WithDefaultEndpointPort(settings.Port);
        }

        internal class MqttServerClientConnectedHandler : IMqttServerClientConnectedHandler
        {
            public Task HandleClientConnectedAsync(MqttServerClientConnectedEventArgs eventArgs)
            {
                Console.WriteLine($"Client connected: { eventArgs.ClientId }");
                return Task.CompletedTask;
            }
        }

        internal class MqttServerClientDisconnectedHandler : IMqttServerClientDisconnectedHandler
        {
            public Task HandleClientDisconnectedAsync(MqttServerClientDisconnectedEventArgs eventArgs)
            {
                Console.WriteLine($"Client disconnected: { eventArgs.ClientId }");
                return Task.CompletedTask;
            }
        }

        
    }
}
