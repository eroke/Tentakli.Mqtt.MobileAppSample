using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Tentakli.Mqtt.MobileAppSample.Models;
using Tentakli.Mqtt.MobileAppSample.Views;
using Tentakli.Mqtt.MobileAppSample.ViewModels;

namespace Tentakli.Mqtt.MobileAppSample.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class HomePage : ContentPage
    {
        private HomeViewModel _viewModel;

        public HomePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new HomeViewModel();
        }
    }
}