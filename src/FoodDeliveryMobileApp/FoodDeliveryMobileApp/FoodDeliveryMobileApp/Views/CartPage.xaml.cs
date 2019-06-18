using FoodDeliveryMobileApp.Models;
using FoodDeliveryMobileApp.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodDeliveryMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        private readonly AccountViewModel _accountViewModel;

        public CartPage()
        {
            InitializeComponent();

            _accountViewModel = AccountViewModel.Instance;

            // Connecting context of this page to the our View Model class
            BindingContext = _accountViewModel;
        }

        private void UpdateCartPage()
        {
            bool showMakeOrderElements = _accountViewModel.Cart.Any();

            var stackLayout = this.Content as StackLayout;
            var elements = stackLayout.Children.Skip(1);

            foreach (var element in elements)
            {
                element.IsVisible = showMakeOrderElements;
            }
        }

        private async void CartPageAppearing(object sender, EventArgs e)
        {
            UpdateCartPage();

            if (!_accountViewModel.Cart.Any())
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert("", "You can choose any product at Products page!", "Ok");
                });
            }
        }

        private void RemoveFromCartButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;

            var product = button?.BindingContext as ProductBase;

            _accountViewModel.RemoveFromCart(product);

            UpdateCartPage();
        }

        private async void MakeOrderButtonClicked(object sender, EventArgs e)
        {
            if (!_accountViewModel.IsAuthorized)
                await DisplayAlert("Please sign in", "You must be authorized to make order!", "Ok");
            else
            {
                await _accountViewModel.MakeOrderAsync();
                await DisplayAlert("", "Thank you for the order!", "Ok");
                UpdateCartPage();
            }
        }
    }
}
