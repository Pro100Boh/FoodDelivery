using FoodDeliveryMobileApp.Models;
using FoodDeliveryMobileApp.Services;
using FoodDeliveryMobileApp.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodDeliveryMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrinksPage : ContentPage
    {
        private readonly DrinksViewModel _drinksViewModel;

        private bool _loaded = false;

        public DrinksPage()
        {

            InitializeComponent();

            _drinksViewModel = new DrinksViewModel(new DrinksService());

            // Connecting context of this page to the our View Model class
            BindingContext = _drinksViewModel;
        }

        private async void DrinksPageAppearing(object sender, EventArgs e)
        {
            try
            {
                if (!_loaded)
                {
                    await _drinksViewModel.LoadDrinksAsync();
                    _loaded = true;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("", ex.Message, "Ok");
            }

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;

            var drink = button?.BindingContext as Drink;

            if (drink != null)
            {
                if (await DisplayAlert("", $"{drink.Name} will be added to your cart", "Ok", "Cancel"))
                    AccountViewModel.Instance.AddToCart(drink);
            }
        }
    }
}
