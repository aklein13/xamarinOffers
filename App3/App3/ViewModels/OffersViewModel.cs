using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using App3.Models;
using System.Json;
using System.Collections.Generic;
using Newtonsoft.Json;
using PCLStorage;

namespace App3.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Offer> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command FilterItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Oferty";
            Items = new ObservableCollection<Offer>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            FilterItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(true));
            City = "Gdynia";
            CityList = new List<string> { "Gdynia", "Sopot", "Gdańsk" };
            MessagingCenter.Subscribe<object, string>(this, "City", (sender, arg) => {
                try
                {
                    if (arg != this.City)
                    {
                        this.City = arg;
                        FilterItemsCommand.Execute(true);
                    }
                }
                catch { }
            });
        }

        async Task ExecuteLoadItemsCommand(bool ShouldFilter = false)
        {
            if (ShouldFilter)
            {
                Application.Current.Properties["previous"] = this.City;
            }
            if (IsBusy)
                await Task.Delay(500);
            IsBusy = true;
            try
            {
                IFolder rootFolder = FileSystem.Current.LocalStorage;
                IFolder folder = await rootFolder.CreateFolderAsync("DolentaCache", CreationCollisionOption.OpenIfExists);
                string fileName = "favourites.txt";
                IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
                string content = await file.ReadAllTextAsync();
                List<Offer> Favs = new List<Offer>();
                try
                {
                    Favs = JsonConvert.DeserializeObject<List<Offer>>(content);
                }
                catch (NullReferenceException)
                {
                    Favs = new List<Offer>();
                }
                Items.Clear();
                var offers = await DataStore.FetchOffersAsync(this.City);
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
                        District = raw["district"],
                        Rooms = raw["rooms"].ToString(),
                        Url = raw["url"],
                        Description = raw["description"],
                        Images = imageArray,
                    };
                    Items.Add(tempOffer);
                    foreach (Offer fav in Favs)
                    {
                        if (tempOffer.Id == fav.Id)
                        {
                            tempOffer.IsFavourite = true;
                        }
                    }
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