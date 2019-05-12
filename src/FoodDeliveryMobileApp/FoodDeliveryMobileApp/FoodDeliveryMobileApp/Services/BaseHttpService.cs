using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FoodDeliveryMobileApp.Services
{
    public abstract class BaseHttpService
    {
        protected const string apiAdress = "http://192.168.31.13:5000/api";

        protected readonly HttpClient httpClient;

        public BaseHttpService()
        {
            httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(15)
            };
        }

    }
}
