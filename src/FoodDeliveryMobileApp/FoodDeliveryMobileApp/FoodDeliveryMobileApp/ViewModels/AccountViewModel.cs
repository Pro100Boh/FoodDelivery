using FoodDeliveryMobileApp.Models;
using FoodDeliveryMobileApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FoodDeliveryMobileApp.ViewModels
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        private readonly IAccountService _accountService;

        public ObservableCollection<Order> OrdersHistoryCollection { get; set; }

        public ObservableCollection<ProductBase> Cart { get; set; }

        private AccountViewModel()
        {
            OrdersHistoryCollection = new ObservableCollection<Order>();
            Cart = new ObservableCollection<ProductBase>();
            _accountService = new AccountService();
        }

        private static AccountViewModel source = null;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public static AccountViewModel Instance
        {
            get
            {
                if (source == null)
                    source = new AccountViewModel();

                return source;
            }
        }

        public async Task RegiserAsync(string email, string password)
        {
            await _accountService.RegiserAsync(email, password);
        }

        public async Task LogInAsync(string email, string password)
        {
            IsAuthorized = await _accountService.LogInAsync(email, password);
            if (IsAuthorized)
                UserEmail = email;
        }

        public void LogOut()
        {
            IsAuthorized = false;
            UserEmail = null;
            _accountService.LogOut();
        }

        public async Task LoadOrdersHistoryAsync()
        {
            var orders = await _accountService.GetOrdersHistoryAsync();

            foreach (var order in orders)
            {
                OrdersHistoryCollection.Add(order);
            }

        }

        public async Task<bool> MakeOrderAsync()
        {
            var productsIds = Cart.Select(p => p.Id);

            var successFlag = await _accountService.MakeOrderAsync(productsIds);

            if (successFlag)
            {
                Cart.Clear();
            }
            return successFlag;
        }

        public void AddToCart(ProductBase product)
        {
            Cart.Add(product);
        }

        public void RemoveFromCart(ProductBase product)
        {
            Cart.Remove(product);
            OnPropertyChanged(nameof(TotalPrice));
        }

        public decimal TotalPrice => Cart.Select(p => p.Price).Sum();

        public bool IsAuthorized { get; private set; } = false;

        public string UserEmail { get; set; }

    }
}
