using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDeliveryMobileApp.Models
{
    public abstract class ProductBase
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual string FullName => Name;

        private decimal price;

        public decimal Price
        {
            get => price;
            set => price = Math.Round(value, 2);
        }
    }
}
