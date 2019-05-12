using FoodDeliveryMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDeliveryMobileApp.Services
{
    public interface IDessertsService
    {
        Task<IEnumerable<Dessert>> GetDessertsAsync();

        Uri GetDessertImageUri(Guid drinkId);
    }
}
