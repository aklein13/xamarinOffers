using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App3.Models;
using App3.Services;
using Newtonsoft.Json;
using PCLStorage;
using Xamarin.Forms;

namespace App3.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Offer Item { get; set; }
        public Command OpenInBrowserCommand { get; set; }
        public Command FavItemCommand { get; set; }
        public Command ShareItemCommand { get; set; }
        public string _favfext;
        public string FavText {
            get
            { return _favfext; }
            set
            { SetProperty(ref _favfext, value); }
        }
        public ItemDetailViewModel(Offer item = null, string favText = "F")
        {
            favText = item.IsFavourite ? "★" : "☆";
            Title = item?.Title;
            Item = item;
            FavText = favText;
            OpenInBrowserCommand = new Command(() => OpenInBrowser());
            FavItemCommand = new Command(async() => await Fav());
            ShareItemCommand = new Command(async () => await ShareItem());
        }
        public async Task ShareItem()
        {
            await DependencyService.Get<IShare>().ShareAsync(this.Item);
        }
        public void OpenInBrowser()
        {
            Device.OpenUri(new Uri(this.Item.Url));
        }
        public async Task Fav()
        {
            this.Item.IsFavourite = !this.Item.IsFavourite;
            this.FavText = this.Item.IsFavourite ? "★" : "☆";
            await FavStorage();
        }
        public async Task FavStorage()
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
                for (int i = Favs.Count - 1; i >= 0; i--)
                {
                    if (Favs[i].Id == this.Item.Id)
                        Favs.RemoveAt(i);
                }
            }
            catch (NullReferenceException)
            {
                Favs = new List<Offer>();
            }
            if (this.Item.IsFavourite)
            {
                Favs.Add(this.Item);
            }
            file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            string json = JsonConvert.SerializeObject(Favs);
            await file.WriteAllTextAsync(json);
        }
    }
}
