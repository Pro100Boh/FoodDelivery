using FoodDeliveryMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDeliveryMobileApp.Services
{
    public interface IDrinksService
    {
        Task<IEnumerable<Drink>> GetDrinksAsync();

        Uri GetDrinkImageUri(Guid drinkId);
    }
}
