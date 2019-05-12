using FoodDeliveryMobileApp.Services;
using FoodDeliveryMobileApp.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodDeliveryMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DessertsPage : ContentPage
    {
        private readonly DessertsViewModel _dessertsViewModel;

        private bool _loaded = false;

        public DessertsPage()
        {

            InitializeComponent();

            _dessertsViewModel = new DessertsViewModel(new DessertsService());

            // Connecting context of this page to the our View Model class
            BindingContext = _dessertsViewModel;
        }

        private async void DessertsPageAppearing(object sender, EventArgs e)
        {
            if (!_loaded)
            {
                await _dessertsViewModel.LoadDessertsAsync();
                _loaded = true;
            }
        }
    }
}
