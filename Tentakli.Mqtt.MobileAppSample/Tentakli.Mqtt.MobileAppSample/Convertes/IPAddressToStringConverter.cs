using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using Xamarin.Forms;

namespace Tentakli.Mqtt.MobileAppSample.Convertes
{
    class IPAddressToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is IPAddress && targetType == typeof(string))
                return value.ToString();
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is string && targetType == typeof(IPAddress))
                if (IPAddress.TryParse((string)value, out IPAddress address))
                    return address;

            return null;
        }
    }
}
