using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodDeliveryMobileApp.Models;
using Newtonsoft.Json;

namespace FoodDeliveryMobileApp.Services
{
    public class DrinksService : BaseHttpService, IDrinksService
    {
        private Uri DrinksUri => new Uri($"{apiAdress}/drinks");

        public async Task<IEnumerable<Drink>> GetDrinksAsync()
        {
            var response = await httpClient.GetAsync(DrinksUri);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Cannot connect to server");

            string responseString = await response.Content.ReadAsStringAsync();

            var drinks = JsonConvert.DeserializeObject<IEnumerable<Drink>>(responseString);

            return drinks;
        }

        public Uri GetDrinkImageUri(Guid drinkId) => new Uri($"{apiAdress}/drinks/{drinkId}/image");

    }
}
