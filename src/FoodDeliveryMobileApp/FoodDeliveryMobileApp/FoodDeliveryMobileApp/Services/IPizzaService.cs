using FoodDeliveryMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDeliveryMobileApp.Services
{
    public interface IPizzaService
    {
        Task<IEnumerable<Pizza>> GetPizzasAsync();

        Uri GetPizzaImageUri(Guid pizzaId);

        Uri GetIngradientImageUri(Guid ingradientId);
    }
}
