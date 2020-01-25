using System;

using Tentakli.Mqtt.MobileAppSample.Models;

namespace Tentakli.Mqtt.MobileAppSample.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
