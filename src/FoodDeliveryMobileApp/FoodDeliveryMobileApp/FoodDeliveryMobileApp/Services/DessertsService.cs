using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FoodDeliveryMobileApp.Models;
using Newtonsoft.Json;

namespace FoodDeliveryMobileApp.Services
{
    public class DessertsService : BaseHttpService, IDessertsService
    {
        private Uri DessertsUri => new Uri($"{apiAdress}/desserts");

        public async Task<IEnumerable<Dessert>> GetDessertsAsync()
        {
            var response = await httpClient.GetAsync(DessertsUri);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Cannot connect to server");

            string responseString = await response.Content.ReadAsStringAsync();

            var desserts = JsonConvert.DeserializeObject<IEnumerable<Dessert>>(responseString);

            return desserts;
        }

        public Uri GetDessertImageUri(Guid drinkId) => new Uri($"{apiAdress}/desserts/{drinkId}/image");
    }
}
