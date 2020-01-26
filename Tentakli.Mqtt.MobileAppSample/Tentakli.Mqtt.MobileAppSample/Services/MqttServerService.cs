using System;
using System.Collections.Generic;
using System.Text;
//using System.Net.Mqtt;
using System.Net;

using MQTTnet;
using MQTTnet.Server;
using System.Threading.Tasks;

namespace Tentakli.Mqtt.MobileAppSample.Services
{
    public class MqttServerService : IMqttServerSevice
    {
        private IMqttServer _mqttServer = null;
        private IMqttServerOptions _mqttServerOptions = new MqttServerOptions();

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
            _mqttServerOptions = new MqttServerOptionsBuilder()
                // Add some default options
                .Build();
        }

        public async Task StartAsync()
        {
            if (_mqttServer != null) 
                return;

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
            _mqttServer = null;
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
