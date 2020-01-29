using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Tentakli.Mqtt.MobileAppSample.Models
{
    public class ServerSettingsModel
    {
        public ushort Timeout;
        public ushort Port;
        public IPAddress Host;
    }
}
