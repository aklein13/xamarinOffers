using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App3.Models;
using Newtonsoft.Json;
using PCLStorage;

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
        public async void Fav()
        {
            this.Item.IsFavourite = !this.Item.IsFavourite;
            this.FavText = this.Item.IsFavourite ? "DEL" : "FAV";
            await FavStorage();
        }
        public async Task FavStorage()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync("DolentaCache", CreationCollisionOption.OpenIfExists);
            string fileName = "favourites1.txt";
            IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            string content = await file.ReadAllTextAsync();
            List<Offer> Favs = new List<Offer>();
            Console.WriteLine("Current fav state");
            try
            {
                Favs = JsonConvert.DeserializeObject<List<Offer>>(content);
                for (int i = Favs.Count - 1; i >= 0; i--)
                {
                    if (Favs[i].Id == this.Item.Id)
                        Favs.RemoveAt(i);
                }
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Nie bylo");
                Favs = new List<Offer>();
            }
            
            if (this.Item.IsFavourite)
            {
                Favs.Add(this.Item);
            }
            foreach (Offer temp in Favs)
            {
                Console.WriteLine(temp.Title);
            }

            file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            string json = JsonConvert.SerializeObject(Favs);
            await file.WriteAllTextAsync(json);
        }
    }
}
