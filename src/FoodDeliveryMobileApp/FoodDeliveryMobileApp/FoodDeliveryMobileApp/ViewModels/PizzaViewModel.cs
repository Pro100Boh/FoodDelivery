using FoodDeliveryMobileApp.Models;
using FoodDeliveryMobileApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;

namespace FoodDeliveryMobileApp.ViewModels
{
    public class PizzaViewModel
    {
        private IPizzaService _pizzaService;

        public ObservableCollection<Pizza> PizzasCollection { get; set; }

        public PizzaViewModel(IPizzaService pizzaService)
        {
            PizzasCollection = new ObservableCollection<Pizza>();
            _pizzaService = pizzaService;
        }

        public async Task LoadPizzasAsync()
        {
            var pizzas = await _pizzaService.GetPizzasAsync();

            foreach (var pizza in pizzas)
            {
                pizza.PizzaImageUri = _pizzaService.GetPizzaImageUri(pizza.Id);

                foreach (var ingradient in pizza.Ingradients)
                {
                    ingradient.IngradientImageUri = _pizzaService.GetIngradientImageUri(ingradient.Id);
                }

                PizzasCollection.Add(pizza);
            }

        }

    }
}
