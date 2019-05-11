using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FoodDeliveryMobileApp.Models
{
    public class Pizza
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        private decimal price;

        public decimal Price
        {
            get => price;
            set => price = Math.Round(value, 2);
        }

        public Uri PizzaImageUri { get; set; }

        public ObservableCollection<Ingradient> Ingradients { get; set; }
    }
}
