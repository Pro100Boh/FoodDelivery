using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FoodDeliveryMobileApp.Models
{
    public class Pizza : ProductBase
    {
        public Uri PizzaImageUri { get; set; }

        public ObservableCollection<Ingradient> Ingradients { get; set; }

        public override string FullName => $"Pizza {Name}";
    }
}
