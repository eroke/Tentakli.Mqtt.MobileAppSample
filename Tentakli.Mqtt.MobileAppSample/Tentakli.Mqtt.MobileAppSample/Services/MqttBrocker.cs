using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mqtt;
using System.Net;

namespace Tentakli.Mqtt.MobileAppSample.Services
{
    public class MqttBrocker : IMqttBrocker
    {
        private IMqttServer _mqttServer = null;
        private MqttConfiguration _mqttConfiguration = new MqttConfiguration();

        public int Port { 
            get { 
                return _mqttConfiguration.Port; 
            } 
            set {
                _mqttConfiguration.Port = value;
            }
        }

        public MqttBrocker()
        {
            _mqttConfiguration.Port = 1883;
            _mqttConfiguration.KeepAliveSecs = 120;
            _mqttConfiguration.WaitTimeoutSecs = 30;
            _mqttConfiguration.MaximumQualityOfService = MqttQualityOfService.ExactlyOnce;
        }

        public void Start()
        {
            try
            {
                if (_mqttServer == null)
                {
                    _mqttServer = MqttServer.Create(_mqttConfiguration);
                    _mqttServer.ClientConnected += _mqttServer_ClientConnected;
                    _mqttServer.ClientDisconnected += _mqttServer_ClientDisconnected;
                    _mqttServer.MessageUndelivered += _mqttServer_MessageUndelivered;
                }
                _mqttServer.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void _mqttServer_MessageUndelivered(object sender, MqttUndeliveredMessage e)
        {
            Console.WriteLine($"Undelivered message: { e.SenderId } - { e.Message.Topic } - { Encoding.UTF8.GetString(e.Message.Payload) }");
        }

        private void _mqttServer_ClientDisconnected(object sender, string e)
        {
            IMqttServer mqttServer = sender as IMqttServer;
            Console.WriteLine($"Client disconnected: {e}");
        }

        private void _mqttServer_ClientConnected(object sender, string e)
        {
            Console.WriteLine($"Client connected: {e}");
        }

        public void Stop()
        {
            _mqttServer?.Stop();
            _mqttServer.ClientConnected -= _mqttServer_ClientConnected;
            _mqttServer.ClientDisconnected -= _mqttServer_ClientDisconnected;
            _mqttServer = null;
        }
    }
}
