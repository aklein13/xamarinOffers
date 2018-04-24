using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using App3.Models;
using App3.Views;
using System.Collections.Generic;
using PCLStorage;
using Newtonsoft.Json;

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
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                await Task.Delay(300);

            IsBusy = true;

            try
            {
                Items.Clear();
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
                catch (System.NullReferenceException)
                {
                    Console.WriteLine("Nie bylo");
                    Favs = new List<Offer>();
                }
                foreach (Offer temp in Favs)
                {
                    Items.Add(temp);
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
