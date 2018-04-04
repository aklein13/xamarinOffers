using System;

using App3.Models;

namespace App3.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Offer Item { get; set; }
        public ItemDetailViewModel(Offer item = null)
        {
            Title = item?.Title;
            Item = item;
        }
    }
}
