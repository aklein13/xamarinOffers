using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using App3.Models;
using App3.ViewModels;

namespace App3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavouritesPage : ContentPage
    {
        FavouritesViewModel viewModel;
        public FavouritesPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new FavouritesViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Offer;
            if (item == null)
                return;
            await Navigation.PushModalAsync(new NavigationPage(new ItemDetailPage(new ItemDetailViewModel(item))));
            FavouritesView.SelectedItem = null;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = viewModel = new FavouritesViewModel();
            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}