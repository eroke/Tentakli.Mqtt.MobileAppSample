using System;
using System.Collections.Generic;
using System.Text;

namespace Tentakli.Mqtt.MobileAppSample.Models
{
    public class ClientSettingsModel
    {
        public string ClientId { get; set; }
        public ushort Timeout { get; set; }
        public string Host { get; set; }
        public ushort Port { get; set; }
    }
}
