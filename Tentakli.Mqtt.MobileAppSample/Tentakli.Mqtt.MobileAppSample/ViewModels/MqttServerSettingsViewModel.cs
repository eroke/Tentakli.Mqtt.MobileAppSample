﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Tentakli.Mqtt.MobileAppSample.Models;
using Xamarin.Forms;

namespace Tentakli.Mqtt.MobileAppSample.ViewModels
{
    class MqttServerSettingsViewModel : BaseViewModel
    {
        private ServerSettingsModel _settings;
        
        public ICommand SaveSettingsCommand { get; private set; }

        public IPAddress Host {
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

        public MqttServerSettingsViewModel()
        {
            Title = "Настройки MQTT сервера";
            _settings = MqttBrocker.GetSettings();
            SaveSettingsCommand = new Command(SaveSettings);
        }

        private void SaveSettings()
        {
            if (Port == 0)
                return;
            if (Timeout == 0)
                return;
            if (Host == null)
                return;

            MqttBrocker.SetSettings(_settings);
        }
    }
}
