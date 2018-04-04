using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using App3.Models;
using App3.ViewModels;
using System.Threading.Tasks;
using Plugin.Share;

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
        void OpenInBrowser(object sender, EventArgs e)
        {
            string url =this.viewModel.Item.Url;
            Device.OpenUri(new Uri(url));
        }
        public Task<bool> CopyToClipboard(string text)
        {
            string url = this.viewModel.Item.Url;
            return CrossShare.Current.SetClipboardText(url);
        }
    }
}