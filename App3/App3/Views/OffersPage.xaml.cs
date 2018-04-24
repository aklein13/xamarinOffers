using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App3.Models;
using App3.ViewModels;

namespace App3.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemsPage : ContentPage
	{
        ItemsViewModel viewModel;
        public ItemsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ItemsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Offer;
            if (item == null)
                return;
            await Navigation.PushModalAsync(new NavigationPage(new ItemDetailPage(new ItemDetailViewModel(item))));
            ItemsListView.SelectedItem = null;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
        public void PickerItemSelected()
        {
            viewModel.FilterItemsCommand.Execute(null);
        }
    }
}