using FoodDeliveryServer.Models;
using System;
using System.Collections.Generic;

namespace FoodDeliveryServer.Infrastructure
{
    public static class CachedData
    {
        public static Dictionary<Guid, byte[]> Images { get; } = new Dictionary<Guid, byte[]>();

        public static List<PizzaViewModel> Pizzas { get; set; }
    }
}
