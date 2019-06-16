using System;
using System.Collections.Generic;

namespace FoodDeliveryServer.Models
{
    public class OrderViewModel
    {
        public DateTime OrderDate { get; set; }

        public IEnumerable<OrderedProductViewModel> OrderedProducts { get; set; }
    }
}
