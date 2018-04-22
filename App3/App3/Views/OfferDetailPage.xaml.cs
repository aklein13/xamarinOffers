using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using App3.Models;
using App3.ViewModels;
using System.Threading.Tasks;
using Plugin.Share;
using App3.Services;

namespace App3.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemDetailPage : ContentPage
	{
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Offer
            {
                Title = "Offer 1",
                Description = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }
        public void OpenInBrowser()
        {
            string url =this.viewModel.Item.Url;
            Device.OpenUri(new Uri(url));
        }
        async void CopyToClipboard()
        {
            await DependencyService.Get<IShare>().ShareAsync(this.viewModel.Item);
            //string url = this.viewModel.Item.Url;
            //return CrossShare.Current.SetClipboardText(url);
        }
        public void FavOffer()
        {
            this.viewModel.Fav();
        }
        async void Back()
        {
            await Navigation.PopModalAsync();
        }
    }
}