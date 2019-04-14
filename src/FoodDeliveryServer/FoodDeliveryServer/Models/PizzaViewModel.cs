using System;
using System.Collections.Generic;

namespace FoodDeliveryServer.Models
{
    public class PizzaViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public IEnumerable<IngradientViewModel> Ingradients { get; set; }
    }
}
