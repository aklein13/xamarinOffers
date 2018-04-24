using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App3.ViewModels;

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
        public void OpenInBrowser()
        {
            this.viewModel.OpenInBrowserCommand.Execute(null);
        }
        public void CopyToClipboard()
        {
            this.viewModel.ShareItemCommand.Execute(null);
        }
        public void FavOffer()
        {
            this.viewModel.FavItemCommand.Execute(null);
        }
        async void Back()
        {
            await Navigation.PopModalAsync();
        }
    }
}