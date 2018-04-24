using System;
using App3.Views;
using PCLStorage;
using Xamarin.Forms;

namespace App3
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            MainPage = new MainPage();
        }
		protected override async void OnStart ()
		{
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync("DolentaCache", CreationCollisionOption.OpenIfExists);
            string fileName = "previousCity.txt";
            IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            string content = await file.ReadAllTextAsync();
            if (content == "Sopot" || content == "Gdańsk")
            {
                MessagingCenter.Send<object, string>(this, "City", content);
            }
        }
		protected override async void OnSleep ()
		{
            if (Current.Properties.ContainsKey("previous"))
            {
                Console.WriteLine("Contains");
                Console.WriteLine(Current.Properties["previous"]);
                IFolder rootFolder = FileSystem.Current.LocalStorage;
                IFolder folder = await rootFolder.CreateFolderAsync("DolentaCache", CreationCollisionOption.OpenIfExists);
                IFile file = await folder.CreateFileAsync("previousCity.txt", CreationCollisionOption.ReplaceExisting);;
                await file.WriteAllTextAsync(Current.Properties["previous"] as string);
            }
		}
		protected override void OnResume ()
		{
            Console.WriteLine("Resume");
        }
	}
}
