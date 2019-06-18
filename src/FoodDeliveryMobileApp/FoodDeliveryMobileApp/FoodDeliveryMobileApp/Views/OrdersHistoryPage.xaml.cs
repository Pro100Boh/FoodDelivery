using FoodDeliveryMobileApp.Services;
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
    public partial class OrdersHistoryPage : ContentPage
    {
        private readonly AccountViewModel _accountViewModel;

        public OrdersHistoryPage()
        {
            InitializeComponent();

            _accountViewModel = AccountViewModel.Instance;

            // Connecting context of this page to the our View Model class
            BindingContext = _accountViewModel;
        }

        private async void OrdersHistoryPageAppearing(object sender, EventArgs e)
        {
            _accountViewModel.OrdersHistoryCollection.Clear();

            if (!_accountViewModel.IsAuthorized)
                await DisplayAlert("", "Please sign in to view your orders history", "Ok");
            else
            {
                await _accountViewModel.LoadOrdersHistoryAsync();
            }

        }
    }
}
