﻿using FoodDeliveryMobileApp.Services;
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

            _pizzaViewModel = new PizzaViewModel(new PizzaService());

            // Connecting context of this page to the our View Model class
            BindingContext = _pizzaViewModel;
        }

        private async void PizzaPageAppearing(object sender, EventArgs e)
        {
            if (!_loaded)
            {
                await _pizzaViewModel.LoadPizzasAsync();
                _loaded = true;
            }
        }
    }
}
