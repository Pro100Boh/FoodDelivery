using FoodDeliveryMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryMobileApp.Services
{
    public interface IAccountService
    {
        Task<bool> RegiserAsync(string email, string password);

        Task<bool> LogInAsync(string email, string password);

        void LogOut();

        Task<IEnumerable<Order>> GetOrdersHistoryAsync();

        Task<bool> MakeOrderAsync(IEnumerable<Guid> productsIds);
    }
}
