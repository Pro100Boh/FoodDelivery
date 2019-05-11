using FoodDeliveryMobileApp.Models;
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
        private const string apiAdress = "http://192.168.31.13:5000/api";

        private readonly HttpClient httpClient;

        public ObservableCollection<Pizza> PizzasCollection { get; set; }

        public PizzaViewModel()
        {
            PizzasCollection = new ObservableCollection<Pizza>();

            httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(15)
            };
        }

        public async Task LoadPizzasAsync()
        {
            var response = await httpClient.GetAsync(GetPizzasUri());

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Cannot connect to server");

            string responseString = await response.Content.ReadAsStringAsync();
            var pizzas = JsonConvert.DeserializeObject<IEnumerable<Pizza>>(responseString);

            foreach (var pizza in pizzas)
            {
                pizza.PizzaImageUri = GetPizzaImageUri(pizza.Id);

                foreach (var ingradient in pizza.Ingradients)
                {
                    ingradient.IngradientImageUri = GetIngradientImageUri(ingradient.Id);
                }

                PizzasCollection.Add(pizza);
            }

        }

        private static Uri GetPizzasUri() => new Uri($"{apiAdress}/pizza");

        private static Uri GetPizzaImageUri(Guid pizzaId) => new Uri($"{apiAdress}/pizza/{pizzaId}/image");

        private static Uri GetIngradientImageUri(Guid ingradientId) => new Uri($"{apiAdress}/ingradients/{ingradientId}/image");

    }
}
