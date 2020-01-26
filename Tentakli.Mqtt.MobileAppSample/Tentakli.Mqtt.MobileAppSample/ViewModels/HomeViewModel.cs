using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Tentakli.Mqtt.MobileAppSample.Models;
using Tentakli.Mqtt.MobileAppSample.Views;

namespace Tentakli.Mqtt.MobileAppSample.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel()
        {
            Title = "Home";
        }
    }
}