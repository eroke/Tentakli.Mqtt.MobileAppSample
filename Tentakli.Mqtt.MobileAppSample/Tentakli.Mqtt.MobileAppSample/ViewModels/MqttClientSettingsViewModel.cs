using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Tentakli.Mqtt.MobileAppSample.Models;
using Xamarin.Forms;

namespace Tentakli.Mqtt.MobileAppSample.ViewModels
{
    public class MqttClientSettingsViewModel : BaseViewModel
    {
        private ClientSettingsModel _settings = null;

        public string ClientId {
            get {
                return _settings.ClientId;
            }
            set {
                if (value == _settings.ClientId)
                    return;
                _settings.ClientId = value;
            }
        }
        public string Host {
            get {
                return _settings.Host;
            }
            set {
                if (value == _settings.Host)
                    return;
                _settings.Host = value;
            }
        }

        public ushort Port {
            get {
                return _settings.Port;
            }
            set {
                if (value == _settings.Port)
                    return;
                _settings.Port = value;
            }
        }
        public ushort Timeout {
            get {
                return _settings.Timeout;
            }
            set {
                if (value == _settings.Timeout)
                    return;
                _settings.Timeout = value;
            }
        }

        public ICommand SaveSettingsCommand { get; private set; }

        public MqttClientSettingsViewModel()
        {
            Title = "Настройки MQTT клиента";
            _settings = MqttClient.GetSettings();
            SaveSettingsCommand = new Command(SaveSettings);
        }

        private void SaveSettings()
        {
            MqttClient.SetSettings(_settings);
        }
    }
}
