using FoodDeliveryMobileApp.Models;
using FoodDeliveryMobileApp.Services;
using FoodDeliveryMobileApp.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodDeliveryMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PizzaPage : ContentPage
    {
        private readonly PizzaViewModel _pizzaViewModel;

        private bool _loaded = false;

        public PizzaPage()
        {

            InitializeComponent();

            _pizzaViewModel = PizzaViewModel.Instance;

            // Connecting context of this page to the our View Model class
            BindingContext = _pizzaViewModel;
        }

        private async void PizzaPageAppearing(object sender, EventArgs e)
        {
            try
            {
                if (!_loaded)
                {
                    await _pizzaViewModel.LoadPizzasAsync();
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

            var pizza = button?.BindingContext as Pizza;

            if (pizza != null)
            {
                if (await DisplayAlert("", $"{pizza.FullName} will be added to your cart", "Ok", "Cancel"))
                    AccountViewModel.Instance.AddToCart(pizza);
            }
        }
    }
}
