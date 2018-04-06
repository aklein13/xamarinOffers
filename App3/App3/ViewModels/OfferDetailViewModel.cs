using System;

using App3.Models;

namespace App3.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Offer Item { get; set; }
        public string _favfext;
        public string FavText {
            get
            { return _favfext; }
            set
            { SetProperty(ref _favfext, value); }
        }
        public ItemDetailViewModel(Offer item = null, string favText = "F")
        {
            favText = item.IsFavourite ? "DEL" : "FAV";
            Title = item?.Title;
            Item = item;
            FavText = favText;
        }
        public void Fav()
        {
            this.Item.IsFavourite = !this.Item.IsFavourite;
            this.FavText = this.Item.IsFavourite ? "DEL" : "FAV";
        }
    }
}
