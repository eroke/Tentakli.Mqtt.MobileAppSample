using System;
using System.Collections.Generic;
using System.Text;

namespace Tentakli.Mqtt.MobileAppSample.Models
{
    public enum MenuItemType
    {
        Home,
        MqttServerSettings,
        MqttClientSettings,
        BrockerSettings,
        About
    }

    public class MainMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
