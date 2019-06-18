using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodDeliveryMobileApp.Models
{
    public class Order
    {
        public DateTime OrderDate { get; set; }

        public IEnumerable<OrderedProduct> OrderedProducts { get; set; }

        public string ViewString
        {
            get
            {
                string res = $"\t Order date: {OrderDate.ToString()} \n\t Products: \n";

                foreach (var OrderedProduct in OrderedProducts)
                {
                    res += $"\t\t • {OrderedProduct.ProductName} - {OrderedProduct.ProductPrice} $ \n";
                }
                res += $"\t Total price: {OrderedProducts.Select(p => p.ProductPrice).Sum()} $";

                return res;
            }
        }
    }
}
