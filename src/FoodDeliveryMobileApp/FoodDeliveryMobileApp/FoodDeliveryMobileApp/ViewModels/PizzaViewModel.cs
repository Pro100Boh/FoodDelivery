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
        public ObservableCollection<Pizza> PizzasCollection { get; set; }

        public PizzaViewModel()
        {
            PizzasCollection = new ObservableCollection<Pizza>();
        }

        private const string apiAdress = "http://192.168.31.13:5000/api";

        public async Task LoadPizzasAsync()
        {
            var httpClient = new HttpClient();

            string requestUri = apiAdress + "/pizza";

            var response = await httpClient.GetAsync(requestUri);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Cannot connect to server");

            string responseString = await response.Content.ReadAsStringAsync();
            var pizzas = JsonConvert.DeserializeObject<IEnumerable<Pizza>>(responseString);

            foreach (var pizza in pizzas)
            {
                pizza.PizzaImageUri = new Uri($"{requestUri}/{pizza.Id}/image");

                foreach (var ingradient in pizza.Ingradients)
                {
                    ingradient.IngradientImageUri = new Uri($"{apiAdress}/ingradients/{ingradient.Id}/image");
                }

                PizzasCollection.Add(pizza);
            }

        }


    }
}
