using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDeliveryMobileApp.Models
{
    public class OrderedProduct
    {
        public string ProductName { get; set; }

        private decimal price;

        public decimal ProductPrice
        {
            get => price;
            set => price = Math.Round(value, 2);
        }
    }
}
