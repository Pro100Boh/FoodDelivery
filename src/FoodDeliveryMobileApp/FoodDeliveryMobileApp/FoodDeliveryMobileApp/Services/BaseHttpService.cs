using System;
using System.Net.Http;

namespace FoodDeliveryMobileApp.Services
{
    public abstract class BaseHttpService
    {
        protected readonly string serverApiAdress;

        protected readonly HttpClient httpClient;

        public BaseHttpService()
        {
            serverApiAdress =  App.Current.Properties["serverApiAdress"] as string;

            httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(15)
            };
        }

    }
}
