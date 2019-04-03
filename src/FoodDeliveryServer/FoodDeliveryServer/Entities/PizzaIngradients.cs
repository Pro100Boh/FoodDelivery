using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDeliveryServer.Entities
{
    public class PizzaIngradients
    {
        public Guid PizzaId { get; set; }
        public Pizza Pizza { get; set; }

        public Guid IngradientId { get; set; }
        public Ingradient Ingradient { get; set; }
    }
}
