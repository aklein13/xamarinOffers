using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using App3.Models;
using App3.Views;
using System.Json;
using System.Collections.Generic;

namespace App3.ViewModels
{
    public class FavouritesViewModel : BaseViewModel
    {
        public ObservableCollection<Offer> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public FavouritesViewModel()
        {
            Title = "Ulubione";
            Items = new ObservableCollection<Offer>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Offer>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Offer;
                Items.Add(_item);
                await DataStore.AddItemAsync(_item);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                //var items = await DataStore.GetItemsAsync(true);
                var offers = await DataStore.FetchOffersAsync();
                //string sampleUrl = "https://otodompl-imagestmp.akamaized.net/images_otodompl/23505315_3_1280x1024_wygodne-mieszkanie-na-osiedlu-aquarius-w-sopocie-mieszkania_rev011.jpg";
                foreach (JsonObject offer in offers["results"])
                {
                    JsonValue raw = offer["raw"];
                    string[] imageArray = raw["images"].ToString().Replace("[", "").Replace("]", "").Replace("\\", "").Replace("\"", "").Split(',');
                    Offer tempOffer = new Offer
                    {
                        Id = raw["offer_id"],
                        Title = raw["title"],
                        Price = raw["price"],
                        City = raw["city"],
                        Description = raw["description"],
                        Images = imageArray,
                    };
                    Items.Add(tempOffer);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
