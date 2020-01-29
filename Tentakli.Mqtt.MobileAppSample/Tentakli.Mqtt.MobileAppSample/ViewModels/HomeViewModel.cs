using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Tentakli.Mqtt.MobileAppSample.Models;
using Tentakli.Mqtt.MobileAppSample.Views;
using System.Windows.Input;

namespace Tentakli.Mqtt.MobileAppSample.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private bool _isClientConnected = false;
        private bool _isServerRunning = false;
        private int _p1;
        private int _p2;
        private int _p3;

        public int P1 {
            get {
                return _p1;
            }
            set {
                SetProperty(ref _p1, value);
            }
        }

        public int P2 {
            get {
                return _p2;
            }
            set {
                SetProperty(ref _p2, value);
            }
        }

        public int P3 {
            get {
                return _p3;
            }
            set {
                SetProperty(ref _p3, value);
            }
        }

        public bool IsClientConnected {
            get {
                return _isClientConnected;
            }
            set {
                SetProperty(ref _isClientConnected, value);
            }
        }

        public bool IsServerRunning {
            get {
                return _isServerRunning;
            }
            set {
                SetProperty(ref _isServerRunning, value);
            }
        }

        public ICommand TurnServerCommand { get; private set; }
        public ICommand TurnClientCommand { get; private set; }
        public HomeViewModel()
        {
            Title = "Home";
            TurnServerCommand = new Command<ToggledEventArgs>(TurnServer);
            TurnClientCommand = new Command<ToggledEventArgs>(TurnClient);
        }

        private void TurnServer(ToggledEventArgs args)
        {
            if (args.Value)
                MqttBrocker
                    .StartAsync()
                    .ContinueWith(t => {
                        if (t.IsFaulted)
                            IsServerRunning = false;
                    });
            else
                MqttBrocker.StopAsync();
        }

        private void TurnClient(ToggledEventArgs args)
        {
            if (args.Value)
                MqttClient
                    .Connect()
                    .ContinueWith(t => {
                        if (t.IsFaulted)
                            IsClientConnected = false;
                        else {
                            MqttClient.Subscribe("p1", MQTTnet.Protocol.MqttQualityOfServiceLevel.AtMostOnce, (value) => {
                                if (int.TryParse(value, out int p1))
                                    P1 = p1;
                            });
                            MqttClient.Subscribe("p2", MQTTnet.Protocol.MqttQualityOfServiceLevel.AtMostOnce, (value) => {
                                if (int.TryParse(value, out int p2))
                                    P2 = p2;
                            });
                            MqttClient.Subscribe("p3", MQTTnet.Protocol.MqttQualityOfServiceLevel.AtMostOnce, (value) => {
                                if (int.TryParse(value, out int p3))
                                    P3 = p3;
                            });
                        }
                    });
            else
                MqttClient.Disconnect();
        }
    }
}