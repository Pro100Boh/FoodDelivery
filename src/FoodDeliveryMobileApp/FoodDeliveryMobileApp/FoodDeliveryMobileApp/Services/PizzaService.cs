using FoodDeliveryMobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDeliveryMobileApp.Services
{
    public class PizzaService : BaseHttpService, IPizzaService
    {
        private Uri PizzasUri => new Uri($"{apiAdress}/pizza");

        public async Task<IEnumerable<Pizza>> GetPizzasAsync()
        {
            var response = await httpClient.GetAsync(PizzasUri);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Cannot connect to server");

            string responseString = await response.Content.ReadAsStringAsync();

            var pizzas = JsonConvert.DeserializeObject<IEnumerable<Pizza>>(responseString);

            return pizzas;
        }

        public Uri GetPizzaImageUri(Guid pizzaId) => new Uri($"{apiAdress}/pizza/{pizzaId}/image");

        public Uri GetIngradientImageUri(Guid ingradientId) => new Uri($"{apiAdress}/ingradients/{ingradientId}/image");
    }
}
