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
    public partial class LogInPage : ContentPage
    {
        private readonly AccountViewModel _accountViewModel;

        public LogInPage()
        {
            InitializeComponent();

            _accountViewModel = AccountViewModel.Instance;

            // Connecting context of this page to the our View Model class
            BindingContext = _accountViewModel;
        }

        private async void LogInClicked(object sender, EventArgs e)
        {
            var stackLayout = this.Content as StackLayout;
            var elements = stackLayout.Children.Skip(1).Take(2);

            var emailEntry = elements.FirstOrDefault() as Entry;
            var passwordEntry = elements.LastOrDefault() as Entry;

            if (string.IsNullOrWhiteSpace(emailEntry.Text))
            {
                await DisplayAlert("", "Please provide email", "Ok");
                return;
            }
            if (string.IsNullOrWhiteSpace(passwordEntry.Text))
            {
                await DisplayAlert("", "Please password email", "Ok");
                return;
            }

            await _accountViewModel.LogInAsync(emailEntry.Text, passwordEntry.Text);

            if (_accountViewModel.IsAuthorized)
            {
                await DisplayAlert("", "Successfully signed in!", "Ok");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("", "Incorrect login or password!", "Ok");
            }
        }

    }
}