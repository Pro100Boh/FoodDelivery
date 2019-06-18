using FoodDeliveryMobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryMobileApp.Services
{
    public class AccountService : BaseHttpService, IAccountService
    {
        private Uri OrdersUri => new Uri($"{serverApiAdress}/orders");

        private Uri RegisterUri => new Uri($"{serverApiAdress}/accounts/register");

        private Uri LogInUri => new Uri($"{serverApiAdress}/accounts/login");

        private string _token;

        public async Task<bool> RegiserAsync(string email, string password)
        {
            var json =  JsonConvert.SerializeObject(new
            {
                email,
                password
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(RegisterUri, content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return false;
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                return false;
            else
                throw new Exception("Cannot connect to server");
        }

        public async Task<bool> LogInAsync(string email, string password)
        {
            var json = JsonConvert.SerializeObject(new
            {
                email,
                password
            });
            
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(LogInUri, content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                _token = await response.Content.ReadAsStringAsync();
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _token);
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return false;
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                return false;
            else
                throw new Exception("Cannot connect to server");
        }

        public void LogOut()
        {
            _token = null;
        }

        public async Task<IEnumerable<Order>> GetOrdersHistoryAsync()
        {
            var response = await httpClient.GetAsync(OrdersUri);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Cannot connect to server");

            string responseString = await response.Content.ReadAsStringAsync();

            var orders = JsonConvert.DeserializeObject<IEnumerable<Order>>(responseString);

            return orders;
        }

        public async Task<bool> MakeOrderAsync(IEnumerable<Guid> productsIds)
        {
            string json = JsonConvert.SerializeObject(productsIds);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(OrdersUri, content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Cannot connect to server");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                return false;
            else
                throw new Exception("Cannot connect to server");
        }
    }
}
