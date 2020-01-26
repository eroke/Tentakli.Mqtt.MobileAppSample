using Tentakli.Mqtt.MobileAppSample.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tentakli.Mqtt.MobileAppSample.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<MainMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<MainMenuItem>
            {
                new MainMenuItem {Id = MenuItemType.Home, Title="Home" },
                new MainMenuItem {Id = MenuItemType.MqttClientSettings, Title="Настройки MQTT клиента" },
                new MainMenuItem {Id = MenuItemType.MqttServerSettings, Title="Настройки MQTT сервера" },
                new MainMenuItem {Id = MenuItemType.About, Title="About" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                int id = (int)((MainMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}