using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDeliveryMobileApp.Models
{
    public class Pizza
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public Uri ImageUri { get; set; }

        public IEnumerable<Ingradient> Ingradients { get; set; }
    }
}
