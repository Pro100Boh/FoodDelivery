using FoodDeliveryMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodDeliveryMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageAccountPage : ContentPage
    {
        private readonly AccountViewModel _accountViewModel;

        public ManageAccountPage()
        {
            InitializeComponent();

            _accountViewModel = AccountViewModel.Instance;

            // Connecting context of this page to the our View Model class
            BindingContext = _accountViewModel;
        }

        private async void LogOutClicked(object sender, EventArgs e)
        {
            _accountViewModel.LogOut();

            await DisplayAlert("", "Signed out!", "Ok");
        }
    }
}