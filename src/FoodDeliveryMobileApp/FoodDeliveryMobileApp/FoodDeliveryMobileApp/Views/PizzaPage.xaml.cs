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
    public partial class PizzaPage : ContentPage
    {
        private PizzaViewModel pizzaViewModel;

        public PizzaPage()
        {

            InitializeComponent();

            pizzaViewModel = new PizzaViewModel(new PizzaService());

            // Connecting context of this page to the our View Model class
            BindingContext = pizzaViewModel;
        }

        private async void PizzaPageAppearing(object sender, EventArgs e)
        {
            await pizzaViewModel.LoadPizzasAsync();

        }
    }
}
