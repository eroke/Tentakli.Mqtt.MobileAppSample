﻿using System;
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
        //public ObservableCollection<Item> Items { get; set; }
        //public Command LoadItemsCommand { get; set; }

        public HomeViewModel()
        {
            Title = "Home";
            //Items = new ObservableCollection<Item>();
            //LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Item;
            //    Items.Add(newItem);
            //    await DataStore.AddItemAsync(newItem);
            //});
        }

        public void Connect()
        {
            
        }

        //async Task ExecuteLoadItemsCommand()
        //{
        //    if (IsBusy)
        //        return;

        //    IsBusy = true;

        //    try
        //    {
        //        Items.Clear();
        //        var items = await DataStore.GetItemsAsync(true);
        //        foreach (var item in items)
        //        {
        //            Items.Add(item);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}
    }
}